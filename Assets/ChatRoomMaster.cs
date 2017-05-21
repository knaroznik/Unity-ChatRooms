using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatRoomMaster : MonoBehaviour {

	public Text chat;
	public ScrollRect rect;

	public void EnterClicked(string input){
		if (input != "") {
			GetComponent<ChatText> ().AddToChat ("knaroznik : " + input + "\n");
		}
	}

	public void changeTextInChat(string chatString){
		chat.text = chatString;
		rect.normalizedPosition = new Vector2 (0f, 0f);
	}
}
