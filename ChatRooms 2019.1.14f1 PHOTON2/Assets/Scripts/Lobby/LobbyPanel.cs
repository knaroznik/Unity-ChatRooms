using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanel : MonoBehaviour
{
    private BaseWindowLobby visibleWindow;
    public Transform WindowParent;

    public void SwitchWindow(BaseWindowLobby nextWindow)
    {
        if(visibleWindow != null)
        {
            Destroy(visibleWindow.gameObject);
        }

        visibleWindow = Instantiate(nextWindow, WindowParent);
    }
}
