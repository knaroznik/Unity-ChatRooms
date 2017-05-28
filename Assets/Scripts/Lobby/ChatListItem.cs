using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class ChatListItem : MonoBehaviour {

	JoinRoomDelegate p_joinDelegate;
	MatchInfoSnapshot p_matchInfo;

	public void Setup(MatchInfoSnapshot _matchInfo, JoinRoomDelegate _delegate){
		this.transform.GetChild (0).gameObject.GetComponent<Text> ().text = _matchInfo.name;
		p_matchInfo = _matchInfo;
		p_joinDelegate = _delegate;
	}

	public void Button_Click(){
		p_joinDelegate.Invoke (p_matchInfo);
	}
}

public delegate void JoinRoomDelegate(MatchInfoSnapshot match);
