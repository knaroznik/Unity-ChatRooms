using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour {

	public void Navigate_ToMainPage(){
		SceneManager.LoadScene ("mainNetwork");
	}
}
