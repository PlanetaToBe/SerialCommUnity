/**
 * SerialCommUnity (Serial Communication for Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;

/**
 * Sample for reading using polling by yourself, and writing too.
 */
public class SampleUserPolling_ReadWrite : MonoBehaviour
{
    public SerialController serialController;
	private SteamVR_TrackedObject trackedObj;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private bool isRunning;

    // Initialization
    void Start()
    {
		Debug.Log("Sup");
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		//		SteamVR_TrackedObject myObj = GetComponent();
	}

    // Executed each frame
    void Update()
    {
        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------

        // If you press one of these keys send it to the serial device. A
        // sample serial device that accepts this input is given in the README.

		SteamVR_Controller.Device myController = SteamVR_Controller.Input((int) trackedObj.index);

		bool triggerButtonDown;
		triggerButtonDown = myController.GetPressDown (triggerButton);

		if (triggerButtonDown && isRunning) {
			serialController.SendSerialMessage("Z");
			isRunning = false;
		} else if (triggerButtonDown && !isRunning) {
			serialController.SendSerialMessage("1");
			isRunning = true;
		}

		if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Sending 1");
            serialController.SendSerialMessage("1");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Sending Z");
            serialController.SendSerialMessage("Z");
        }


        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
		if (ReferenceEquals (message, SerialController.SERIAL_DEVICE_CONNECTED))
			Debug.Log ("Connection established, port: "+ serialController.portName);
		else if (ReferenceEquals (message, SerialController.SERIAL_DEVICE_DISCONNECTED)) {
			Debug.Log ("Connection attempt failed or disconnection detected, port: "+ serialController.portName);
		} else {
			Debug.Log ("Message arrived: " + message);
		}
    }
}
