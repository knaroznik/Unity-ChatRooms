using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour {

	[SerializeField] Transform i_windowParent;

	GameObject p_actualWindow;

	public void ChangeWindow(GameObject l_newWindow){
		Destroy (p_actualWindow);
		p_actualWindow = Instantiate (l_newWindow, i_windowParent);
	}
}
