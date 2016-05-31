using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ViewSwap : MonoBehaviour {
	 
	public static bool enableOkBtn = false;

	public GameObject[] cardboardObjects;
	public GameObject[] monoObjects;
	public bool switched = false;

	public GameObject[] cardboardObjects2;
	public GameObject[] monoObjects2;
	public bool switched2 = false;

	public Text advance;
	//public Button okBtn;

	//For trail renderer
	public TrailRenderer trail;
	public Material trailColor;
	public Material trailInvisible;

	//Lighting
	public Color pauseColor;
	public Color pauseColor2;

	//Map button
	public Button mazeButton;
	public Sprite unavailable;
	public Sprite uHaveCreds;

	// Turn on or off VR mode
	void ActivateVRMode(bool goToVR) {
		foreach (GameObject cardboardThing in cardboardObjects) {
			cardboardThing.SetActive(goToVR);
		}
		foreach (GameObject monoThing in monoObjects) {
			monoThing.SetActive(!goToVR);
		}
		if (UIManagerScript.vrOff) {
			Cardboard.SDK.VRModeEnabled = false;
		} else {
			Cardboard.SDK.VRModeEnabled = goToVR;
		}

		// Tell the game over screen to redisplay itself if necessary
		//gameObject.GetComponent<GameController>().RefreshGameOver();
	}
		

	public void Switch() {


		if (GameLogic.mazeCreds > 0) {
			if (GameLogic.gameStarted) {
				GameLogic.runLighting = false;
				RenderSettings.ambientLight = pauseColor;
				RenderSettings.ambientGroundColor = pauseColor2;


				ActivateVRMode (false);
				switched = true;
				Debug.Log (switched + "No VR!");

				trail.GetComponent<Renderer> ().material = trailColor;
				GameLogic.mazeCreds--; //decrease maze creds
			}
		}
	}
	IEnumerator Switchback() 
	{

		//yield return new WaitForSeconds(3); 
		advance.text = "Back to game in 3...";    
		yield return new WaitForSeconds(1);  

		advance.text = "Back to game in 2...";    
		yield return new WaitForSeconds(1);  

		advance.text = "Back to game in 1...";
		yield return new WaitForSeconds(1);
			ActivateVRMode(true);
			switched = false;
		trail.GetComponent<Renderer>().material = trailInvisible;
		GameLogic.runLighting = true;

		//okBtn.gameObject.SetActive (false);
		//okBtn.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
		//okBtn.GetComponentInChildren<Text>().color = Color.clear;


			Debug.Log (switched+"VR!");
		//round1.enabled = false;
	}

	public void mapClicked()
	{
		if (GameLogic.mazeCreds > 0) {
			Debug.Log ("They can click!");
			mazeButton.image.sprite = uHaveCreds;
		}
	}
	public void mapChangeBack()
	{
		mazeButton.image.sprite = unavailable;
	}

	void Update () {


		if (switched == true) {
			//round1.enabled = false;
			StartCoroutine(Switchback ());
		}


		//Debug.Log ("updating");

	}

	void Start() {

		//round1.enabled = true;
		//round1.enabled = false;

		trail.GetComponent<Renderer>().material = trailInvisible;
		ActivateVRMode(true);
	}
}
