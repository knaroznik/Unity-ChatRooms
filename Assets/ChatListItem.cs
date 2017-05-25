using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class ChatListItem : MonoBehaviour {

	public void Setup(MatchInfoSnapshot matchInfo){
		this.transform.GetChild (0).gameObject.GetComponent<Text> ().text = matchInfo.name;
	}
}
