using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class LobbyCenter : MonoBehaviour {

	#region Variables and class Functions : 

	private string p_chatName;
	private uint p_roomSize = 6;
	private NetworkManager p_networkManager;
	private List<GameObject> p_chatList = new List<GameObject> ();
	private Transform p_chatListParent;
	private string p_searchingName;

	private RoomConnection roomConnection;

	[SerializeField] private GameObject i_chatListItemPrefab;
	[SerializeField] private GameObject i_chatListTextPrefab;

	void Start () {
		
		roomConnection = new RoomConnection ();

		UserAccount.defaultUserName ();
		p_networkManager = NetworkManager.singleton;
		if (p_networkManager.matchMaker == null) {
			p_networkManager.StartMatchMaker ();
		}
	}

	void showErrorOnChatList(string l_errorText){
		ClearChatList ();
		GameObject chatText = Instantiate (i_chatListTextPrefab, p_chatListParent);
		chatText.GetComponent<ChatListText> ().setText (l_errorText);
		p_chatList.Add (chatText);
	}

	public void setchatListParent(Transform l_newParent){
		p_chatListParent = l_newParent;
	}

	#endregion

	#region Create Chat :

	public void SetChatName (string l_chatName){
		p_chatName = l_chatName;
	}

	public void CreateRoom (){
		StartCoroutine (roomConnection.CreateRoom (p_chatName, p_networkManager, p_roomSize));
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
			showErrorOnChatList ("Cant get rooms!");
			return;
		}

		if (matchList.Count < 1) {
			showErrorOnChatList ("No chats at this moment!");
			return;
		}

		foreach (MatchInfoSnapshot match in matchList) {
			GameObject chatItem = Instantiate (i_chatListItemPrefab, p_chatListParent);
			chatItem.GetComponent<ChatListItem> ().Setup (match, joinRoom, 10);
			p_chatList.Add (chatItem);
		}
	}


	#endregion

	#region joining : 

	void joinRoom(MatchInfoSnapshot match, int seconds){
		p_networkManager.matchMaker.JoinMatch (match.networkId, "", "", "", 0, 0, p_networkManager.OnMatchJoined);
		StartCoroutine (waitForJoin (seconds));
	}

	IEnumerator waitForJoin(int l_waitCounter){
		while (l_waitCounter > 0) {
			showErrorOnChatList ("Joining... (" + l_waitCounter + ")");
			yield return new WaitForSeconds (1f);
			l_waitCounter--;
		}
		showErrorOnChatList ("Cant connect, refreshing chats...");
		yield return new WaitForSeconds (1f);

		MatchInfo matchInfo = p_networkManager.matchInfo;
		if (matchInfo != null) {
			p_networkManager.matchMaker.DropConnection (matchInfo.networkId, matchInfo.nodeId, 0, p_networkManager.OnDropConnection);
			p_networkManager.StopClient ();
		}
		yield return new WaitForSeconds (2f);
		RefreshChatList ();
	}

	#endregion

	#region Finding chat by name and connecting to it : 

	public void SetSearchName(string _input){
		p_searchingName = _input;
	}

	public void SearchForChat(){
		if (p_networkManager.matchMaker == null) {
			p_networkManager.StartMatchMaker ();
		}
		showErrorOnChatList ("Searching for room : " + p_searchingName);
		p_networkManager.matchMaker.ListMatches (0, 20, p_searchingName, true, 0, 0, OnMatchSearch);
	}

	void OnMatchSearch(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList){
		if (!success || matchList == null) {
			showErrorOnChatList ("Cant get chatRooms");
			return;
		}

		if (matchList.Count < 1) {
			showErrorOnChatList ("Cant find Chat!");
			return;
		}

		joinRoom (matchList [0], 15);
	}

	#endregion

	#region Quiting :

	public void Quit_ButtonClick(){
		Application.Quit ();
	}

	#endregion
}
