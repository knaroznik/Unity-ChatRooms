using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UI;

public class ChatUserInit : MonoBehaviourPunCallbacks
{
    public Text chatText;
    public string FolderName;
    public string PrefabName;

    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        GameObject localPlayer = PhotonNetwork.Instantiate(Path.Combine(FolderName, PrefabName), Vector3.zero, Quaternion.identity);
        localPlayer.GetComponent<ChatUser>().Init(chatText);
    }
}
