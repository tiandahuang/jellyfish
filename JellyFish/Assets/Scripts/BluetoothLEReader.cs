using UnityEngine;
using System;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class BluetoothLEReader : MonoBehaviour
{
    [Header("XIAO BLE Device Info")]
    public string TargetDeviceName = "Jelly Broadcast Test";
    public string ServiceUUID = "deadbeef-dead-beef-dead-beefdeadbeef";
    public string CharacteristicUUID = "deadbeef-dead-beef-dead-beefdeadbeef";

    [Header("Debug TextMesh (Optional)")]
    [Tooltip("실시간 로그를 표시할 TextMesh 객체")]
    public TextMeshProUGUI logTextMesh;

    private enum States { None, Initialize, Scan, Connect, Subscribe, Subscribed, Disconnect }
    private States _currentState = States.None;

    private string _connectedDeviceAddress = null;
    private bool _isScanning = false;
    private bool _isConnected = false;

    private void Start()
    {
        SetState(States.Initialize);
    }

    private void Update()
    {
        switch (_currentState)
        {
            case States.Initialize:
                // BLE 초기화
                BluetoothLEHardwareInterface.Initialize(
                    asCentral: true,
                    asPeripheral: false,
                    action: () =>
                    {
                        LogMessage("[BLE] Initialized.");
                        SetState(States.Scan, 1.0f);
                    },
                    errorAction: (error) =>
                    {
                        LogError("[BLE] Init Error: " + error);
                    }
                );
                _currentState = States.None;
                break;

            case States.Scan:
                StartScan();
                _currentState = States.None;
                break;

            case States.Connect:
                if (_isScanning)
                {
                    BluetoothLEHardwareInterface.StopScan();
                    _isScanning = false;
                }
                ConnectToDevice(_connectedDeviceAddress);
                _currentState = States.None;
                break;

            case States.Subscribe:
                SubscribeToCharacteristic(_connectedDeviceAddress, ServiceUUID, CharacteristicUUID);
                _currentState = States.None;
                break;

            case States.Disconnect:
                if (_isConnected)
                {
                    BluetoothLEHardwareInterface.DisconnectPeripheral(_connectedDeviceAddress, (address) =>
                    {
                        LogWarning("[BLE] Disconnected from " + address);
                        _isConnected = false;
                        _connectedDeviceAddress = null;
                    });
                }
                _currentState = States.None;
                break;
        }
    }

    private States _nextState;
    private void SetState(States newState, float delay = 0f)
    {
        if (delay > 0f)
        {
            _nextState = newState;
            Invoke(nameof(InvokeStateChange), delay);
        }
        else
        {
            _currentState = newState;
        }
    }
    private void InvokeStateChange()
    {
        _currentState = _nextState;
    }

    // ------------------- BLE 로직 -------------------

    private void StartScan()
    {
        _isScanning = true;
        LogMessage("[BLE] Starting scan...");

        // 서비스 UUID가 필요하면 배열로 넣고, 아니면 null
        string[] serviceUUIDs = { ServiceUUID.ToLower() };

        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(
            serviceUUIDs,
            (deviceAddress, deviceName) => {
                // 간단 콜백
                LogMessage($"[BLE] Discovered: name={deviceName}, addr={deviceAddress}");
            },
            (deviceAddress, deviceName, rssi, advData) => {
                // 확장 콜백
                if (!string.IsNullOrEmpty(deviceName) && deviceName.Contains(TargetDeviceName))
                {
                    LogMessage($"[BLE] Found target device: {deviceName} ({deviceAddress})");
                    BluetoothLEHardwareInterface.StopScan();
                    _isScanning = false;

                    _connectedDeviceAddress = deviceAddress;
                    SetState(States.Connect, 0.5f);
                }
            },
            rssiOnly: false
        );
    }

    private void ConnectToDevice(string deviceAddress)
    {
        if (string.IsNullOrEmpty(deviceAddress))
        {
            LogError("[BLE] ConnectToDevice failed: deviceAddress is null.");
            return;
        }

        LogMessage("[BLE] Connecting to " + deviceAddress);
        BluetoothLEHardwareInterface.ConnectToPeripheral(
            deviceAddress,
            (addr) => {
                // 연결 성공
                LogMessage("[BLE] Connected to " + addr);
                _isConnected = true;
            },
            (addr, serviceUUID) => {
                LogMessage("[BLE] Discovered Service: " + serviceUUID);
            },
            (addr, serviceUUID, characteristicUUID) => {
                LogMessage($"[BLE] Discovered Characteristic: {characteristicUUID}");

                if (IsSameUUID(characteristicUUID, CharacteristicUUID))
                {
                    LogMessage("[BLE] Target Characteristic found. Will subscribe...");
                    SetState(States.Subscribe, 0.5f);
                }
            },
            (addr) => {
                LogWarning("[BLE] Disconnected from " + addr);
                _isConnected = false;
                _connectedDeviceAddress = null;
            }
        );
    }

    private void SubscribeToCharacteristic(string deviceAddress, string serviceUUID, string characteristicUUID)
    {
        LogMessage("[BLE] Subscribing to characteristic...");
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(
            deviceAddress,
            FullUUID(serviceUUID),
            FullUUID(characteristicUUID),
            (notifAddr, notifChar) => {
                // Notify on/off
                LogMessage($"[BLE] Notification state changed - device={notifAddr}, char={notifChar}");
            },
            (notifAddr, notifChar, data) => {
                // 실제 Notify 수신
                LogMessage($"[BLE] Notify from {notifChar}: {data.Length} bytes");
                string text = Encoding.UTF8.GetString(data);
                LogMessage("[BLE] Received text: " + text);

                // 여기서 받은 데이터 처리
            }
        );

        SetState(States.Subscribed, 1.0f);
    }

    public void WriteSomeData(string message)
    {
        if (!_isConnected || string.IsNullOrEmpty(_connectedDeviceAddress)) return;

        byte[] dataToSend = Encoding.UTF8.GetBytes(message);
        BluetoothLEHardwareInterface.WriteCharacteristic(
            _connectedDeviceAddress,
            FullUUID(ServiceUUID),
            FullUUID(CharacteristicUUID),
            dataToSend,
            dataToSend.Length,
            withResponse: true,
            (writeChar) => {
                LogMessage("[BLE] Write completed: " + writeChar);
            }
        );
    }

    // --- 유틸 함수 ---

    private string FullUUID(string uuid)
    {
        // 이미 128bit 문자열이라고 가정하고, 소문자로만 맞춤
        return uuid.ToLower();
    }

    private bool IsSameUUID(string uuidA, string uuidB)
    {
        return FullUUID(uuidA).Equals(FullUUID(uuidB));
    }

    // --- 커스텀 로깅: TextMesh & Console ---
    private void LogMessage(string message)
    {
        Debug.Log(message);
        AppendTextMesh(message);
    }

    private void LogWarning(string message)
    {
        Debug.LogWarning(message);
        AppendTextMesh("<color=yellow>" + message + "</color>");
    }

    private void LogError(string message)
    {
        Debug.LogError(message);
        AppendTextMesh("<color=red>" + message + "</color>");
    }

    private void AppendTextMesh(string msg)
    {
        if (logTextMesh != null)
        {
            logTextMesh.text += msg + "\n";
        }
    }
}
