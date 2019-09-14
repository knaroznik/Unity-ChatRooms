using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatUser : MonoBehaviourPunCallbacks
{

    private Text chatText;

    public void Init(Text _chatText)
    {
        chatText = _chatText;
        photonView.RPC("Write", RpcTarget.All, "<color=#a52a2aff> new User joined the room </color>");
    }

    [PunRPC]
    void Write(string message)
    {
        chatText.text += message;

    }
}
