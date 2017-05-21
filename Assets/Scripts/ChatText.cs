using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatText : MonoBehaviour {

	string chat;

	public void AddToChat(string addOn){
		chat += addOn;
		GetComponent<ChatRoomMaster> ().changeTextInChat (chat);
	}
}
