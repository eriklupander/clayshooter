using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ShotgunController : MonoBehaviour {

	public GameObject shotgunPrefab;
	public GameObject bulletPrefab;
	public GameObject muzzleFlashPrefab;

	private GameObject myShotgun;

	private bool fire = false;
	private FirstPersonController fpc;

	// Use this for initialization
	void Start () {
		fpc = FindObjectOfType<FirstPersonController>();

		myShotgun = (GameObject) Instantiate (shotgunPrefab, transform.position, transform.rotation);
		myShotgun.transform.parent = transform;
		myShotgun.transform.localPosition = new Vector3 (0.3f, -0.3f, .8f);

		// Disable shadow casting for the shotgun since we have no player model that casts shadows.
		Renderer renderer = (Renderer) myShotgun.GetComponentsInChildren<Renderer> ().GetValue (0);
		renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

	}
	public Texture2D crosshairTexture;
	public float crosshairScale = 1;


	private float Stray = .05f;

	void FixedUpdate () {
		if (fire) {
			FireShotgun (); 
		}
	}

	private void FireShotgun ()
	{
		bulletPrefab.GetComponentInChildren<Renderer> ().enabled = Settings.tracersEnabled;
		AudioSource[] audios = myShotgun.GetComponents<AudioSource> ();
		foreach (AudioSource audio in audios) {
			audio.Play ();
		}
		Vector3 basePos = myShotgun.transform.position;
		basePos += new Vector3 (0f, 0.3f, 0f);
		Quaternion rot = transform.rotation;
		for (var a = 0; a < 20; a++) {
			var randomNumberX = Random.Range (-Stray, Stray);
			var randomNumberY = Random.Range (-Stray, Stray);
			var randomNumberZ = Random.Range (-Stray, Stray);

			// TODO consider using a pool instead.
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, basePos, rot);
			bullet.transform.Rotate (randomNumberX, randomNumberY, randomNumberZ);
			Vector3 fwd = transform.forward;
			bullet.GetComponent<Rigidbody> ().AddForce (fwd * 12.0f, ForceMode.Impulse);
			Destroy (bullet, 3.0f);
		}
		renderMuzzleFlash ();
		Animator animator = myShotgun.GetComponent<Animator> ();
		animator.SetBool ("Reload", true);
		fire = false;
	}

	private void renderMuzzleFlash ()
	{
		GameObject muzzleFlash = (GameObject)Instantiate (muzzleFlashPrefab);
		muzzleFlash.transform.parent = myShotgun.transform;
		muzzleFlash.transform.localPosition = new Vector3 (0.1f, 0.05f, 1.4f);
		ParticleSystem muzzlePs = muzzleFlash.GetComponent<ParticleSystem> ();
		muzzlePs.Play ();
	}


	// Update is called once per frame
	void Update () {
		#if !MOBILE_INPUT
		if (Input.GetKeyUp(KeyCode.Mouse0) && fpc.enabled == true)
		{
			fire = true;
		}
		if (Input.GetKeyUp(KeyCode.T))
		{
			Settings.tracersEnabled = !Settings.tracersEnabled;
			Settings settings = FindObjectOfType<Settings>();
			settings.UpdateSettingsState();
		}
		if (Input.GetKeyUp(KeyCode.Y))
		{
			Settings.easyMode = !Settings.easyMode;
			Settings settings = FindObjectOfType<Settings>();
			settings.UpdateSettingsState();
		}
		if (Input.GetKeyUp(KeyCode.Escape)) {
			OnEscapeKey ();
		}
		#endif
	}

	void OnEscapeKey ()
	{
		Settings settings = FindObjectOfType<Settings> ();
		settings.UpdateSettingsState ();
		fpc.enabled = !fpc.enabled;
		GameObject optionsMenu = GameObject.FindWithTag ("OptionsMenu");
		// Check if we should activate the options menu
		if (!fpc.enabled) {
			optionsMenu.GetComponent<Canvas> ().enabled = true;
		}
		else {
			// Deactivate it
			optionsMenu.GetComponent<Canvas> ().enabled = false;
		}
	}

	void OnGUI()
	{
		//if not paused
		if (Time.timeScale != 0)
		{
			if (crosshairTexture!=null)
				GUI.DrawTexture(new Rect((Screen.width - crosshairTexture.width*crosshairScale)/2 ,(Screen.height-crosshairTexture.height*crosshairScale)/2, crosshairTexture.width*crosshairScale, crosshairTexture.height*crosshairScale),crosshairTexture);
			else
				Debug.Log("No crosshair texture set in the Inspector");
		}
	}
}
