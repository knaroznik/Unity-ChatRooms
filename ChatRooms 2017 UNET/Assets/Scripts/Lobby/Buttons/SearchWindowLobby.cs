using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchWindowLobby : BaseWindowLobby {

	new void Start () {
		base.Start ();
		c_lobbyCenter.setchatListParent (this.transform.GetChild (2));
	}

	public void SetChatNameToSearch(string l_input){
		c_lobbyCenter.SetSearchName (l_input);
	}

	public void Search(){
		if (c_userName != "") {
			c_lobbyCenter.SearchForChat ();
		}
	}
}
