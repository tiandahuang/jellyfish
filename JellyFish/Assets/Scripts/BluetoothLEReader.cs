using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BluetoothLEReader : MonoBehaviour
{

    private const string DeviceName = "Jelly Broadcast Test"; // device name
    private const string ServiceUUID = "deadbeef-dead-beef-dead-beefdeadbeef"; // UUID
    private bool isConnected = false;

    // Start is called before the first frame update
    void Start()
    {
        BluetoothLEHardwareInterface.Initialize(
            true, false,
            () => Debug.Log("Bluetooth Initialized"),
            error => Debug.LogError($"Bluetooth Initialization Error: {error}")
            );
        StartScanning();
    }

    private void StartScanning()
    {
        Debug.Log("Scanning for BLE devices...");

        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(
            null, // 특정 서비스 UUID 없이 모든 디바이스 검색
            (address, name) =>
            {
                Debug.Log($"Device Found: {name} ({address})");

                if (name == DeviceName)
                {
                    Debug.Log($"Target Device Found: {name}");
                    BluetoothLEHardwareInterface.StopScan();
                    ConnectToDevice(address);
                }
            },
            (address, name, rssi, advertisingInfo) => { /* 추가 정보 필요시 처리 */ }
        );
    }

    private void ConnectToDevice(string address)
    {
        Debug.Log($"Connecting to {address}...");

        BluetoothLEHardwareInterface.ConnectToPeripheral(
            address,
            connectAction: deviceName =>
            {
                Debug.Log($"Connected to {deviceName}");
                isConnected = true;
            },
            serviceAction: (deviceAddress, serviceUUID) =>
            {
                Debug.Log($"Service discovered: {serviceUUID}");
                if (serviceUUID.ToLower() == ServiceUUID.ToLower())
                {
                    // 서비스와 연결된 특성 탐색
                    DiscoverCharacteristics(deviceAddress, serviceUUID);
                }
            },
            characteristicAction: (deviceAddress, serviceUUID, characteristicUUID) =>
            {
                Debug.Log($"Characteristic discovered: {characteristicUUID}");
            },
            disconnectAction: deviceName =>
            {
                Debug.Log($"Disconnected from {deviceName}");
                isConnected = false;
            }
        );
    }

    private void DiscoverCharacteristics(string deviceAddress, string serviceUUID)
    {
        Debug.Log("Discovering characteristics...");
        BluetoothLEHardwareInterface.ReadCharacteristic(
            deviceAddress,
            serviceUUID,
            serviceUUID, // 여기서 특성과 서비스 UUID가 동일한 경우를 가정
            (characteristic, data) =>
            {
                string receivedData = System.Text.Encoding.UTF8.GetString(data);
                Debug.Log($"Data received from {characteristic}: {receivedData}");
            }
        );
    }

    private void OnApplicationQuit()
    {
        if (isConnected)
        {
            BluetoothLEHardwareInterface.DeInitialize(() =>
            {
                Debug.Log("Bluetooth Deinitialized");
            });
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
