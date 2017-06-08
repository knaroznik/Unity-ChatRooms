using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class ChatListItem : MonoBehaviour {

	JoinRoomDelegate p_joinDelegate;
	MatchInfoSnapshot p_matchInfo;
	int p_seconds;

	public void Setup(MatchInfoSnapshot _matchInfo, JoinRoomDelegate _delegate, int _seconds){
		this.transform.GetChild (0).gameObject.GetComponent<Text> ().text = _matchInfo.name;
		p_matchInfo = _matchInfo;
		p_joinDelegate = _delegate;
		p_seconds = _seconds;
	}

	public void Button_Click(){
		if (!UserAccount.isDefault()) {
			p_joinDelegate.Invoke (p_matchInfo, p_seconds);
		}
	}
}

public delegate void JoinRoomDelegate(MatchInfoSnapshot match, int seconds);
