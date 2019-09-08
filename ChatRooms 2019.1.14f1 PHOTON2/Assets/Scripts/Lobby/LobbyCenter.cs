using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCenter : MonoBehaviourPunCallbacks
{

    public Text ServerStatusText;

    #region PHOTON2
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        ServerStatusText.text = "Searching...";
    }

    public override void OnConnectedToMaster() //Callback function for when the first connection is established successfully.
    {
        ServerStatusText.text = "Connected to : " + PhotonNetwork.CloudRegion;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnCreateRoomFailed(short returnCode, string message) //callback function for if we fail to create a room. Most likely fail because room name was taken.
    {
        Debug.Log("Failed to create room... name exists already!");

    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public override void OnJoinedRoom() //Callback function for when we successfully create or join a room.
    {
        StartGame();
    }
    private void StartGame() //Function for loading into the multiplayer scene.
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("ChatRoom"); //because of AutoSyncScene all players who join the room will also be loaded into the multiplayer scene.
        }
    }

    #endregion

    #region Variables and class Functions : 

    private string p_chatName;
	private uint p_roomSize = 6;
	private List<GameObject> p_chatList = new List<GameObject> ();
	private Transform p_chatListParent;
	private string p_searchingName;

	[SerializeField] private GameObject i_chatListItemPrefab;
	[SerializeField] private GameObject i_chatListTextPrefab;


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
        if (PhotonNetwork.IsConnectedAndReady)
        {
            RoomOptions options = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)p_roomSize };
            PhotonNetwork.CreateRoom(p_chatName, options);
        }
    }

    


    #endregion

    #region All chat list : 

    public void RefreshChatList(){
		ClearChatList ();

	}

	void ClearChatList(){
		foreach (GameObject chat in p_chatList) {
			Destroy (chat);
		}
	}


	#endregion

	#region joining : 

	void joinRoom(){
		
	}

	#endregion

	#region Finding chat by name and connecting to it : 

	public void SetSearchName(string _input){
		p_searchingName = _input;
	}

	public void SearchForChat(){
	}

	#endregion

	#region Quiting :

	public void Quit_ButtonClick(){
		Application.Quit ();
	}

	#endregion
}
