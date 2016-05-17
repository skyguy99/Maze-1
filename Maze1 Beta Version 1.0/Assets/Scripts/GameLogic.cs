using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class GameLogic : MonoBehaviour {

	//For rotation
	public GameObject world;
	private Vector3 headTemp = new Vector2(0.0f, 0.0f);

	private float headX;

	bool roundCanvasUp = false;
	public static bool roundWasReset = false;

	//public Canvas cubeCanvas;
	public static bool gameStarted = false;
	public static bool gameOver = false;

	public Camera cam1;
	public Camera cam2;

	//On eye canvas
	public Canvas eyeCanvas;
	public Image cubeImage;
	public Sprite img2;
	public Text timeEyeText;
	public Text timesFound;

	public static int score = 0;
	public static int currentRound = 1;

	public static float timer;
	string minutes = Mathf.Floor(timer / 60).ToString("00");
	string seconds = (timer % 60).ToString("00");
	private bool timerOn = false;

	//For round over move
	public Camera mainCamera;
	private Vector3 temp = new Vector3(6.2f,1.7f,-3.9f);
	private Quaternion rotation = Quaternion.Euler(0, 0, 0);

	//For round over canvas
	public Rigidbody m_RigidBody;
	public Canvas roundOver;
	public Text roundText;
	public Text timerText;
	float ctx;
	float ctz;

	//For start canvas 
	public Canvas startCanvas;
	public Text countdown; 

	//For mission accomplish/game over canvas
	public Canvas endCanvas;
	public Text results;
	public Image completes;
	public Image completes2;

	public Text resultsRounds;
	string body = "hi";
	string body2 = "hi";

	//round completes
	public static bool r1Complete = false;
	public static bool r2Complete = false;
	public static bool r3Complete = false;
	public static bool r4Complete = false;
	public static bool r5Complete = false;

	public Sprite r1Comp;
	public Sprite r1InComp;
	public Sprite r2Comp;
	public Sprite r2InComp;
	public Sprite r3Comp;
	public Sprite r3InComp;
	public Sprite r4Comp;
	public Sprite r4InComp;
	public Sprite r5Comp;
	public Sprite r5InComp;

	//For bottom canvases
	public Canvas giveUp;
	public Canvas menu; 

	//For giving up
	public Canvas terminate;
	public Text results2;
	public Text roundsTxt2;
	public Text giveUpResults;
	//public Text roundsTxt2;

	public CardboardHead head1;
	public GameObject main;
	Quaternion headRot;
	Quaternion test1 = new Quaternion(0.0f,0.0f,0.0f,0.0f);

	// Use this for initialization
	void Start () {

		roundCanvasUp = false;

		//headRot = head1.transform.localRotation;
		//completes.transform.localPosition = new Vector2();
		completes.transform.localPosition = new Vector3(-70.3f,-72.0f,0.0f);

		gameOver = false;
		currentRound = 1;
		gameStarted = false;
		score = 0;

		terminate.enabled = false;
		giveUp.enabled = false;
		endCanvas.enabled = false;
		eyeCanvas.enabled = false;
		timesFound.text = score.ToString();
		roundText.text = "ROUND "+currentRound+" COMPLETED";
		timerText.text = minutes + ":" + seconds;
		timeEyeText.text = minutes + ":" + seconds;

		countdown.text = "3";

		roundOver.enabled = false;
		eyeCanvas.enabled = false; 

		move.moveEnabled = false;
		StartCoroutine (startUp ());

		r1Complete = false;
		r2Complete = false;
		r3Complete = false;
		r4Complete = false;
		r5Complete = false;



	} //--------------------------------------------------------------------------------------------------------------------

	public void VRToggle()
	{
		Debug.Log ("Switch!!");
		if (!Cardboard.SDK.VRModeEnabled)
			Cardboard.SDK.VRModeEnabled = true;
		else
			Cardboard.SDK.VRModeEnabled = false;
	}

	IEnumerator startUp()
	{
 countdown.text = "3";    
		yield return new WaitForSeconds(1);  

		countdown.text = "2";    
		yield return new WaitForSeconds(1);  

		countdown.text = "1";    
		yield return new WaitForSeconds(1);  

		countdown.text = "GO";    
		yield return new WaitForSeconds(1);  

		startCanvas.enabled = false;
		eyeCanvas.enabled = true;
		move.moveEnabled = true;
		//eyeCanvas.enabled = true;
		timerOn = true;
		gameStarted = true;
		countdown.text = "";  
	}

	public void givingUp()
	{
		if (!gameOver && !roundCanvasUp) {
			Debug.Log ("Terminate");
			gameOver = true;
			m_RigidBody.transform.position = temp;
			m_RigidBody.transform.rotation = rotation;
			m_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; //freeze rotation
			move.moveEnabled = false;
			terminate.enabled = true;
			eyeCanvas.enabled = false;

			roundsTxt2.text = body2;
			giveUpResults.text = body;

			results2.text = body;
		}

	}

	//Resets round when clicked
	public void roundReset() {

		roundCanvasUp = false;
		roundWasReset = true;

		if (!gameOver) {
			m_RigidBody.transform.position = temp;
			//m_RigidBody.transform.rotation = rotation;

			Debug.Log ("games not over!!!!!!");
			//updateRoundComplete ();
			currentRound++;

			timerOn = true;
			stopTimer ();
			roundOver.enabled = false;
			eyeCanvas.enabled = true;
			move.moveEnabled = true;
			updateRoundText ();
			CardboardReticle.cubeFound = false;
			//Instantiate (trail);
		} else {
			//currentRound++;
			//updateRoundComplete();
			CardboardReticle.cubeFound = false;
			stopTimer ();
			roundOver.enabled = false;
			missionOverFunc ();
			Debug.Log ("Mission over!");
		}


	}
	void updateRoundComplete()
	{
		if (currentRound == 1)
			r1Complete = true;
		else if (currentRound == 2)
			r2Complete = true;
		else if (currentRound == 3)
			r3Complete = true;
		else if (currentRound == 4)
			r4Complete = true;
		else if (currentRound == 5)
			r5Complete = true;

		Debug.Log (r1Complete);
		Debug.Log (r2Complete);
		Debug.Log (r3Complete);
		Debug.Log (r4Complete);
		Debug.Log (r5Complete);

	}

	void setTime() {

		if (currentRound == 1)
			email.r1Time = minutes + ":" + seconds;
		else if (currentRound == 2)
			email.r2Time = minutes + ":" + seconds;
		else if (currentRound == 3)
			email.r3Time = minutes + ":" + seconds;
		else if (currentRound == 4)
			email.r4Time = minutes + ":" + seconds;
		else if (currentRound == 5)
			email.r5Time = minutes + ":" + seconds;

	}

	//Updates canvas when cube found
	public void cubeFound()
	{
		if (CardboardReticle.cubeFound == true) {

			score++;
			timesFound.text = score.ToString();

			CardboardReticle.cubeFound = false;
		}
	}
	void updateRoundText()
	{
		roundText.text = "ROUND "+currentRound+" COMPLETED";
	}

	void updateEyeText()
	{
		timesFound.text = score.ToString();
		//cubeImage.sprite = img2; //change image

		minutes = Mathf.Floor (timer / 60).ToString ("00");
		seconds = (timer % 60).ToString("00");
		timeEyeText.text = minutes + ":" + seconds;

	}
	public void stopTimer()
	{
		//setTime ();

		timer = 0;
		minutes = 0.ToString("00");
		seconds = 0.ToString("00");
		timeEyeText.text = minutes + ":" + seconds;
	}
	void roundOverFunc()
	{
		//ok.enabled = true;
		roundCanvasUp = true;
		//stopTimer ();
		setTime();
		timerOn = false;
		move.moveEnabled = false;
		timerText.text = minutes + ":" + seconds;
		roundOver.enabled = true;
		eyeCanvas.enabled = false;
	}
	/*Sprite gameOverImages() {

		if (UIManagerScript.roundNum == 1) {
			completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.5f);
			if (r1Complete)
				return r1Comp;
			else
				return r1InComp;
		}
		else if (UIManagerScript.roundNum == 2) {
			completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.3f);
			if (r2Complete)
				return r2Comp;
			else
				return r2InComp;
		}
		else if (UIManagerScript.roundNum == 3) {
			completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.2f);
			if (r3Complete)
				return r3Comp;
			else
				return r3InComp;
		}
		else if (UIManagerScript.roundNum == 4) {
			completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.2f);
			if (r4Complete)
				return r4Comp;
			else
				return r4InComp;
		}
		else {
			if (r5Complete)
				return r5Comp;
			else
				return r5InComp;
		}
	}*/
	void gameOverImagesUpdate() {

		if (UIManagerScript.roundNum == 1) {
			//completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.5f);
			completes.sprite = r1Comp;
		}
		else if (UIManagerScript.roundNum == 2) {
			//completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.3f);
			completes.sprite = r2Comp;
		}
		else if (UIManagerScript.roundNum == 3) {
			//completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.2f);
			completes.sprite = r3Comp;
		}
		else if (UIManagerScript.roundNum == 4) {
			//completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.2f);
			completes.sprite = r4Comp;
		}
		else {
			completes.sprite = r5Comp;
		}
	}
	void gameOverImagesUpdate2() {

		if (currentRound == 1) {
			//completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.5f);
			completes2.sprite = r1InComp;
			completes2.transform.localPosition = new Vector3(-70.3f, -114.0f, 0.0f);
		}
		else if (currentRound == 2) {
			//completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.3f);
				completes2.sprite = r2InComp;
			completes2.transform.localPosition = new Vector3(-70.3f, -102.0f, 0.0f);
			//Debug.Log ("ITS ROUND 2!!");
		}
		else if (currentRound == 3) {
			//completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.2f);
			completes2.sprite = r3InComp;
			completes2.transform.localPosition = new Vector3(-70.3f, -91.8f, 0.0f);
		}
		else if (currentRound == 4) {
			//completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.2f);
			completes2.sprite = r4InComp;
			completes2.transform.localPosition = new Vector3(-70.3f, -81.2f, 0.0f);
		}
		else {
			completes2.sprite = r5InComp;
			completes2.transform.localPosition = new Vector3(-70.3f, -90.6f, 0.0f);
		}
	}

	void moveCompletes1()
	{
		if (UIManagerScript.roundNum == 1) {
			completes.transform.localPosition = new Vector3(-70.3f, -111.8f, 0.0f);

		}
		else if (UIManagerScript.roundNum == 2) {
			completes.transform.localPosition = new Vector3(-70.3f, -101.0f, 0.0f);

		}
		else if (UIManagerScript.roundNum == 3) {
			//completes.transform.localPosition = new Vector2(completes.transform.localPosition.x, (completes.transform.localPosition.y)-0.5f);
			completes.transform.localPosition = new Vector3(-70.3f, -89.9f, 0.0f);

		}
		else if (UIManagerScript.roundNum == 4) {
			completes.transform.localPosition = new Vector3(-70.3f, -80.7f, 0.0f);

		}
		else {
			completes.transform.localPosition = new Vector3(-70.3f, -72.5f, 0.0f);
		}
	}
	void moveCompletes2()
	{
	}

	void roundOverFunc2()
	{
		//stopTimer ();

		roundCanvasUp = true;
		gameOver = true;
		moveCompletes1 ();

		//completes.sprite = gameOverImages ();
		setTime();
		timerOn = false;
		move.moveEnabled = false;
		timerText.text = minutes + ":" + seconds;
		roundOver.enabled = true;
		eyeCanvas.enabled = false;
	}
	void missionOverFunc()
	{
		//roundOver.enabled = false;
		//currentRound = 1; //RESET ROUNDS

		Debug.Log ("Mission over!");

		move.moveEnabled = false;
		endCanvas.enabled = true;
		results.text = body;
		resultsRounds.text = body2;

		//Set give up too
		roundsTxt2.text = body2;
		giveUpResults.text = body;
	}
	void updateFinalText() {
		if (currentRound == 1)
			body = "\n"+email.r1Time;
		else if (currentRound == 2)
			body = "\n"+email.r1Time + "\n" + email.r2Time;
		else if (currentRound == 3)
			body = "\n" + email.r1Time + "\n " + email.r2Time + "\n " + email.r3Time;
		else if (currentRound == 4)
			body = "\n" + email.r1Time + "\n" + email.r2Time + "\n" + email.r3Time + "\n" + email.r4Time;
		else if (currentRound == 5)
			body = "\n " + email.r1Time + "\n" + email.r2Time + "\n" + email.r3Time + "\n" + email.r4Time + "\n" + email.r5Time;
	}
	void updateFinalTextRounds() {
		if (currentRound == 1)
			body2 = "\nRound 1: ";
		else if (currentRound == 2)
			body2 = "\nRound 1: "+ "\nRound 2: ";
		else if (currentRound == 3)
			body2 = "\nRound 1: " + "\nRound 2: "+"\nRound 3: ";
		else if (currentRound == 4)
			body2 = "\nRound 1: "+"\nRound 2: "+"\nRound 3: "+"\nRound 4: ";
		else if (currentRound == 5)
			body2 = "\nRound 1: "+"\nRound 2: "+ "\nRound 3: " +"\nRound 4: " + "\nRound 5: ";

	}

	void alterCanvasDirection()
	{
		if (headRot.y >= -0.4f && headRot.y < 0.3f) {
			Debug.Log ("NORTH");
			//roundOver.transform.position = new Vector3(roundOver.transform.position.x, roundOver.transform.position.y, roundOver.transform.position.z);
			roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 1);
			ctx = main.transform.localPosition.x + 0.0f;
			ctz = main.transform.localPosition.z + 1.0f;
			roundOver.transform.position = new Vector3(ctx, 2.0f ,ctz);

			//roundOver.transform.position = new Vector3(6.2f,2.0f,-3.0f);
			endCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 1);
			endCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminate.transform.rotation = Quaternion.Euler(Vector3.up * 1);
			terminate.transform.position = new Vector3(ctx, 2.0f ,ctz);

		}
		else if(headRot.y >= 0.3f && headRot.y < 0.8f) {
			Debug.Log ("EAST");
			//roundOver.transform.position = new Vector3(roundOver.transform.position.x, roundOver.transform.position.y, roundOver.transform.position.z);
			roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			ctx = main.transform.localPosition.x + 1.0f;
			ctz = main.transform.localPosition.z + 0.0f;
			roundOver.transform.position = new Vector3(ctx, 2.0f ,ctz);

			endCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			endCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminate.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			terminate.transform.position = new Vector3(ctx, 2.0f ,ctz);
		}
		else if(headRot.y >= 0.8f && headRot.y < 1.0f || headRot.y >= -0.9f && headRot.y < -1.0f) 
		
		{
			Debug.Log ("SOUTH");
			//roundOver.transform.position = new Vector3(roundOver.transform.position.x, roundOver.transform.position.y, roundOver.transform.position.z);
			roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			ctx = main.transform.localPosition.x + 0.0f;
			ctz = main.transform.localPosition.z + -1.0f;
			roundOver.transform.position = new Vector3(ctx, 2.0f ,ctz);

			endCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			endCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminate.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			terminate.transform.position = new Vector3(ctx, 2.0f ,ctz);
		}
		else if(headRot.y >= -0.9f && headRot.y < -0.4f) {
			Debug.Log ("WEST");
			//roundOver.transform.position = new Vector3(roundOver.transform.position.x, roundOver.transform.position.y, roundOver.transform.position.z);
			roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			ctx = main.transform.localPosition.x + -1.0f;
			ctz = main.transform.localPosition.z + 0.0f;
			roundOver.transform.position = new Vector3(ctx, 2.0f ,ctz);

			endCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			endCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminate.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			terminate.transform.position = new Vector3(ctx, 2.0f ,ctz);
		}
	}


	// Update is called once per frame --------------------------------------------------------------------------------------
	void Update () {

		//Debug.Log ("IMAGE IS AT "+completes.transform.localPosition);

		//Set correct start pt
		if(UIManagerScript.sceneNum == 0)
			temp = new Vector3(6.2f,1.7f,-3.9f);
		else if(UIManagerScript.sceneNum == 1)
			temp = new Vector3(6.2f,1.7f,-3.9f);
		else
			temp = new Vector3(5.32f,1.57f,3.91f);

		headRot = head1.transform.localRotation;

		Debug.Log (temp);
		gameOverImagesUpdate ();
		gameOverImagesUpdate2 ();

		alterCanvasDirection();

		//world.transform.rotation = new Quaternion(0.0f, )
		//Debug.Log ("head is:"+head1.transform.localRotation);

		//Debug.Log ("Canvas is"+roundOver.transform.position);
		//Debug.Log ("world is"+world.transform.rotation);

		//-------------------------------------------------

		if (!move.moveBegun) {
			giveUp.enabled = false;
			menu.enabled = true;
		} else {
			giveUp.enabled = true;
			menu.enabled = false;
		}

		if(timerOn)
			timer += Time.deltaTime;


		updateFinalText();
		updateFinalTextRounds ();
		updateEyeText ();

		if (CardboardReticle.cubeFound == true) {
			if (!gameOver && gameStarted) {
				//Reset round if still rounds to go
				updateRoundComplete ();
				if (currentRound < UIManagerScript.roundNum) {
					
					roundOverFunc ();
				} else {
					Debug.Log ("Rounds over! Game over");

					roundOverFunc2 ();
				}
			}
			//move and cardboard to false
		}

	}
}