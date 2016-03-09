using UnityEngine;
using System.Collections;

public class WaterCollideController : MonoBehaviour {

	public ParticleSystem waterSpray;
	/*
	void OnTriggerEnter(Collider other) {
		if (!other.CompareTag ("TempBullet")) {
			return;
		}

		print ("Something splashed into our water!");
		ParticleSystem splashPs = (ParticleSystem) Instantiate (waterSpray, other.transform.position, Quaternion.identity);
		splashPs.Play ();
		Destroy (splashPs, splashPs.duration);
		Destroy (other.gameObject);
	}
	*/
}
