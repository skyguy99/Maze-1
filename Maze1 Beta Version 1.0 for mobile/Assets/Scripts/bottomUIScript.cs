using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bottomUIScript : MonoBehaviour {

	public Button giverUp;
	public Button back;
	public Sprite clicked1;
	public Sprite clicked2;
	public Sprite notClicked1;
	public Sprite notClicked2;

	public void backToMenu() {

		SceneManager.LoadScene ("Menu2");
		Debug.Log ("GO TO MENU!!");

	}

	public void changeImage1()
	{
		back.image.sprite = clicked1;
	}
	public void changeImageBack1()
	{
		back.image.sprite = notClicked1;
	}
	public void changeImageBack2()
	{
		giverUp.image.sprite = notClicked2;
	}
	public void changeImage2()
	{
		giverUp.image.sprite = clicked2;
	}

	void onMouseOver()
	{
		Debug.Log ("hovering!");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//back.GetComponent<SpriteRenderer> ().sprite = clicked1;

	}
}
