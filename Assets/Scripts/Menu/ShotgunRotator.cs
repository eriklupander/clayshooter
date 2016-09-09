using UnityEngine;
using System.Collections;

public class ShotgunRotator : MonoBehaviour {
	
	//  private GameObject shotgun;

	// Use this for initialization
	void Start () {
		// shotgun = GetComponent<> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3 (0f, 30f, 0f) * Time.deltaTime);
	}
}
