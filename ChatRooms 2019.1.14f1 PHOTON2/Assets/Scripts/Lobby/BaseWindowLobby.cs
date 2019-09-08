using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWindowLobby : MonoBehaviour {

	protected string c_userName;
	protected LobbyCenter c_lobbyCenter;

	protected void Start () {
		c_lobbyCenter = FindObjectOfType<LobbyCenter> ();
	}

	public void SetUserName(string l_input){
		c_userName = l_input;
	}

	public void SetUserNameGlobal(){
		UserAccount.SetUserName (c_userName);
	}

	public void SetUserNameGlobal(string l_input){
		UserAccount.SetUserName (l_input);
	}
}
