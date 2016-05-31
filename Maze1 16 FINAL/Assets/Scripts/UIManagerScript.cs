using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {

	public static int sceneNum = 0;
	public Canvas menuCanvas;

	public Canvas settingsCanvas;
	public static int roundNum = 3;
	public static bool markerOn = false;
	public static bool vrOff = false;


	public Button markerButton;
	public Text markerBtnText;
	//Image btnImage = markerButton.GetComponent<Image>();

	public static string markerTxt = "";

	void Start() {
		//RenderSettings.ambientLight = Color.white;

		vrOff = false;
		settingsCanvas.enabled = false;
		markerTxt = "Off";

	}

	public void StartGame() {
		
		//Make sure to port over all logic

			if (sceneNum == 0) {
				SceneManager.LoadScene ("1");
			} else if (sceneNum == 1) {
				SceneManager.LoadScene ("2");
			} else if (sceneNum == 2) {
				SceneManager.LoadScene ("3");
			}
	}
	public void goToSettings() {
		
		menuCanvas.enabled = false;
		settingsCanvas.enabled = true;
		Debug.Log ("To settings!");

	}
	public void backToMenu() {

		menuCanvas.enabled = true;
		settingsCanvas.enabled = false;
	}

	public void rightClick() {

		if (sceneNum == 0) {
			sceneNum = 1;
			Debug.Log (sceneNum);

		} else if (sceneNum == 1) {
			sceneNum = 2;
			Debug.Log (sceneNum);
		}
		else if (sceneNum == 2) {
			sceneNum = 0;
			Debug.Log (sceneNum);
		}
	}

	public void leftClick() {
		if (sceneNum == 0) {
			sceneNum = 2;
			Debug.Log (sceneNum);
		} else if (sceneNum == 1) {
			sceneNum = 0;
			Debug.Log (sceneNum);
		}
		else if (sceneNum == 2) {
			sceneNum = 1;
			Debug.Log (sceneNum);
		}
	}

	public void leftClick2()
	{
		if (roundNum == 3) {
			roundNum = 2;
			Debug.Log (roundNum);
		} else if (roundNum == 2) {
			roundNum = 1;
			Debug.Log (roundNum);
		}
		else if (roundNum == 1) {
			roundNum = 5;
			Debug.Log (roundNum);
		}
		else if (roundNum == 5) {
			roundNum = 4;
			Debug.Log (roundNum);
		}
		else if (roundNum == 4) {
			roundNum = 3;
			Debug.Log (roundNum);
		}
	}
	public void rightClick2()
	{
		if (roundNum == 3) {
			roundNum = 4;
			Debug.Log (roundNum);
		} else if (roundNum == 2) {
			roundNum = 3;
			Debug.Log (roundNum);
		}
		else if (roundNum == 1) {
			roundNum = 2;
			Debug.Log (roundNum);
		}
		else if (roundNum == 5) {
			roundNum = 1;
			Debug.Log (roundNum);
		}
		else if (roundNum == 4) {
			roundNum = 5;
			Debug.Log (roundNum);
		}
	}
	public void markerClick()
	{
		if (markerOn) {
			
			//btnImage.sprite = null;
			markerOn = false;
			markerTxt = "Off";
			markerBtnText.text = "Markers disabled";
		} 
		else {
			markerOn = true;
			markerTxt = "On";
			markerBtnText.text = "Markers enabled";
		}
	}


	void Update()
	{
		Debug.Log (UIManagerScript.vrOff);

		if(vrOff)
			Cardboard.SDK.VRModeEnabled = false;
	}
}
