using UnityEngine;
using System.Collections;

public class GetCube : MonoBehaviour {

	Ray ray;
	RaycastHit hit;

	// Use this for initialization
	void Start () {

	
	}

	//Tests for mouse hover
	void OnMouseOver()
	{
		print ("Moused over!");
	}
		
	void OnCollisionStay(Collision col){
		
		if (col.gameObject.tag == "Maze" || col.gameObject.tag == "Player") {
			
			Debug.Log ("touching!!");
			Destroy(this.gameObject);
			spawnCubes.touchedMaze = true;

		}
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")) {
			Debug.Log ("Clicked!");
		}
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	
	}
}
