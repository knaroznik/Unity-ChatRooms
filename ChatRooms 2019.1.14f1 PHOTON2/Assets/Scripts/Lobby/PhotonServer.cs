using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PhotonServer : MonoBehaviourPunCallbacks
{
    public Text ServerStatusText;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        ServerStatusText.text = "Searching...";
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    
    private void StartGame() //Function for loading into the multiplayer scene.
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("ChatRoom"); //because of AutoSyncScene all players who join the room will also be loaded into the multiplayer scene.
        }
    }

    public override void OnConnectedToMaster() //Callback function for when the first connection is established successfully.
    {
        ServerStatusText.text = "Connected to : " + PhotonNetwork.CloudRegion;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Create room succesfull");
    }

    public override void OnCreateRoomFailed(short returnCode, string message) //callback function for if we fail to create a room. Most likely fail because room name was taken.
    {
        Debug.Log("Failed to create room... name exists already!");
    }

    public override void OnJoinedRoom() //Callback function for when we successfully create or join a room.
    {
        StartGame();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Cannot Join room");
    }
}
