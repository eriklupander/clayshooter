using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompetitionStage : MonoBehaviour {

	private Text stageText;

	private volatile bool started = false;
	private volatile int currentClay = 0;
	private int totalClays = 25;
	private int shotsFired = 0;
	private int currentHits = 0;
	private int currentMisses = 0;
	private ArrayList hitData = new ArrayList ();

	private volatile bool clayInFlight = false;
	private volatile float nextFireTime = -1.0f;

	private ClayLauncherController clayLauncherController;

	// Use this for initialization
	void Start () {
		clayLauncherController = GameObject.FindWithTag ("ClayThrower").GetComponent<ClayLauncherController>();
		GameObject go = GameObject.FindWithTag ("StageText"); 
		stageText = go.GetComponent<Text> ();
		stageText.text = "Clay: " + (currentClay) + "/" + totalClays + " -- Hit: " + currentHits + "/" + currentClay;
	}

	public void BeginStage() {
		currentClay = 0;
		currentHits = 0;
		shotsFired = 0;
		hitData.Clear ();
		started = true;
		stageText.text = "Starting new round!";
		clayInFlight = false;
		nextFireTime = Time.realtimeSinceStartup + Random.Range (1.0f, 2.0f);
	}

	public void RegisterHit(HitInfo hitInfo) {
		hitData.Add (hitInfo);
		currentHits++;
		clayInFlight = false;
		nextFireTime = Time.realtimeSinceStartup + Random.Range (1.0f, 2.0f);
		stageText.text = "Clay: " + (currentClay) + "/" + totalClays + " -- Hit: " + currentHits + "/" + currentClay;
	}

	public void RegisterMiss() {
		currentMisses++;
		clayInFlight = false;
		nextFireTime = Time.realtimeSinceStartup + Random.Range (1.0f, 2.0f);
		stageText.text = "Clay: " + (currentClay) + "/" + totalClays + " -- Hit: " + currentHits + "/" + currentClay;
	}

	void Update() {
		if (started) {
			if (currentClay >= totalClays) {
				FinishStage ();
				return;
			}

			if (clayInFlight || nextFireTime == 0.0f) {
				// Do nothing
				return;
			}

			// If a launch is scheduled
			if (nextFireTime > 0f) {
				if (nextFireTime < Time.realtimeSinceStartup) {
					
					currentClay++;

					nextFireTime = -1.0f;
					clayInFlight = true;
					clayLauncherController.FireClayPiegon ();
				}
			}


		}
	}

	private void FinishStage() {
		// print ("Finished stage...");
		started = false;
		// TODO present results
	}

}
