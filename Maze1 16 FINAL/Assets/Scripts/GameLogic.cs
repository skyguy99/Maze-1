using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class GameLogic : MonoBehaviour {

	//For rotation
	public GameObject world;
	private Vector3 headTemp = new Vector2(0.0f, 0.0f);
	//private Vector3 newPos = new Vector3 (1.25f,0.4f,-0.4f);

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
	public static int mazeCreds = 0;
	public static int totalScore = 0;
	public bool setScorer = true;

	public static int currentRound = 1;

	private int countdownTime;
	public int saveTime;
	public static float timer; //set time in seconds
	string minutes = Mathf.Floor(timer / 60).ToString("00");
	string seconds = (timer % 60).ToString("00");
	public static bool timerOn = false;

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
	public GameObject[] extras;


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

	public Canvas gameOverScore;
	public Canvas terminateScore;
	public Text GOScore1;
	public Text GOScore2;

	//Pause
	public Canvas pauseCanvas;

	public Text totalScoreTxt;

	public Canvas mapCanvas;

	public CardboardHead head1;
	public GameObject main;
	Quaternion headRot;
	Quaternion test1 = new Quaternion(0.0f,0.0f,0.0f,0.0f);

	//Bottom menu
	public Canvas botMenu;

	//Color change
	public static bool runLighting = false;

	private int currentColor = 0;
	private float changeTime = 0.2f;
	public Color startColor;
	public Color startColor2;
	public Color[] colors = new Color[5];
	public Color[] colors2 = new Color[5];

	public static int cubesInScene = 0;

	void NextColor(){
		
		if(currentColor>=colors.Length-1){
			currentColor = 0;
		}else{
			currentColor +=1;
		}
	}


	void SetChangeTime(float ct){
		
		changeTime = ct;
	}

	// Use this for initialization
	void Start () {

		//mazeCreds = 0;

		//Set time up by scene
		if (UIManagerScript.sceneNum == 0) { //CHANGE THIS LATER!!!!
			countdownTime = 65; //in sec
			//color change
			changeTime = 0.2f;
		} else if (UIManagerScript.sceneNum == 1) {
			countdownTime = 240;
			changeTime = 0.2f;
		} else {
			countdownTime = 360;
			changeTime = 0.2f;
		}

		timer = countdownTime;
		saveTime = 0;
		timerOn = false;

		botMenu.enabled = false;
		roundCanvasUp = false;
		roundWasReset = false;
		mapCanvas.enabled = false;

		//headRot = head1.transform.localRotation;
		//completes.transform.localPosition = new Vector2();
		completes.transform.localPosition = new Vector3(-70.3f,-72.0f,0.0f);

		gameOver = false;
		currentRound = 1;
		gameStarted = false;
		score = 0;
		totalScore = 0;
		setScorer = true;


		pauseCanvas.enabled = false;
		terminate.enabled = false;
		giveUp.enabled = false;
		endCanvas.enabled = false;
		terminateScore.enabled = false;
		gameOverScore.enabled = false;

		eyeCanvas.enabled = false;
		timesFound.text = score.ToString();
		roundText.text = "ROUND "+currentRound+" SCORE:";
		timerText.text = "Time left: "+minutes + ":" + seconds;
		totalScoreTxt.text = totalScore.ToString();

		timeEyeText.text = minutes + ":" + seconds;

		countdown.text = "5";

		//CardboardReticle.cubeFound = false;
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
		UIManagerScript.vrOff = !UIManagerScript.vrOff;

		Debug.Log ("Switch!!");
		if (!Cardboard.SDK.VRModeEnabled)
			Cardboard.SDK.VRModeEnabled = true;
		else
			Cardboard.SDK.VRModeEnabled = false;
	}

	IEnumerator startUp()
	{
		countdown.text = "5";    
		yield return new WaitForSeconds(1);  

		countdown.text = "4";    
		yield return new WaitForSeconds(1);  

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
		runLighting = true;
		countdown.text = "";

		giveUp.enabled = true;
		menu.enabled = false;
		score = 0;

		//Cube enum
		GameObject[] allObjects = GameObject.FindGameObjectsWithTag("blueCube");
		foreach (GameObject go in allObjects) {
			cubesInScene++;

		}
	}

	public void unPause()
	{

		timeEyeText.text = minutes + ":" + seconds;
		timerOn = true;
		//stopTimer ();
		//resetTimer(); //reset
		roundOver.enabled = false;
		eyeCanvas.enabled = true;
		move.moveEnabled = true;
		pauseCanvas.enabled = false;
		giveUp.enabled = true;
		botMenu.enabled = false;
	}

	public void gameOverFunc()
	{
		//GAME OVER/THEY LOSE
		gameOver = true;
		timerOn = false;
		roundsTxt2.text = body2;
		giveUpResults.text = body; //time

		//results2.text = body;

		move.moveEnabled = false;
		terminate.enabled = true;
		eyeCanvas.enabled = false;
		RenderSettings.ambientLight = startColor;
		RenderSettings.ambientGroundColor = startColor2;
	}

	public void givingUp()
	{

		if (!gameOver && timerOn) {	
			//PAUSE GAME
			timerOn = false;


			//Enable bottom menu

			move.moveEnabled = false;
			giveUp.enabled = false;
			botMenu.enabled = true;
		}

	}

	//Resets round when clicked
	public void roundReset() {

		//RESET GAME
		setScorer = true;

		//Decrement time
		if (UIManagerScript.sceneNum == 2) {
			countdownTime -= 15;
		} else {
			countdownTime -= 10;
		}

		roundCanvasUp = false;
		score = 0;
		//cubesInScene = 0;
		destroyAllMazeCubes ();
		mazeCreds = 0;


		if (!gameOver) {
			roundWasReset = true;
			m_RigidBody.transform.position = temp;
			//m_RigidBody.transform.rotation = rotation;

			Debug.Log ("games not over!!!!!!");
			//updateRoundComplete ();
			currentRound++;

			timerOn = true;
			//stopTimer ();
			resetTimer(); //reset
			roundOver.enabled = false;
			eyeCanvas.enabled = true;
			move.moveEnabled = true;
			updateRoundText ();
			CardboardReticle.cubeFound = false;
			//Instantiate (trail);
		} else {

			RenderSettings.ambientLight = startColor;
			RenderSettings.ambientGroundColor = startColor2;
			CardboardReticle.cubeFound = false;
			//stopTimer ();
			resetTimer();
			roundOver.enabled = false;
			missionOverFunc ();
			Debug.Log ("Mission over!");
		}
		//Reenumerate num scenes each times starts again

		/*GameObject[] allObjects = GameObject.FindGameObjectsWithTag("blueCube");
		foreach (GameObject go in allObjects) {
			cubesInScene++;
		}*/ //maybe in spawn script?
			
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

		//Debug.Log ("Set time called for"+currentRound);
	}

	//Updates canvas when cube found
	public void cubeFound()
	{
		/*if (CardboardReticle.cubeFound == true) {

			score++;
			timesFound.text = score.ToString();

			CardboardReticle.cubeFound = false;
		}*/
	}
	void updateRoundText()
	{
		roundText.text = "ROUND "+currentRound+" SCORE:";
	}

	void updateEyeText()
	{
		timesFound.text = score.ToString();
		//cubeImage.sprite = img2; //change image

		minutes = Mathf.Floor (timer / 60).ToString ("00");
		seconds = (timer % 60).ToString("00");
		if(timerOn)
			timeEyeText.text = minutes + ":" + seconds;
		else
			timeEyeText.text = "PAUSED";

	}
	void destroyAllMazeCubes()
	{
		extras = GameObject.FindGameObjectsWithTag ("mazeCube");

		for(int i = 0 ; i < extras.Length ; i ++)
		{
			Destroy(extras[i]);
		}
	}

	public void resetTimer()
	{

		timer = countdownTime;
		minutes = 0.ToString("00");
		seconds = 0.ToString("00");
		timeEyeText.text = minutes + ":" + seconds;
		//Debug.Log ("Timer is"+timer);
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
		setTotalScore ();
		totalScoreTxt.text = totalScore.ToString ();
		mapCanvas.enabled = false;
		roundCanvasUp = true;
		//Score

		//stopTimer ();
		setTime();
		timerOn = false;
		move.moveEnabled = false;
		timerText.text = "Time left: "+minutes + ":" + seconds;
		totalScoreTxt.text = totalScore.ToString();
		roundOver.enabled = true;
		eyeCanvas.enabled = false;
	}

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
		mapCanvas.enabled = false;
		gameOver = true;
		moveCompletes1 ();

		//completes.sprite = gameOverImages ();
		setTime();
		timerOn = false;
		move.moveEnabled = false;
		timerText.text = "Time left: "+minutes + ":" + seconds;
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
	public void switchToEndScore()
	{
		endCanvas.enabled = false;
		gameOverScore.enabled = true;
	}
	public void switchToTermScore()
	{
		terminate.enabled = false;
		terminateScore.enabled = true;
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
			//Debug.Log ("NORTH");
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

			mapCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 1);
			mapCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminateScore.transform.rotation = Quaternion.Euler(Vector3.up * 1);
			terminateScore.transform.position = new Vector3(ctx, 2.0f ,ctz);

			gameOverScore.transform.rotation = Quaternion.Euler(Vector3.up * 1);
			gameOverScore.transform.position = new Vector3(ctx, 2.0f ,ctz);

		}
		else if(headRot.y >= 0.3f && headRot.y < 0.8f) {
			//Debug.Log ("EAST");
			//roundOver.transform.position = new Vector3(roundOver.transform.position.x, roundOver.transform.position.y, roundOver.transform.position.z);
			roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			ctx = main.transform.localPosition.x + 1.0f;
			ctz = main.transform.localPosition.z + 0.0f;
			roundOver.transform.position = new Vector3(ctx, 2.0f ,ctz);

			endCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			endCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminate.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			terminate.transform.position = new Vector3(ctx, 2.0f ,ctz);

			mapCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			mapCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminateScore.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			terminateScore.transform.position = new Vector3(ctx, 2.0f ,ctz);

			gameOverScore.transform.rotation = Quaternion.Euler(Vector3.up * 90);
			gameOverScore.transform.position = new Vector3(ctx, 2.0f ,ctz);
		}
		else if(headRot.y >= 0.8f && headRot.y < 1.0f || headRot.y >= -0.9f && headRot.y < -1.0f) 
		
		{
			//Debug.Log ("SOUTH");
			//roundOver.transform.position = new Vector3(roundOver.transform.position.x, roundOver.transform.position.y, roundOver.transform.position.z);
			roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			ctx = main.transform.localPosition.x + 0.0f;
			ctz = main.transform.localPosition.z + -1.0f;
			roundOver.transform.position = new Vector3(ctx, 2.0f ,ctz);

			endCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			endCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminate.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			terminate.transform.position = new Vector3(ctx, 2.0f ,ctz);

			mapCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			mapCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminateScore.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			terminateScore.transform.position = new Vector3(ctx, 2.0f ,ctz);

			gameOverScore.transform.rotation = Quaternion.Euler(Vector3.up * 180);
			gameOverScore.transform.position = new Vector3(ctx, 2.0f ,ctz);
		}
		else if(headRot.y >= -0.9f && headRot.y < -0.4f) {
			//Debug.Log ("WEST");
			//roundOver.transform.position = new Vector3(roundOver.transform.position.x, roundOver.transform.position.y, roundOver.transform.position.z);
			roundOver.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			ctx = main.transform.localPosition.x + -1.0f;
			ctz = main.transform.localPosition.z + 0.0f;
			roundOver.transform.position = new Vector3(ctx, 2.0f ,ctz);

			endCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			endCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminate.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			terminate.transform.position = new Vector3(ctx, 2.0f ,ctz);

			mapCanvas.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			mapCanvas.transform.position = new Vector3(ctx, 2.0f ,ctz);

			terminateScore.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			terminateScore.transform.position = new Vector3(ctx, 2.0f ,ctz);

			gameOverScore.transform.rotation = Quaternion.Euler(Vector3.up * 270);
			gameOverScore.transform.position = new Vector3(ctx, 2.0f ,ctz);
		}
	}

	IEnumerator canvasWait() {

		mapCanvas.enabled = true;
		yield return new WaitForSeconds(5);
		mapCanvas.enabled = false;
	}
	void showBonusCanvas1()
	{
		mapCanvas.enabled = true;
		//yield WaitForSeconds (5);
		mapCanvas.enabled = false;
	}
	void setTotalScore()
	{	
		if (setScorer) {
			saveTime = (int)timer;
			Debug.Log ("Save time is" + saveTime);
			totalScore += 20 * saveTime;

			setScorer = false;
		}

	}


	// Update is called once per frame --------------------------------------------------------------------------------------
	void Update () {

		//Cube canvas help
		if (Input.GetButtonDown ("Fire1")) {
			//Debug.Log ("kill cube canvas");
			mapCanvas.enabled = false;
		}

		//Debug.Log (UIManagerScript.vrOff);

		Debug.Log ("Creds = "+mazeCreds);
		Debug.Log ("cubes = "+cubesInScene);
		Debug.Log ("actual cubes="+spawnCubes.numCubesExist);


		if(UIManagerScript.vrOff)
			Cardboard.SDK.VRModeEnabled = false;

		Cardboard.SDK.EnableSettingsButton = false;
		Cardboard.SDK.BackButtonMode = Cardboard.BackButtonModes.Off;


		//Debug.Log (saveTime + "----" + timer);
		Debug.Log("SCORE = "+totalScore);

		//For game over scores
		GOScore1.text = totalScore.ToString();
		GOScore2.text = totalScore.ToString ();

		//Debug.Log ((int)timer);
		GameObject[] allObjects = GameObject.FindGameObjectsWithTag("blueCube");

		GameObject[] mazeCubes = GameObject.FindGameObjectsWithTag("mazeCube");

		cubesInScene = allObjects.Length;
		if (timerOn) {
			foreach (GameObject g in allObjects) {
				g.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
			}
			foreach (GameObject m in mazeCubes) {
				m.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
			}

		}

		if (move.firstMapFound && move.numMapFound < 1 && timerOn) { //can take timeron out if problems

			move.firstMapFound = false;
			move.numMapFound++;

			StartCoroutine(canvasWait());

		}

		if(Input.GetKeyDown("space")){
			NextColor();
		}

		//giveUp.enabled = false;
		//Debug.Log ("IMAGE IS AT "+completes.transform.localPosition);

		//Set correct start pt
		if(UIManagerScript.sceneNum == 0)
			temp = new Vector3(6.2f,1.7f,-3.9f);
		else if(UIManagerScript.sceneNum == 1)
			temp = new Vector3(6.2f,1.7f,-3.9f);
		else
			temp = new Vector3(5.32f,1.57f,3.91f);

		headRot = head1.transform.localRotation;

		//Debug.Log (temp);
		gameOverImagesUpdate ();
		gameOverImagesUpdate2 ();

		alterCanvasDirection();



		if (timerOn) {
			timer -= Time.deltaTime;

			//colors
			if (timerOn && runLighting) {
				//RenderSettings.ambientLight = Color.Lerp (RenderSettings.ambientLight, colors2 [currentColor], changeTime * Time.deltaTime);
				//RenderSettings.ambientSkyColor = Color.Lerp (RenderSettings.ambientSkyColor, colors [currentColor], changeTime * Time.deltaTime);
				//RenderSettings.ambientGroundColor = Color.Lerp (RenderSettings.ambientGroundColor, colors2[currentColor], changeTime*Time.deltaTime);
			}
		} 

		if(timer <= 0)
		{
			//ROUND OVER/GAME OVER
			//resetTimer ();
			if (!gameOver) {
				gameOverFunc();
			}
		}
		//Debug.Log (timer);

		//Debug.Log (timer);
		updateFinalText();
		updateFinalTextRounds ();
		updateEyeText ();

		if (cubesInScene <= 0) {
			if (!gameOver && gameStarted) {
				
				if (currentRound < UIManagerScript.roundNum) {
					//Still rounds to play
					roundOverFunc ();
				} else {
					//They won

					roundOverFunc2 ();
				}

			}

		}

	}
}