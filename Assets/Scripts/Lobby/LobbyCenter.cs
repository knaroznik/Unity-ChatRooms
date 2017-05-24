using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyCenter : MonoBehaviour {

	private string p_chatName;
	private uint p_roomSize = 6;
	private NetworkManager p_networkManager;

	void Start () {
		p_networkManager = NetworkManager.singleton;
		if (p_networkManager.matchMaker == null)
		{
			p_networkManager.StartMatchMaker();
		}
	}

	public void SetChatName (string l_chatName)
	{
		p_chatName = l_chatName;
	}

	public void CreateRoom ()
	{
		if (p_chatName != "" && p_chatName != null) {
			Debug.Log ("Creating Room: " + p_chatName + " with room for " + p_roomSize + " players.");
			p_networkManager.matchMaker.CreateMatch (p_chatName, p_roomSize, true, "", "", "", 0, 0, p_networkManager.OnMatchCreate);
		} else {
			Debug.Log ("Need to pick a name");
		}
	}
}
