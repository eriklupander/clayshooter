using UnityEngine;
using System.Collections;

public class ZombieStartScript : MonoBehaviour {

	private Animator animator;
	//public GameObject myZombie;
	private GameObject player;
	public GameObject walkerRagdollPrefab;
	public ParticleSystem bloodPs; 

	private bool dead = false;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		animator.SetBool ("walk", true);
		animator.SetBool ("attack", false);
		animator.SetBool ("hit", false);
		player = GameObject.FindGameObjectWithTag ("MainCamera");
		// bloodPs = (ParticleSystem) GameObject.FindGameObjectWithTag ("BloodPs");
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			return;
		}
		Vector3 mypos = player.transform.position;
		if (Vector3.Distance (player.transform.position, transform.position) < 20f) {
			animator.SetBool ("walk", false);
			animator.SetBool ("attack", true);
			print ("Attack1");
		} else {
			animator.SetBool ("attack", false);
			// If not within attacking range, we should walk?
			if (animator.GetBool("hit")) {
				animator.SetBool ("hit", false);
				// Do nothing
			} else {
				animator.SetBool ("walk", true);
			}


			if (animator.GetBool("walk") ==  true) {

				Vector3 lookAt = new Vector3 (mypos.x, transform.position.y, mypos.z);
				transform.LookAt(lookAt);
				transform.position += transform.forward * Time.deltaTime * .4f;
			}
		}



	}

	void OnCollisionEnter(Collision collision) {
		ParticleSystem hitPs = (ParticleSystem)Instantiate (bloodPs, collision.collider.transform.position, Quaternion.identity);
		hitPs.Play ();
		Destroy (hitPs, hitPs.duration);

		if (collision.collider.CompareTag ("TempBullet") && !dead) {
			animator.SetBool ("hit", true);
			animator.SetBool ("walk", false);
			animator.SetBool ("attack", false);
			dead = true;
			GameObject zombie = (GameObject) Instantiate (walkerRagdollPrefab, transform.position, transform.rotation);

			//Vector3 direction = zombie.transform.forward;

			//zombie.GetComponent<Rigidbody>().AddForce (-direction.normalized*500f, ForceMode.Impulse);
			Destroy (this.gameObject);



		}
	}
}
