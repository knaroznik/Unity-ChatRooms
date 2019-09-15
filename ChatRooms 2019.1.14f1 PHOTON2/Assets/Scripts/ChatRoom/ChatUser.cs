using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatUser : MonoBehaviourPunCallbacks
{

    private Text chatText;

    public void Start()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("Write", RpcTarget.All, "<color=#a52a2aff>" + UserAccount.GetUserName() + " joined the room </color>");
        }
    }

    [PunRPC]
    void Write(string message)
    {
        Chat.Instance.Write(message);
        

    }
}
