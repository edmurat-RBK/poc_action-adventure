using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatConsole : MonoBehaviour
{
    [Header("Console GameObjects")]
    public GameObject historyBox;
    public GameObject inputBox;

    [Space(5)]

    [Header("Controls")]
    public KeyCode openConsoleKey = KeyCode.Return;
    public KeyCode sendMesssageKey = KeyCode.Return;
    public KeyCode closeConsoleKey = KeyCode.Escape;

    [Space(5)]

    [Header("Parameters")]
    [Range(25, 250)] public int maximumHistoryLength = 25;

    private bool consoleOpen = false;
    private List<string> history = new List<string>();
    private GameObject console;

    private void Start()
    {
        console = transform.GetChild(0).gameObject;
        console.SetActive(false);
    }

    private void Update()
    {
        if(!consoleOpen)
        {
            if(Input.GetKeyDown(openConsoleKey))
            {
                consoleOpen = true;
                console.SetActive(true);
            }
        }
        else
        {
            if(Input.GetKeyDown(closeConsoleKey))
            {
                consoleOpen = false;
                console.SetActive(false);
            }
        }
    }

    private void AddMessage(string message)
    {
        history.Add(message);
    }

    private void RemoveMessage()
    {
        while(history.Count > maximumHistoryLength)
        {
            history.RemoveAt(0);
        }
    }
}
