using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class LobbyCenter : MonoBehaviour {

	#region Variables and Start() : 

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

	#endregion

	#region Create Chat :

	public void SetChatName (string l_chatName){
		p_chatName = l_chatName;
	}

	public void CreateRoom (){
		if (p_chatName != "" && p_chatName != null) {
			p_networkManager.matchMaker.CreateMatch (p_chatName, p_roomSize, 
				true, "", "", "", 0, 0, p_networkManager.OnMatchCreate);
		}
	}

	#endregion

	#region All chat list : 

	public void RefreshChatList(){
		ClearChatList ();

		if (p_networkManager.matchMaker == null) {
			p_networkManager.StartMatchMaker ();
		}

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
			chatItem.GetComponent<ChatListItem> ().Setup (match, joinRoom);
			p_chatList.Add (chatItem);
		}
	}
	#endregion

	#region joining : 

	void joinRoom(MatchInfoSnapshot match){
		p_networkManager.matchMaker.JoinMatch (match.networkId, "", "", "", 0, 0, p_networkManager.OnMatchJoined);
		StartCoroutine (waitForJoin ());
	}

	IEnumerator waitForJoin(){
		int l_waitCounter = 10;
		while (l_waitCounter > 0) {
			yield return new WaitForSeconds (1f);
			l_waitCounter--;
		}

		Debug.Log ("Cant connect!");
		yield return new WaitForSeconds (1f);

		MatchInfo matchInfo = p_networkManager.matchInfo;
		if (matchInfo != null) {
			p_networkManager.matchMaker.DropConnection (matchInfo.networkId, matchInfo.nodeId, 0, p_networkManager.OnDropConnection);
			p_networkManager.StopHost ();
		}

		RefreshChatList ();
	}

	#endregion
}
