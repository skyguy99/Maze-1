using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class spawnCubes : MonoBehaviour {

	public static bool touchedMaze = false;
	public static bool finalPosReached = false;

	public Material color1;
	public Material color2;
	public Material color3;
	public Material color4;

	public Vector3 position; 
	public GameObject cube;
	public Canvas cubeAlert;
	int numToSpawn;

	// Use this for initialization
	void Start () {

		spawnRandCube ();
	
	}
	void spawnRandCube() {

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

		touchedMaze = false;
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

			Instantiate(cube, position, Quaternion.identity);
			Debug.Log ("spawned it!");
		finalPosReached = true;

	}

	
	// Update is called once per frame
	void Update () {

		while (touchedMaze == true) {
			spawnRandCube ();
		}
	
	}
}
