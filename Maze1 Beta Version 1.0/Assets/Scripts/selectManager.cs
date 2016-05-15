using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class selectManager : MonoBehaviour {

	public static bool hasClicked = false;
	public static string mazeText;
	Text txt;

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text="Maze 1";
	}
	void updateMazeText()
	{
		if (UIManagerScript.sceneNum == 0)
			mazeText = "Maze 1";
		else if (UIManagerScript.sceneNum == 1)
			mazeText = "Double Grid";
		else if (UIManagerScript.sceneNum == 2)
			mazeText = "Labyrinth";
	}

	// Update is called once per frame
	void Update () {

		updateMazeText ();
		txt.text = mazeText;
		}
  
}
