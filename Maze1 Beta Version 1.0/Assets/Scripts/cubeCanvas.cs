using UnityEngine;
using System.Collections;

public class cubeCanvas : MonoBehaviour {

	Ray ray;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionStay(Collision col){

		/*if (col.gameObject.tag == "Maze") {

			Debug.Log ("canvas touching!!");
			Destroy(this.gameObject);

		}*/
	}
	
	// Update is called once per frame
	void Update () {

	
		if (spawnCubes.touchedMaze == true) {
			Destroy(this.gameObject);
		}
	}
}
