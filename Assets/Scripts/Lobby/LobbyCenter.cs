using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class LobbyCenter : MonoBehaviour {

	private string p_chatName;
	private uint p_roomSize = 6;
	private NetworkManager p_networkManager;

	private List<GameObject> p_chatList = new List<GameObject> ();
	[SerializeField] private GameObject i_chatListItemPrefab;
	[SerializeField] private Transform i_chatListParent;

	void Start () {
		p_networkManager = NetworkManager.singleton;
		if (p_networkManager.matchMaker == null)
		{
			p_networkManager.StartMatchMaker();
		}
		RefreshChatList ();
	}

	//Host Game : 

	public void SetChatName (string l_chatName){
		p_chatName = l_chatName;
	}

	public void CreateRoom (){
		if (p_chatName != "" && p_chatName != null) {
			p_networkManager.matchMaker.CreateMatch (p_chatName, p_roomSize, 
				true, "", "", "", 0, 0, p_networkManager.OnMatchCreate);
		}
	}

	public void RefreshChatList(){
		ClearChatList ();
		p_networkManager.matchMaker.ListMatches (0, 20, "", true, 0, 0, OnMatchList);
	}

	void ClearChatList(){
		foreach (GameObject chat in p_chatList) {
			Destroy (chat);
		}
	}

	void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList){
		if (!success || matchList == null) {
			Debug.Log ("Cant get chatRooms");
			return;
		}

		foreach (MatchInfoSnapshot match in matchList) {
			GameObject chatItem = Instantiate (i_chatListItemPrefab, i_chatListParent);
			chatItem.GetComponent<ChatListItem> ().Setup (match);
		}
	}
}
