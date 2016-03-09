using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

	public Canvas optionsMenu;

	public Toggle easyModeToggle;
	public Toggle tracersToggle;

	public static bool easyMode = false;
	public static bool tracersEnabled = false;

	void Start() {
		easyModeToggle.onValueChanged.AddListener( (val) => OnDifficultySelected(val));
		tracersToggle.onValueChanged.AddListener( (val) => OnTracersSelected(val));
		UpdateSettingsState ();
	}

	public void UpdateSettingsState() {
		easyModeToggle.isOn = easyMode;
		tracersToggle.isOn = tracersEnabled;
	}

	public void OnDifficultySelected(bool value) {
		print ("OnDifficultySelected: " + value);
		Settings.easyMode = value;
	}
	public void OnTracersSelected(bool value) {
		print ("OnTracersSelected: " + value);
		Settings.tracersEnabled = value;
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
