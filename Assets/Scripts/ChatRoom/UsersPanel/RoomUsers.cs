using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUsers : MonoBehaviour {
	
	//TODO : all numbers in variables.

	private bool p_isExpanded = false;

	private RectTransform p_panelRect;
	public GameObject i_arrowSprite;

	void Start(){
		p_panelRect = GetComponent<RectTransform> ();
		p_panelRect.sizeDelta = new Vector2 (0, p_panelRect.sizeDelta.y);
		p_panelRect.position += new Vector3 (-p_panelRect.position.x, 0f, 0f);
	}

	public void ButtonExpand_Click(){
		if (p_isExpanded) {
			small ();
		} else {
			big ();
		}
	}

	void small(){
		p_panelRect.position -= new Vector3 (p_panelRect.sizeDelta.x / 2, 0f, 0f);
		p_panelRect.sizeDelta = new Vector2 (0, p_panelRect.sizeDelta.y);
		rotateArrow ();
		p_isExpanded = false;
	}

	void big(){
		p_panelRect.sizeDelta = new Vector2 (200, p_panelRect.sizeDelta.y);
		p_panelRect.position += new Vector3 (p_panelRect.sizeDelta.x / 2, 0f, 0f);
		rotateArrow ();
		p_isExpanded = true;
	}

	void rotateArrow(){
		i_arrowSprite.GetComponent<RectTransform> ().localScale *= -1;
	}
}
