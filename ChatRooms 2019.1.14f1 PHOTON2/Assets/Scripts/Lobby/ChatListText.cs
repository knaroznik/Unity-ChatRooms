using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatListText : MonoBehaviour {

	[SerializeField] Text i_InfoText;

	public void setText(string l_input){
		i_InfoText.text = l_input;
	}
}
