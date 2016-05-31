using UnityEngine;
using System.Collections;

public class GetCube : MonoBehaviour {

	Ray ray;
	RaycastHit hit;

	Vector3 position;

	float xLowBound;
	float xHighBound;
	float zLowBound;
	float zHighBound;



	// Use this for initialization
	void Start () {

		//spawn ();
	
	}

		
	void OnCollisionStay(Collision col){

		//col.gameObject.tag == "Maze"
		if (col.gameObject.tag == "Maze" && GameLogic.timerOn && GameLogic.cubesInScene > 2) { //Can take this out

			Destroy(this.gameObject);
			spawnCubes.numCubesExist--;
		}

		/*if (col.gameObject.tag == "blueCube" || col.gameObject.tag == "mazeCube") {

			//Destroy cube
			Destroy(this.gameObject);
			spawnCubes.numCubesExist--;

			//Debug.Log ("touching!!" + spawnCubes.numCubesExist);

			spawnCubes.touchedMaze = true;

		}*/
	}

	void killOutliers()
	{
		if (UIManagerScript.sceneNum == 0) {
			xLowBound = 7.0f;
			xHighBound = 26.0f;
			zLowBound = -8.0f;
			zHighBound = 10.0f;

			
		} else if (UIManagerScript.sceneNum == 1) {
			xLowBound = 7.0f;
			xHighBound = 45.0f;
			zLowBound = -8.0f;
			zHighBound = 10.0f;
			
		} else {
			xLowBound = 7.0f;
			xHighBound = 44.0f;
			zLowBound = -22.0f;
			zHighBound = 25.0f;
		}

		if ((this.transform.position.x < xLowBound || this.transform.position.x > xHighBound )|| (this.transform.position.z < zLowBound || this.transform.position.z > zHighBound)) {
			Debug.Log ("Out of bounds!!!");
			spawnCubes.numCubesExist--;
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {

		//Instantiate (this.gameObject, position, Quaternion.identity);

		/*if (this.transform.position.x != 0) {
			Debug.Log (this.transform.position.x);
		}*/
		killOutliers ();

		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	
	}
}
