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
public class SampleUserPolling_ReadWrite_TearDown : MonoBehaviour
{
    public SerialController serialController;

    // Initialization
    void Start()
    {
        serialController = GetComponent<SerialController>();
    }

	void Update() {
		string message = serialController.ReadSerialMessage();

		if (message == null)
			return;

		string[] lines = message.Split ('\n');
		string workingLine = lines [lines.Length - 1];
		float currentPosition = float.Parse (workingLine);
		currentPosition = ((currentPosition / 100.0f) - 0.5f) * .1f;

		// Check if the message is plain data or a connect/disconnect event.
		if (ReferenceEquals (message, SerialController.SERIAL_DEVICE_CONNECTED))
			Debug.Log ("Connection established, port: "+ serialController.portName);
		else if (ReferenceEquals (message, SerialController.SERIAL_DEVICE_DISCONNECTED)) {
			Debug.Log ("Connection attempt failed or disconnection detected, port: "+ serialController.portName);
		} else {
			Debug.Log ("Message arrived: " + message);
				transform.position = transform.position + new Vector3 (currentPosition, 0, 0);
			Debug.Log (currentPosition);
		}
	}
		
	void OnTriggerEnter(Collider other) {
		Debug.Log ("On");
		serialController.SendSerialMessage("1");
	}

	void OnTriggerExit(Collider other) {
		Debug.Log ("Off");
		serialController.SendSerialMessage("Z");
	}
}
