using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;

public class RoomConnection {

	private bool g_canCreate = false;
	private string g_chatName ="";

	public IEnumerator CreateRoom(string chatName, NetworkManager networkManager, uint roomSize){
		if (chatName != "" && chatName != null) {
			g_chatName = chatName;

			yield return networkManager.matchMaker.ListMatches (0, 20, "", true, 0, 0, OnCreationList);

			if(g_canCreate){
				networkManager.matchMaker.CreateMatch (chatName, roomSize, 
					true, "", "", "", 0, 0, networkManager.OnMatchCreate);
			}
		}
	}

	void OnCreationList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList){
		for (int i = 0; i < matchList.Count; i++) {
			if (matchList [i].name == g_chatName) {
				g_canCreate = false;
				return;
			}
		}
		g_canCreate = true;
	}
}
