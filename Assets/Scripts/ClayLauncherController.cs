using UnityEngine;
using System.Collections;

public class ClayLauncherController : MonoBehaviour {

	public GameObject clayPrefab;

	private bool fireClayFromRandom;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (fireClayFromRandom) {
			fireClayFromRandom = false;

			Vector3 localOffset = new Vector3(0,0,1); // right in front of launcher
			Vector3 worldOffset = transform.rotation * localOffset;
			Vector3 spawnPosition = transform.position + worldOffset;
			GameObject clay = (GameObject) Instantiate (clayPrefab, spawnPosition, transform.rotation);

			// Launch angle, both horizontal and vertical should be randomized.
			float x = Random.Range(-45,45);
			float y = Random.Range (30, 50);
			Vector3 localTargetOffset = new Vector3(x, y, 50);
			Vector3 worldTargetOffset = transform.rotation * localTargetOffset;
			Vector3 targetPosition = transform.position + worldTargetOffset;

			Vector3 direction = clay.transform.position - targetPosition; 

			clay.GetComponent<Rigidbody>().AddForce (-direction.normalized*50f, ForceMode.Impulse);
			Destroy (clay.gameObject, 10f); // destroy clay after 10 seconds

		}
	}

	void Update () {
		#if !MOBILE_INPUT

		if (Input.GetKeyUp(KeyCode.E))
		{
			fireClayFromRandom = true;
		}
		#endif
	}
}
