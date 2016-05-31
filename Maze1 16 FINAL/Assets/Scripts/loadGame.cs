using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadGame : MonoBehaviour {

	//public Animator anim;

	// Use this for initialization
	void Start () {
		
		StartCoroutine (waitForGame ());

	}
	IEnumerator waitForGame()
	{
		yield return new WaitForSeconds (8);
		//anim.Play ("New Animation");
		SceneManager.LoadScene ("Menu2");
	}
	// Update is called once per frame
	void Update () {

		Debug.Log ("SPLASH SCREEN!!!");
	
	}
}
