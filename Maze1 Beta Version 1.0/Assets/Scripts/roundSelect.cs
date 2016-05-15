using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class roundSelect : MonoBehaviour {

	Text txt;

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text=UIManagerScript.roundNum+" rounds";
	}

	// Update is called once per frame
	void Update () {
		txt.text=UIManagerScript.roundNum+" rounds";
}
}
