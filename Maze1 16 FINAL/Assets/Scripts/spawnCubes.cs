using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class spawnCubes : MonoBehaviour {

	public static int numCubesExist = 0;
	public static int NUMTOBESPAWNED = 6;

	public static int numCubesExist2 = 0;
	public static int NUMTOBESPAWNED2 = 6;

	public GameObject cube2;

	public static bool touchedMaze = false;
	public static bool finalPosReached = false;

	public Material color1;
	public Material color2;
	public Material color3;
	public Material color4;

	Vector3 position; 

	public GameObject cube;

	int numToSpawn;

	// Use this for initialization
	void Start () {

		if (UIManagerScript.sceneNum == 0) {
			NUMTOBESPAWNED = 13;
			NUMTOBESPAWNED2 = 5;
		}
		else if(UIManagerScript.sceneNum == 1) {
			NUMTOBESPAWNED = 20;
			NUMTOBESPAWNED2 = 10;
		}
		else if(UIManagerScript.sceneNum == 2) {
			NUMTOBESPAWNED = 32;
			NUMTOBESPAWNED2 = 15;
		}

		//Debug.Log (NUMTOBESPAWNED);
		spawnRandCubes ();
	
	}
		
	void spawnRandCubes() {

//Spawn maze and cubes
		while(numCubesExist < NUMTOBESPAWNED)
		{
			cubeSpawner ();
		}
		/*while(GameLogic.cubesInScene < NUMTOBESPAWNED)
		{
			cubeSpawner ();
		}*/

		while(numCubesExist2 < NUMTOBESPAWNED2)
		{
			mazeCubeSpawner ();
		}

	

	}
	void mazeCubeSpawner()
	{

		//Random positions
		if (UIManagerScript.sceneNum == 0) {
			position = new Vector3 (Random.Range (7.0F, 26.0F), (float)1.4, Random.Range (-9.0F, 9.0F));
			Debug.Log (position);
		}
		else if(UIManagerScript.sceneNum == 1) {
			position = new Vector3 (Random.Range (7.0F, 42.0F), (float)1.4, Random.Range (-9.0F, 9.0F));
			Debug.Log (position);
		}
		else if(UIManagerScript.sceneNum == 2) {
			position = new Vector3 (Random.Range (-4.7F, 44.0F), (float)1.4, Random.Range (-23.0F, 25.0F));
			Debug.Log (position);
		}

		//spawn
		Instantiate (cube2, position, Quaternion.identity);
		Debug.Log ("spawned maze cube!"+numCubesExist2);
		numCubesExist2++;
	}

	void cubeSpawner()
	{
		//Random textures
		switch (Random.Range (0, 3)) {
		case 0:
			cube.GetComponent<Renderer> ().material = color1;

			break;

		case 1:
			cube.GetComponent<Renderer> ().material = color2;

			break;

		case 2:
			cube.GetComponent<Renderer> ().material = color3;
			break;

		case 3:
			cube.GetComponent<Renderer> ().material = color4;

			break;

		}

		//Random positions
		if (UIManagerScript.sceneNum == 0) {
			position = new Vector3 (Random.Range (7.0F, 26.0F), (float)1.4, Random.Range (-9.0F, 10.0F));
			Debug.Log (position);
		}
		else if(UIManagerScript.sceneNum == 1) {
			position = new Vector3 (Random.Range (7.0F, 47.0F), (float)1.4, Random.Range (-9.0F, 10.0F));
			Debug.Log (position);
		}
		else if(UIManagerScript.sceneNum == 2) {
			position = new Vector3 (Random.Range (-4.7F, 44.0F), (float)1.4, Random.Range (-23.0F, 25.0F));
			Debug.Log (position);
		}

		//spawn
		Instantiate (cube, position, Quaternion.identity);
		//Debug.Log ("spawned it!"+numCubesExist);
		numCubesExist++; //should do based off this?
	}

	
	// Update is called once per frame
	void Update () {

		//Instantiate (cube, new Vector3 (Random.Range (-4.7F, 44.0F), (float)1.4, Random.Range (-23.0F, 25.0F)), Quaternion.identity);

		if (GameLogic.roundWasReset) {
			numCubesExist = 0;
			numCubesExist2 = 0;

			GameLogic.roundWasReset = false;
			Debug.Log ("Called once!"+numCubesExist);
			//Respawn for round
			spawnRandCubes();
		}
	
	}
}
