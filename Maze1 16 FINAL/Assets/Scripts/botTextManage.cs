using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class botTextManage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Text botText;

	void Start()
	{
		botText.color = new Color(0.92f,0.56f,0.02f);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//theText.color = new Color(120,120,120); //Or however you do your color
		botText.color = new Color(0.63f,0.38f,0f);
		//botText.color = Color.white;
		Debug.Log("mouse over!");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//theText.color = Color.black; //Or however you do your color
		botText.color = new Color(0.92f,0.56f,0.02f);
	}
}
