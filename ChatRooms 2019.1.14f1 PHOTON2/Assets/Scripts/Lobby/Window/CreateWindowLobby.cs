using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWindowLobby : BaseWindowLobby {

	public void SetChatName(string l_input){
		c_lobbyCenter.ChatName = l_input;
	}

	public void Create(){
		if (c_userName != "") {
			c_lobbyCenter.CreateRoom ();
		}
	}
}
