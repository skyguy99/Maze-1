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

	// Turn on or off VR mode
	void ActivateVRMode(bool goToVR) {
		foreach (GameObject cardboardThing in cardboardObjects) {
			cardboardThing.SetActive(goToVR);
		}
		foreach (GameObject monoThing in monoObjects) {
			monoThing.SetActive(!goToVR);
		}
		Cardboard.SDK.VRModeEnabled = goToVR;

		// Tell the game over screen to redisplay itself if necessary
		//gameObject.GetComponent<GameController>().RefreshGameOver();
	}
	void ActivateVRMode2(bool goToVR) {
		foreach (GameObject cardboardThing in cardboardObjects2) {
			cardboardThing.SetActive(goToVR);
		}
		foreach (GameObject monoThing in monoObjects2) {
			monoThing.SetActive(!goToVR);
		}
		Cardboard.SDK.VRModeEnabled = goToVR;

		// Tell the game over screen to redisplay itself if necessary
		//gameObject.GetComponent<GameController>().RefreshGameOver();
	}

	public void Switch() {
		ActivateVRMode(false);
		switched = true;
		Debug.Log (switched+"No VR!");

		trail.GetComponent<Renderer>().material = trailColor;
	}
	public void Switch2() {
		ActivateVRMode2(false);
		switched2 = true;
		Debug.Log (switched+"No VR!");

		trail.GetComponent<Renderer>().material = trailColor;
	}
	IEnumerator Switchback() 
	{

		//yield return new WaitForSeconds(3); 
		advance.text = "Advance to next round in 3...";    
		yield return new WaitForSeconds(1);  

		advance.text = "Advance to next round in 2...";    
		yield return new WaitForSeconds(1);  

		advance.text = "Advance to next round in 1...";
		yield return new WaitForSeconds(1);
			ActivateVRMode(true);
			switched = false;
		trail.GetComponent<Renderer>().material = trailInvisible;

		//okBtn.gameObject.SetActive (false);
		//okBtn.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
		//okBtn.GetComponentInChildren<Text>().color = Color.clear;


			Debug.Log (switched+"VR!");
		//round1.enabled = false;
	}
	IEnumerator Switchback2() 
	{

		yield return new WaitForSeconds(3); 

		ActivateVRMode2(true);
		switched2 = false;
		trail.GetComponent<Renderer>().material = trailInvisible;

		//okBtn.gameObject.SetActive (false);
		//okBtn.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
		//okBtn.GetComponentInChildren<Text>().color = Color.clear;


		Debug.Log (switched+"VR 2!");
		//round1.enabled = false;
	}

	void Update () {

		if (Input.GetKey(KeyCode.Escape) || Cardboard.SDK.Tilted) {
			//Debug.Log ("Show map");
			Switch2 ();
			Debug.Log ("help em");

		}

		Cardboard.SDK.OnBackButton += ()=>{
			Debug.Log("WORKING"); 
			Switch2 ();
		};

		if (switched == true) {
			//round1.enabled = false;
			StartCoroutine(Switchback ());
		}
		if (switched2 == true) {
			//round1.enabled = false;
			StartCoroutine(Switchback2 ());
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
