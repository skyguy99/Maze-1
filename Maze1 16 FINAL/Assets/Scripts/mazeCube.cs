using UnityEngine;
using System.Collections;

public class mazeCube : MonoBehaviour {

	float xLowBound;
	float xHighBound;
	float zLowBound;
	float zHighBound;

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionStay(Collision col){

		/*if (col.gameObject.tag == "Maze") {

			//Destroy cube
			Destroy(this.gameObject);
			spawnCubes.numCubesExist2--;

			//Debug.Log ("touching!!" + spawnCubes.numCubesExist2);

			//spawnCubes.touchedMaze = true;

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
			Destroy (this.gameObject);
		}
	}

	
	// Update is called once per frame
	void Update () {

		killOutliers ();

		transform.Rotate (new Vector3 (0, 45, 0) * Time.deltaTime);
	
	}
}
