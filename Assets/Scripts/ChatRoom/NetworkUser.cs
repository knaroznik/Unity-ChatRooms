using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NetworkUser : NetworkBehaviour {

	#region Variables + Start/Update

	private InputField p_chatInputField;
	private ChatText p_chatModel;
	private EventSystem system;

	void Start(){
		p_chatInputField = GameObject.FindGameObjectWithTag ("GameController").GetComponent<InputField> ();
		p_chatModel = FindObjectOfType<ChatText> ();
		system = EventSystem.current;
	}
		
	void Update(){
		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown (KeyCode.Return)) {
			if (p_chatInputField.text != "") {
				CmdWrite (p_chatInputField.text, UserAccount.GetUserName());
				p_chatInputField.text = "";
				//TODO : Selecting InputField.
				EventSystem.current.SetSelectedGameObject (p_chatInputField.gameObject, new BaseEventData(system));
			}
		}
	}
	#endregion

	#region Chat Writing 
	[Command]
	void CmdWrite(string text, string username){
		RpcWrite (text, username);
	}

	[ClientRpc]
	void RpcWrite(string text, string username){
		p_chatModel.AddToChat(username + " : " + text);
	}

	#endregion

	#region User Network Start

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		CmdShowUser (UserAccount.GetUserName ());
	}

	[Command]
	void CmdShowUser(string l_userName){
		RpcShowUser (l_userName);
	}

	[ClientRpc]
	void RpcShowUser(string l_userName){
		p_chatModel.AddActiveUser (l_userName);
		p_chatModel.AddToChat ("<color=#a52a2aff>" + l_userName + " joined the room </color>");
		this.gameObject.name = l_userName;
	}

	#endregion

	#region Showing disconnection
	[Command]
	public void CmdDeleteDisconnectedUser(string l_input){
		RpcDeleteDisconnectedUser (l_input);
	}

	[ClientRpc]
	public void RpcDeleteDisconnectedUser(string l_input){
		p_chatModel.DeleteDisconnectedUser (l_input);
		p_chatModel.AddToChat (l_input + " has left");
	}

	#endregion
}
