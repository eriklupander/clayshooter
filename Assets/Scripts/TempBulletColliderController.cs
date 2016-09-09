using UnityEngine;
using System.Collections;

public class TempBulletColliderController : MonoBehaviour {

	public GameObject _hitPrefab;
	public float fireTime;
	// private bool collided = false;

	void Start() {
		fireTime = Time.realtimeSinceStartup;
	}

	// Update is called once per frame
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.name.Equals ("Terrain") || other.gameObject.name.Equals ("ClayLauncher") || other.gameObject.name.Equals ("Cylinder")) {
				

			foreach (ContactPoint _contactPoint in other.contacts) {
				SpawnHit(_contactPoint.point, Quaternion.FromToRotation(Vector3.forward, _contactPoint.normal));
			}
			if (Random.value > 0.5f) {
				Destroy (this.gameObject);	
			}
			//collided = true;
		}
	}

	private void SpawnHit(Vector3 position, Quaternion rotation)
	{
		GameObject.Instantiate (_hitPrefab, position, rotation);
	}
}
