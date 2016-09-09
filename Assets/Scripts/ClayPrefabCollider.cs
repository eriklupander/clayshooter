using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClayPrefabCollider : MonoBehaviour {

	public ParticleSystem prefabPs;
	public GameObject clayFragmentPrefab;
	public ParticleSystem clayHitPartPrefab;

	public float radius = 2.0F;
	public float power = 1500.0F;

	private float projectileSpeed = 325f;

	//private GameObject leadLine;
	private GameObject inGameCanvas;
	private Image leadImage;



	void Start() {
		if (Settings.easyMode) {
			inGameCanvas = (GameObject)GameObject.FindWithTag ("InGameCanvas");	
			if (ClayShooter.currentLeadImageIndex >= ClayShooter.leadImageCount - 1) {
				ClayShooter.currentLeadImageIndex = 0;
			} else {
				ClayShooter.currentLeadImageIndex++;
			}

			leadImage = inGameCanvas.GetComponentsInChildren<Image> ()[ClayShooter.currentLeadImageIndex];
			leadImage.enabled = true;
		}
	}

	void FixedUpdate() {
		if (Settings.easyMode && leadImage != null) {

			Vector2 screenPos = RectTransformUtility.WorldToScreenPoint (Camera.main, CalculateLead());
			leadImage.transform.position = screenPos;
			//DrawLine (transform.position, lead.gameObject.transform.position, Color.grey);
		}
	}

	/*
	void DrawLine(Vector3 start, Vector3 end, Color color) {

		
		leadLine.transform.position = start;
		leadLine.AddComponent<LineRenderer>();
		LineRenderer lr = leadLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(color, color);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);

	}
	*/

	// Borrowed from http://www.tosos.com/pages/calculating-a-lead-on-a-target/

	private Vector3 CalculateLead () {
		Vector3 V = transform.GetComponent<Rigidbody> ().velocity;
		Vector3 D = transform.position - Camera.main.transform.position;
		float A = V.sqrMagnitude - projectileSpeed * projectileSpeed;
		float B = 2 * Vector3.Dot (D, V);
		float C = D.sqrMagnitude;
		if (A >= 0) {
			Debug.LogError ("No solution exists");
			return transform.position;
		} else {
			float rt = Mathf.Sqrt (B*B - 4*A*C);
			float dt1 = (-B + rt) / (2 * A);
			float dt2 = (-B - rt) / (2 * A);
			float dt = (dt1 < 0 ? dt2 : dt1);
			return transform.position + V * dt;
		}
	}

	private bool collidedWithGround = false;

	void OnCollisionEnter(Collision collision) {
		
		// print("Distance to shooter: " + dist);
		if (collision.collider.CompareTag ("TempBullet")) {
			float dist = Vector3.Distance(collision.collider.transform.position, Camera.main.transform.position);
			float timeSinceFire = Time.realtimeSinceStartup - collision.collider.GetComponent<TempBulletColliderController>().fireTime;
			// print ("Bullet spent " + timeSinceFire + " seconds in air. That translates to a shell velocity of: " + dist / timeSinceFire);
			HitInfo hit = new HitInfo(timeSinceFire, dist, collision.collider.GetComponent<Rigidbody>().velocity.magnitude);
			CompetitionState.stage.RegisterHit (hit);

			ParticleSystem hitPs = (ParticleSystem)Instantiate (clayHitPartPrefab, transform.position, Quaternion.identity);
			hitPs.GetComponent<Rigidbody> ().velocity = this.GetComponent<Rigidbody> ().velocity;

			hitPs.Play ();
			Destroy (hitPs, hitPs.duration);
			for (var a = 0; a < Random.Range(10,30); a++) {
				Vector3 offset = Random.insideUnitSphere;
				offset = offset / 10;

				GameObject fragment1 = (GameObject)Instantiate (clayFragmentPrefab, transform.position + offset, Quaternion.identity);
				fragment1.GetComponent<Rigidbody> ().drag = Random.Range (1.0f, 2.0f);
				fragment1.GetComponent<Rigidbody> ().MoveRotation (Random.rotation);
				fragment1.GetComponent<Rigidbody> ().velocity = this.GetComponent<Rigidbody> ().velocity;
				fragment1.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius, 0.0F);

				Destroy (fragment1, 8);
			}
			Destroy (this.gameObject);
			if (leadImage != null) {
				leadImage.enabled = false;
			}

		}

		if (collision.collider.CompareTag ("Ground")) {
			if (!collidedWithGround) {
				CompetitionState.stage.RegisterMiss ();
			}
			collidedWithGround = true;
			ParticleSystem ps = (ParticleSystem)Instantiate (prefabPs, transform.position, Quaternion.identity);

			ps.Play ();
			Destroy (ps.gameObject, ps.duration);
			if (leadImage != null) {
				leadImage.enabled = false;
			}


		}
	}
}
