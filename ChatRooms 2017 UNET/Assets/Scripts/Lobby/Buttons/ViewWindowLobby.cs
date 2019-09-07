using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ViewWindowLobby : BaseWindowLobby {

	new void Start () {
		base.Start ();
		c_lobbyCenter.setchatListParent (this.transform.GetChild(0).GetChild(0));
		Refresh ();
	}

	public void Refresh(){
		c_lobbyCenter.RefreshChatList ();
	}


}
