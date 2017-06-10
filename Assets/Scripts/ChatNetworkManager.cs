using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChatNetworkManager : NetworkManager {

	public override void OnServerDisconnect (NetworkConnection conn)
	{
		string name = conn.playerControllers [0].gameObject.name;
		ClientScene.localPlayers [0].gameObject.GetComponent<NetworkUser> ().CmdDeleteDisconnectedUser (name);
		base.OnServerDisconnect (conn);
	}
}
