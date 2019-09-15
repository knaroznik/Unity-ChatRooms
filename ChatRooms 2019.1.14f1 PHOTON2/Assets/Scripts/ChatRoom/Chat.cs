using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public Text chatText;

    public static Chat Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void Write(string message)
    {
        chatText.text += message + "\n";
    }
}
