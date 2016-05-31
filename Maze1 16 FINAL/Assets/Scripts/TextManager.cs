using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class TextManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Text theText;


	public void OnPointerEnter(PointerEventData eventData)
	{
		theText.color = new Color(120,120,120); //Or however you do your color

		Debug.Log("mouse over!");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		theText.color = Color.black; //Or however you do your color

	}
}