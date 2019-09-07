using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ChatNetworkManager : NetworkManager {

	public override void OnServerDisconnect (NetworkConnection conn)
	{
		string name = conn.playerControllers [0].gameObject.name;
		ClientScene.localPlayers [0].gameObject.GetComponent<NetworkUser> ().CmdDeleteDisconnectedUser (name);
		base.OnServerDisconnect (conn);
	}

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
		GameObject player;
		player = (GameObject)Object.Instantiate(this.playerPrefab, Vector3.zero, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}



	public override void OnClientSceneChanged(NetworkConnection conn)
	{
		ClientScene.AddPlayer(conn, 0);
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		//base.OnClientConnect(conn);
	}
}
