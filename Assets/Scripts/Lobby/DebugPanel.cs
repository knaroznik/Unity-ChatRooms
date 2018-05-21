using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour {

	[SerializeField] private GameObject i_DebugPanel;
	private Text DebugText;

	private static DebugPanel instance;

	public static DebugPanel GetInstance(){
		return instance;
	}

	void Awake(){
		instance = this;
		DebugText = i_DebugPanel.transform.GetChild (0).GetComponent<Text> ();
	}

	public void Report(string reportedText){
		StartCoroutine (reportText (reportedText));
	}

	private IEnumerator reportText(string reportedText){
		DebugText.text = reportedText;
		i_DebugPanel.SetActive (true);
		yield return new WaitForSeconds (3);
		i_DebugPanel.SetActive (false);
	}
}
