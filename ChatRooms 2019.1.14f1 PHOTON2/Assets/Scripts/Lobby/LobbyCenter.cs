using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCenter : MonoBehaviourPunCallbacks
{

    private string p_chatName;

    public string ChatName {
        get
        {
            return p_chatName;
        }

        set
        {
            p_chatName = value;
        }
    }


    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            RoomOptions options = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)p_roomSize };
            PhotonNetwork.CreateRoom(ChatName, options);
        }
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(ChatName);
    }

    #region Variables and class Functions : 

    
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
