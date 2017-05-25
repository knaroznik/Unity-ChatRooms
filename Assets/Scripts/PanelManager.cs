using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour {

	[SerializeField] private GameObject i_chatPanel;
	[SerializeField] private GameObject i_loginPanel;

	float p_height;

	void Start(){
		
		//Variables 
		p_height = i_chatPanel.GetComponent<RectTransform> ().rect.height;

		//Activity
		i_chatPanel.SetActive (true);
		i_loginPanel.SetActive (true);

		//Setting up Scene
		i_chatPanel.transform.position += new Vector3 (0f, p_height, 0f);
	}

	public void LogIn(string input){
		if (input != "") {
			StartCoroutine(logInAsCoroutine());
			UserAccount.Username = input;
		}
	}

	IEnumerator logInAsCoroutine(){
		while (Vector3.Distance (Vector3.zero, i_chatPanel.GetComponent<RectTransform>().localPosition) > 0.1f) {
			i_chatPanel.transform.position -= new Vector3 (0f, p_height/80, 0f);
			i_loginPanel.transform.position -= new Vector3 (0f, p_height/80, 0f);
			yield return new WaitForEndOfFrame ();
		}
	}

	public void LogOut(){
		StartCoroutine(logOutAsCoroutine());
	}

	IEnumerator logOutAsCoroutine(){
		while (Vector3.Distance (Vector3.zero, i_loginPanel.GetComponent<RectTransform>().localPosition) > 0.1f) {
			i_chatPanel.transform.position += new Vector3 (0f, p_height/80, 0f);
			i_loginPanel.transform.position += new Vector3 (0f, p_height/80, 0f);
			yield return new WaitForEndOfFrame ();
		}
	}

	public void Navigate_ToMainPage(){
		SceneManager.LoadScene ("mainNetwork");
	}
}
