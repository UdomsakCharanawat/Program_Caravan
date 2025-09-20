using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArduinoBluetoothAPI;
using System;

public class connectBLE : MonoBehaviour
{
    public Canvas bleCanvas;

    //control bluetooth
    public BluetoothHelper bluetoothHelper;
    public string deviceName;
    public Text text;
    public string received_message;
    public RawImage statusBLE;
    public bool statusBLEconnect, disableCanvas, clicked;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        bleCanvas.enabled = true;
        StartCoroutine(waitConnectBLE());
        bleCanvas.worldCamera = Camera.main;
        StartCoroutine(inputData());
    }

    public void Update()
    {
        try
        {
            if (bluetoothHelper.isConnected() && !statusBLEconnect)
            {
                StartCoroutine(disableBLEcanvas());
                disableCanvas = true;
                statusBLEconnect = true;
            }
            else if (!bluetoothHelper.isConnected() && statusBLEconnect)
            {
                StartCoroutine(disableBLEcanvas());
                disableCanvas = false;
                statusBLEconnect = false;
            }
        }
        catch
        {
            // Debug.Log("not found ble");
        }

    }

    IEnumerator inputData()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            bluetoothHelper.SendData("r");
        }
        
    }

    IEnumerator disableBLEcanvas()
    {
        yield return new WaitForSeconds(1);

        if (disableCanvas) bleCanvas.enabled = false;
        if (!disableCanvas) bleCanvas.enabled = true;

    }

    IEnumerator waitConnectBLE()
    {
        yield return new WaitForSeconds(1);
        settingBLE();
        yield return new WaitForSeconds(10f);
        reconnect();
    }

    public void settingBLE()
    {
        try
        {
			bluetoothHelper = BluetoothHelper.GetInstance(deviceName);
			bluetoothHelper.OnConnected += OnConnected;
			bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
			bluetoothHelper.OnDataReceived += OnMessageReceived; //read the data

			bluetoothHelper.setTerminatorBasedStream("\n"); //delimits received messages based on \n char

			LinkedList<BluetoothDevice> ds = bluetoothHelper.getPairedDevicesList();

			foreach (BluetoothDevice d in ds)
			{
				Debug.Log($"{d.DeviceName} {d.DeviceAddress}");
			}
            text.text = bluetoothHelper.isDevicePaired().ToString();

        }
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}

    void OnMessageReceived(BluetoothHelper helper)
    {
        received_message = helper.Read();
        Debug.Log(received_message);
    }

    void OnConnected(BluetoothHelper helper)
    {
        statusBLE.color = Color.green;
        text.text = "connected";
        clicked = true;

        try
        {
            helper.StartListening();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void OnConnectionFailed(BluetoothHelper helper)
    {
        statusBLEconnect = false;
        statusBLE.color = Color.red;
        text.text = "failed";
        Debug.Log("Connection Failed");
        clicked = false;
        StartCoroutine(clickReconnectBLE());
    }

    IEnumerator clickReconnectBLE()
    {
        while (!clicked)
        {
            yield return new WaitForSeconds(10f);
            reconnect();
        }
    }

    public void reconnect()
    {
        if (bluetoothHelper.isDevicePaired())
        {
            bluetoothHelper.Connect(); // tries to connect
            text.text = "connecting";
        }
    }

    public void disconnect()
    {
        bluetoothHelper.Disconnect();
        statusBLE.color = Color.blue;
        text.text = "disconnect";
    }

    /*void OnGUI()
    {
        if (bluetoothHelper != null)
            bluetoothHelper.DrawGUI();
        else
            return;

        if (!bluetoothHelper.isConnected())
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height / 10, Screen.width / 5, Screen.height / 10), "Connect"))
            {
                if (bluetoothHelper.isDevicePaired())
                {
                    bluetoothHelper.Connect(); // tries to connect
                    text.text = "reconnect" + bluetoothHelper.isConnected().ToString();
                }
                    
                //else
                    //sphere.GetComponent<Renderer>().material.color = Color.grey;
            }

        if (bluetoothHelper.isConnected())
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height - 2 * Screen.height / 10, Screen.width / 5, Screen.height / 10), "Disconnect"))
            {
                bluetoothHelper.Disconnect();
                statusBLE.color = Color.blue;
                text.text = "disconnect" + bluetoothHelper.isConnected().ToString();
                //sphere.GetComponent<Renderer>().material.color = Color.blue;
            }

        if (bluetoothHelper.isConnected())
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height / 10, Screen.width / 5, Screen.height / 10), "Send text"))
            {
                bluetoothHelper.SendData(new Byte[] { 0, 0, 85, 0, 85 });
                
                // bluetoothHelper.SendData("This is a very long long long long text");
            }
    }
    */
    void OnDestroy()
    {
        if (bluetoothHelper != null)
        {
            bluetoothHelper.Disconnect();
        }
    }
}
