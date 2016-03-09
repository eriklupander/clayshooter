using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class UnderWaterController : MonoBehaviour {

	//public GameObject underwaterImageEffect;

	private bool active = false;
	// Use this for initialization
	void Start () {
		GetComponent<Blur> ().enabled = false;
		GameObject.FindWithTag ("UnderwaterTag").gameObject.GetComponent<Renderer>().enabled = false;
		active = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		float y = transform.position.y;
		if (y < 4.9f && !active) {
			GetComponent<Blur> ().enabled = true;
			GameObject.FindWithTag ("UnderwaterTag").gameObject.GetComponent<Renderer>().enabled = true;
			// underwaterImageEffect.enabled = true;
			print ("Activating");
			active = true;
		} else if (y >= 4.9f && active) {
			GetComponent<Blur> ().enabled = false;
			GameObject.FindWithTag ("UnderwaterTag").gameObject.GetComponent<Renderer>().enabled = false;
			// underwaterImageEffect.enabled = false;
			print ("De-activating");
			active = false;
		} 
	}
}
