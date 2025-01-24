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
            },

            (disconnectedAddress) => {
                Debug.Log("Disconnected from " + disconnectedAddress);
            }
        );
    }
}
