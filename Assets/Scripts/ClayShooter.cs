using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClayShooter : MonoBehaviour {

	public GameObject clayPrefab;

	private bool fireClay;
	private bool fireClayFromRandom;


	// Not really a setting, more of a global...
	public static int leadImageCount;
	public static int currentLeadImageIndex = 0;

	void Start() {
		GameObject inGameCanvas = (GameObject)GameObject.FindWithTag ("InGameCanvas");	
		leadImageCount = inGameCanvas.GetComponentsInChildren<Image> ().Length;
	}

	void FixedUpdate () {
		if (fireClay) {
			
			fireClay = false;
			GameObject clay = (GameObject) Instantiate (clayPrefab, transform.position, transform.rotation);
			clay.GetComponent<Rigidbody>().AddForce (clay.transform.forward * 50.0f, ForceMode.Impulse);

			Destroy (clay.gameObject, 10f); // destroy clay after 10 seconds
		}
		if (fireClayFromRandom) {
			fireClayFromRandom = false;

			Vector3 localOffset = new Vector3(35,0,10);
			Vector3 worldOffset = transform.rotation * localOffset;
			Vector3 spawnPosition = transform.position + worldOffset;
			GameObject clay = (GameObject) Instantiate (clayPrefab, spawnPosition, transform.rotation);

			Vector3 localTargetOffset = new Vector3(0,10,25);
			Vector3 worldTargetOffset = transform.rotation * localTargetOffset;
			Vector3 targetPosition = transform.position + worldTargetOffset;

			Vector3 direction = clay.transform.position - targetPosition; // against me?

			clay.GetComponent<Rigidbody>().AddForce (-direction.normalized*50f, ForceMode.Impulse);
			Destroy (clay.gameObject, 10f); // destroy clay after 10 seconds

		}
	}



	// Update is called once per frame
	void Update () {
		#if !MOBILE_INPUT
		if (Input.GetKeyUp(KeyCode.Tab))
		{
			fireClay = true;
		}
		if (Input.GetKeyUp(KeyCode.Q))
		{
			fireClayFromRandom = true;
		}
		/*
		if (Input.GetKeyDown(KeyCode.C)) {
			foreach (Camera c in Camera.allCameras) {
						
				if (c.name == "FirstPersonCharacter") {
					c.enabled = true;
				}
			}
			foreach (Camera c in Camera.allCameras) {
				if (c.name == "ActionCamera") {
					//c.enabled = false;
				}

			}
		}
		*/

		#endif
	}
}
