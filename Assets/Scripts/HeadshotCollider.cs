using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeadshotCollider : MonoBehaviour {

	private int i = 0;

	void OnCollisionEnter(Collision collision) {
		print("Enter head!!");
		// print("Distance to shooter: " + dist);
		if (collision.collider.CompareTag ("TempBullet")) {
			print("HEADSHOT " + i++);
		}
	}
}

