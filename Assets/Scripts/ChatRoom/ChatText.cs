using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChatText : NetworkBehaviour {

	private ChatRoomMaster controller;
	private bool p_firstTime = true;

	[SyncVar] private string p_chatText;
	private List<string> p_activeUsers = new List<string>();
	[SyncVar] private string p_activeUsersAsString = "";

	void Start(){
		controller = GetComponent<ChatRoomMaster> ();
	}

	void stringToArray(){
		if (p_activeUsersAsString != "") {
			string[] l_array = p_activeUsersAsString.Split (',');
			foreach (string x in l_array) {
				p_activeUsers.Add (x);
			}
			directRequestToController ();
		}
	}

	public void AddToChat(string l_addText){
		p_chatText += l_addText;
		p_chatText += "\n";
		controller.changeTextInChat (p_chatText);
	}

	public void AddActiveUser(string l_userName){
		if (p_firstTime) {
			stringToArray ();
			p_firstTime = false;
		}
		p_activeUsers.Add(l_userName);
		directRequestToController ();
	}

	void directRequestToController(){
		string l_activeUsersFullText = "Active Users : ";
		l_activeUsersFullText += string.Join (", ", p_activeUsers.ToArray ());
		p_activeUsersAsString = string.Join (", ", p_activeUsers.ToArray ());
		controller.changeActiveUsersText (l_activeUsersFullText);
	}

	public void DeleteDisconnecteduser(string l_userName){
		if (p_activeUsers.Contains (l_userName)) {
			p_activeUsers.Remove (l_userName);
			directRequestToController ();
		}
	}
}
