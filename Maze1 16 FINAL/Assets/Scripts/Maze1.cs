using UnityEngine;
using System.Collections;

public class Maze1 : MonoBehaviour {

	[System.Serializable]

	public class Cell {
		bool visited;
		public GameObject northWall;
		public GameObject southWall;
		public GameObject eastWall;
		public GameObject westWall;
	}

	public GameObject wall;
	private GameObject wallHolder;
	public Cell[] cells;

	public float wallLength = 3.0f;
	private float zWallLength = 2.0f;
	public int xSize = 5;
	public int ySize = 5; 

	public Vector3 initialPos;

	// Use this for initialization
	void Start () {


		CreateWalls ();
	
	}

	void CreateWalls()
	{
		wallHolder = new GameObject();
		wallHolder.name = "Maze";

		Debug.Log ("walls being created");
		initialPos = new Vector3 ((-xSize / 2) + wallLength / 2, 0.0f, (-ySize / 2) + wallLength / 2);
		//You CAN set psotiion to something in code

		Vector3 myPos = initialPos;

		GameObject zWalls;

		//y axis - vert
		for(int i=0; i<=ySize;i++) { //rows
			for (int j = 0; j <= xSize+1; j++) { //cols
				myPos = new Vector3 (initialPos.x + (j * wallLength)-1, 0.0f, initialPos.z + (i*wallLength));
				zWalls = Instantiate(wall, myPos, Quaternion.Euler(0.0f, 90.0f,0.0f)) as GameObject;
				zWalls.transform.parent = wallHolder.transform;
			}
		}

		//x axis - horz
		for(int i=0; i<=ySize+1;i++) { //rows
			for (int j = 0; j <= xSize; j++) { //cols
				myPos = new Vector3 (initialPos.x + (j * wallLength) - wallLength / 2, 0.0f, initialPos.z + (i*wallLength)-wallLength/2);

				zWalls = Instantiate(wall, myPos, Quaternion.identity) as GameObject;

				zWalls.transform.parent = wallHolder.transform;
			}
		}

		CreateCells (); //make cells of walls
	}

	void CreateCells() 
	{
		GameObject[] allWalls;
		int children = wallHolder.transform.childCount;


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
