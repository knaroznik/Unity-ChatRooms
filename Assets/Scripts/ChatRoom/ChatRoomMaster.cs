using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ChatRoomMaster : NetworkBehaviour {

	[SerializeField]private Text i_chatTextComponent;
	[SerializeField]private ScrollRect i_chatScrollRect;
	[SerializeField]private Text i_chatActiveUsersText;

	public void changeTextInChat(string chatString){
		i_chatTextComponent.text = chatString;
		i_chatScrollRect.normalizedPosition = new Vector2 (0f, 0f);
	}

	public void Navigate_ToMainPage(){
		if (isServer) {
			NetworkManager.singleton.StopHost ();
		} else {
			NetworkManager.singleton.StopClient ();
		}
	}

	public void changeActiveUsersText(string l_newText){
		i_chatActiveUsersText.text = l_newText;
	}
}
