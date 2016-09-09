using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class Settings : MonoBehaviour {

	// public Canvas optionsMenu;
	public Text statusText;
	public Toggle easyModeToggle;
	public Toggle tracersToggle;
	public Toggle postProcessingToggle;

	public static bool easyMode = false;
	public static bool tracersEnabled = false;
	public static bool postProcessingEnabled = false;

	void Start() {
		easyModeToggle.onValueChanged.AddListener( (val) => OnDifficultySelected(val));
		tracersToggle.onValueChanged.AddListener( (val) => OnTracersSelected(val));
		postProcessingToggle.onValueChanged.AddListener( (val) => OnPostProcessingSelected(val));
		UpdateSettingsState ();
	}

	public void UpdateSettingsState() {
		easyModeToggle.isOn = easyMode;
		tracersToggle.isOn = tracersEnabled;
		postProcessingToggle.isOn = postProcessingEnabled;
	}

	public void OnDifficultySelected(bool value) {
		print ("OnDifficultySelected: " + value);
		Settings.easyMode = value;
		statusText.text = "Easy mode " + (value ? "ON" : "OFF");
	}
	public void OnTracersSelected(bool value) {
		print ("OnTracersSelected: " + value);
		Settings.tracersEnabled = value;
		statusText.text = "Tracers " + (value ? "ON" : "OFF");
	}
	public void OnPostProcessingSelected(bool value) {
		print ("OnPostProcessingSelected: " + value);
		Settings.postProcessingEnabled = value;

		GameObject mc = GameObject.FindWithTag ("MainCamera");

		mc.GetComponent<Antialiasing> ().enabled = Settings.postProcessingEnabled;
		mc.GetComponent<Bloom> ().enabled = Settings.postProcessingEnabled;
		mc.GetComponent<VignetteAndChromaticAberration> ().enabled = Settings.postProcessingEnabled;
		mc.GetComponent<SunShafts> ().enabled = Settings.postProcessingEnabled;
		// mc.GetComponent<Blur> ().enabled = Settings.postProcessingEnabled;

		statusText.text = "Post-processing effects " + (value ? "ON" : "OFF");
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
