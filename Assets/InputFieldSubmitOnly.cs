using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputFieldSubmitOnly : InputField {

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		for (int i = 0; i < this.onEndEdit.GetPersistentEventCount(); ++i) {
			int index = i;
			this.onEndEdit.SetPersistentListenerState(index, UnityEventCallState.Off);
			this.onEndEdit.AddListener(delegate(string text) {
				if (!EventSystem.current.alreadySelecting) {
					((Component)this.onEndEdit.GetPersistentTarget(index)).SendMessage(this.onEndEdit.GetPersistentMethodName(index), text);
				}
			});
		}
	}
}
