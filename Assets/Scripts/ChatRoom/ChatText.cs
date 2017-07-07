using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChatText : NetworkBehaviour {

	private ChatRoomMaster controller;


	[SyncVar] private string p_chatText;

	//UsersPanel : 
	private bool p_firstTime = true;
	private Dictionary<string, GameObject> p_RoomUsers = new Dictionary<string, GameObject>();
	public GameObject i_RoomUserObject;
	public Transform i_RoomUsersParent;
	[SyncVar] private string p_UserList_SyncVar = "";

	void Start(){
		controller = GetComponent<ChatRoomMaster> ();
	}

	public void AddToChat(string l_addText){
		p_chatText += l_addText;
		p_chatText += "\n";
		controller.changeTextInChat (p_chatText);
	}



	//UsersPanel : 

	public void AddActiveUser(string l_userName){
		if (p_firstTime) {
			readUsersFromServer ();
			p_firstTime = false;
		}
		GameObject l_RoomUser = Instantiate (i_RoomUserObject, i_RoomUsersParent);
		p_RoomUsers.Add (l_userName, l_RoomUser);
		showUsersAndSaveSyncVar ();
	}

	void readUsersFromServer(){
		if (p_UserList_SyncVar != "") {
			string[] l_array = p_UserList_SyncVar.Split (',');
			foreach (string x in l_array) {
				GameObject l_RoomUser = Instantiate (i_RoomUserObject, i_RoomUsersParent);
				p_RoomUsers.Add (x, l_RoomUser);
			}
			showUsersAndSaveSyncVar ();
		}
	}

	void showUsersAndSaveSyncVar(){
		changeNames();
		saveSyncVar();
	}

	//TODO
	void changeNames(){
		
	}

	void saveSyncVar(){
		var keys = new List<string>(p_RoomUsers.Keys);
		p_UserList_SyncVar = string.Join (", ", keys.ToArray());
	}

	public void DeleteDisconnectedUser(string l_userName){
		if (p_RoomUsers.ContainsKey (l_userName)) {
			GameObject objectToDestroy;
			p_RoomUsers.TryGetValue (l_userName, out objectToDestroy);
			Destroy (objectToDestroy);
			p_RoomUsers.Remove (l_userName);
			showUsersAndSaveSyncVar ();
		}
	}
}
