using UnityEngine;
using System;
using System.Collections;

public class BLEConnection : MonoBehaviour
{
    private string targetDeviceName = "Jelly Broadcast Test";

    void Start()
    {
        // Initialize
        // asCentral = true, asPeripheral = false
        BluetoothLEHardwareInterface.Initialize(
            true, false,
            () => {
                Debug.Log("BLE Initialized");
                StartScan();
            },
            (error) => {
                Debug.LogError("BLE Init Error: " + error);
            }
        );
    }

    void StartScan()
    {
      
        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(
            null,
            (deviceAddress, deviceName) => {
                if (!string.IsNullOrEmpty(deviceName))
                {
                    Debug.Log($"[Scan] Found device: {deviceName} / Address: {deviceAddress}");
                    if (deviceName.Equals(targetDeviceName))
                    {
                        Debug.Log("Found Jelly device, connecting...");
                        BluetoothLEHardwareInterface.StopScan();
                        ConnectToDevice(deviceAddress);
                    }
                }
            },

            actionAdvertisingInfo: null,
            rssiOnly: false,
            clearPeripheralList: true
        );
    }

    void ConnectToDevice(string address)
    {
        BluetoothLEHardwareInterface.ConnectToPeripheral(
            address,
            // connectAction
            (connectedAddress) => {
                Debug.Log("Connected to " + connectedAddress);
            },
            // serviceAction
            (connectedAddress, serviceUUID) => {
                Debug.Log("Discovered service: " + serviceUUID);
            },
            // characteristicAction
            (connectedAddress, serviceUUID, characteristicUUID) => {
                Debug.Log("Discovered characteristic: " + characteristicUUID);
                string targetServiceUUID = serviceUUID;
                string targetCharacteristicUUID = characteristicUUID;
                    if(characteristicUUID == "deadbeef-dead-beef-dead-beefdeadbeef") // DO NOT Touch!
                    {
                        SubscribeCharacteristic(connectedAddress, serviceUUID, characteristicUUID);
                    }
            },
            // disconnectAction
            (disconnectedAddress) => {
                Debug.Log("Disconnected from " + disconnectedAddress);
            }
        );
    }


    void SubscribeCharacteristic(string deviceAddress, string serviceUUID, string characteristicUUID)
    {

        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(
            deviceAddress,
            serviceUUID,
            characteristicUUID,
            (notifyAddress, notifyCharacteristic) => {
                Debug.Log($"Notify registered.: {notifyCharacteristic}");
            },
            (updatedAddress, updatedCharacteristic, data) => {
       
        //Debug.Log($"updatedAddress: {updatedAddress}, updatedCharacteristic=({updatedCharacteristic}), gyro=({data})");
        
        if (data != null && data.Length >= 14)
        {
            ushort gesture = BitConverter.ToUInt16(data, 0);
            short accelX = BitConverter.ToInt16(data, 2);
            short accelY = BitConverter.ToInt16(data, 4);
            short accelZ = BitConverter.ToInt16(data, 6);
            short gyroX = BitConverter.ToInt16(data, 8);
            short gyroY = BitConverter.ToInt16(data, 10);
            short gyroZ = BitConverter.ToInt16(data, 12);

            Debug.Log($"gesture: {gesture}, accel=({accelX}, {accelY}, {accelZ}), gyro=({gyroX}, {gyroY}, {gyroZ})");
        }
        
        
        
            
        
    }
);

    }

}
