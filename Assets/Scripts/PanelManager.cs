using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

	public GameObject ChatPanel;
	public GameObject LoginPanel;

	float height;

	void Start(){
		//Variables 
		height = ChatPanel.GetComponent<RectTransform> ().rect.height;

		//Activity
		ChatPanel.SetActive (true);
		LoginPanel.SetActive (true);

		//Setting up Scene
		ChatPanel.transform.position += new Vector3 (0f, height, 0f);
	}

	public void LogIn(string input){
		if (input != "") {
			StartCoroutine(logInAsCoroutine());
			UserAccount.Username = input;
		}
	}

	IEnumerator logInAsCoroutine(){
		while (Vector3.Distance (Vector3.zero, ChatPanel.GetComponent<RectTransform>().localPosition) > 0.1f) {
			ChatPanel.transform.position -= new Vector3 (0f, height/80, 0f);
			LoginPanel.transform.position -= new Vector3 (0f, height/80, 0f);
			yield return new WaitForEndOfFrame ();
		}
	}

	public void LogOut(){
		StartCoroutine(logOutAsCoroutine());
	}

	IEnumerator logOutAsCoroutine(){
		while (Vector3.Distance (Vector3.zero, LoginPanel.GetComponent<RectTransform>().localPosition) > 0.1f) {
			ChatPanel.transform.position += new Vector3 (0f, height/80, 0f);
			LoginPanel.transform.position += new Vector3 (0f, height/80, 0f);
			yield return new WaitForEndOfFrame ();
		}
	}
}
