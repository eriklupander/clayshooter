using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public Canvas mainMenu;
	public Canvas optionsMenu;
	public Button exitButton;
	public Button optionsButton;
	public Toggle difficultyToggle;
	public Toggle tracersToggle;

	private bool optionsVisible = false;

	public void Start() {
		
		// TODO get state of checkboxes, assign to global vars for what they control.
		mainMenu = mainMenu.GetComponent<Canvas> ();
		optionsMenu = optionsMenu.GetComponent<Canvas> ();
		optionsButton = optionsButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();
		mainMenu.enabled = true;
		optionsMenu.enabled = false;

		difficultyToggle.onValueChanged.AddListener( (val) => OnDifficultySelected(val));
		tracersToggle.onValueChanged.AddListener( (val) => OnTracersSelected(val));

		difficultyToggle.isOn = Settings.easyMode;
		tracersToggle.isOn = Settings.tracersEnabled;
	}

	public void StartLevel() {
		SceneManager.LoadScene (1);
	}

	public void ExitGame() {
		Application.Quit ();
	}

	public void OnBackButtonPressed() {
		optionsVisible = false;

		optionsMenu.enabled =  optionsVisible;
		mainMenu.enabled = !optionsVisible;
	}

	public void OnOptionsPressed() {
		optionsVisible = !optionsVisible;
		optionsMenu.enabled =  optionsVisible;
		mainMenu.enabled = !optionsVisible;
	}

	public void OnDifficultySelected(bool value) {
		print ("OnDifficultySelected: " + value);
		Settings.easyMode = value;
	}
	public void OnTracersSelected(bool value) {
		print ("OnTracersSelected: " + value);
		Settings.tracersEnabled = value;
	}
}
