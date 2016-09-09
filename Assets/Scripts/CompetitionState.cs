using UnityEngine;
using System.Collections;

public class CompetitionState : MonoBehaviour {

	private static int currentStage = 0;
	private static int totalStages = 2;

	public static CompetitionStage stage;

	// Use this for initialization
	void Start () {
		stage = gameObject.AddComponent<CompetitionStage>() as CompetitionStage;
	}
	
	// Update is called once per frame
	void Update () {
		#if !MOBILE_INPUT
		if (Input.GetKeyUp(KeyCode.Backspace))
		{
			print("Beginning stage");
			stage.BeginStage();
		}
		#endif
	}
}
