using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkUser : NetworkBehaviour {

	private InputField chatInputField;
	private ChatText chatController;

	void Start(){
		chatInputField = GameObject.FindGameObjectWithTag ("GameController").GetComponent<InputField> ();
		chatController = FindObjectOfType<ChatText> ();

		if (isLocalPlayer) {
			this.name = "LocalPlayer";
		}
	}

	void Update(){
		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown (KeyCode.Return)) {
			if (chatInputField.text != "") {
				CmdWrite (chatInputField.text, UserAccount.Username);
				chatInputField.text = "";
			}
		}
	}

	[Command]
	void CmdWrite(string text, string username){
		RpcWrite (text, username);
	}

	[ClientRpc]
	void RpcWrite(string text, string username){
		chatController.AddToChat(username + " : " + text + "\n");
	}
}
