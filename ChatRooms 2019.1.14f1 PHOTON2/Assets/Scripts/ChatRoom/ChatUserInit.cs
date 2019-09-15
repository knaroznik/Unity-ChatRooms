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

    void Awake()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        GameObject localPlayer = PhotonNetwork.Instantiate(Path.Combine(FolderName, PrefabName), Vector3.zero, Quaternion.identity);
    }
}
