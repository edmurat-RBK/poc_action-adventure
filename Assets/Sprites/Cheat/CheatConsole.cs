using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MessageType
{
    LOG,
    INFO,
    WARN,
    ERROR,
    NONE
}

public class ConsoleMessage
{
    public string message;
    public MessageType type;

    public ConsoleMessage(MessageType type, string message)
    {
        this.message = message;
        this.type = type;
    }
}

public class CheatConsole : MonoBehaviour
{
    [Header("Console GameObjects")]
    public GameObject historyBox;
    public GameObject textPrefab;
    public InputField inputField;

    [Space(5)]

    [Header("Controls")]
    public KeyCode openConsoleKey = KeyCode.Return;
    public KeyCode sendMesssageKey = KeyCode.Return;
    public KeyCode closeConsoleKey = KeyCode.Escape;
    public KeyCode previousCommandKey = KeyCode.UpArrow;
    public KeyCode nextCommandKey = KeyCode.DownArrow;

    [Space(5)]

    [Header("Parameters")]
    [Range(25, 500)] public int maximumHistoryLength = 25;

    [Space(5)]

    [Header("Colors")]
    public Color logColor;
    public Color infoColor;
    public Color warnColor;
    public Color errorColor;
    public Color undefinedColor;

    private GameObject console;
    private bool consoleOpen = false;
    private List<GameObject> messageList = new List<GameObject>();
    private List<string> commandHistory = new List<string>() { "" };
    private int indexHistory = 0;
    

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
                inputField.ActivateInputField();
            }
        }
        else
        {
            if(Input.GetKeyDown(closeConsoleKey))
            {
                consoleOpen = false;
                console.SetActive(false);
            }

            if(Input.GetKeyDown(sendMesssageKey) && !inputField.text.Equals(""))
            {
                ParseCommand(inputField.text);
                commandHistory.Add(inputField.text);
                indexHistory = commandHistory.Count;
                inputField.text = "";
                inputField.ActivateInputField();
            }

            if(Input.GetKeyDown(previousCommandKey))
            {
                indexHistory--;
                if(indexHistory < 0)
                {
                    indexHistory = 0;
                }
                GetCommandFromHistory();
            }

            if (Input.GetKeyDown(nextCommandKey))
            {
                indexHistory++;
                if (indexHistory > commandHistory.Count)
                {
                    indexHistory = commandHistory.Count;
                }
                GetCommandFromHistory();
            }
        }
    }

    private void AddMessage(ConsoleMessage message)
    {
        GameObject instance = Instantiate(textPrefab, historyBox.transform);
        instance.GetComponent<Text>().text = message.message;
        switch(message.type)
        {
            case MessageType.LOG:
                instance.GetComponent<Text>().color = logColor;
                break;

            case MessageType.INFO:
                instance.GetComponent<Text>().color = infoColor;
                break;

            case MessageType.WARN:
                instance.GetComponent<Text>().color = warnColor;
                break;

            case MessageType.ERROR:
                instance.GetComponent<Text>().color = errorColor;
                break;

            default:
                instance.GetComponent<Text>().color = undefinedColor;
                break;
        }
        messageList.Add(instance);

        RemoveMessage();
    }

    private void RemoveMessage()
    {
        while(messageList.Count > maximumHistoryLength)
        {
            Destroy(messageList[0]);
            messageList.RemoveAt(0);
        }
    }

    private void GetCommandFromHistory()
    {
        if(indexHistory < commandHistory.Count)
        {
            inputField.text = commandHistory[indexHistory];
        }
        else
        {
            inputField.text = "";
        }
    }

    private void ParseCommand(string cmd)
    {
        string[] word = cmd.Split(' ');
        switch(word[0])
        {
            case "help":
                commandHelp(word);
                break;

            case "say":
                commandSay(word);
                break;

            case "random":
                commandRandom(word);
                break;

            default:
                AddMessage(new ConsoleMessage(MessageType.ERROR, word[0]+" : Unknown command"));
                break;
        }
    }

    public void commandHelp(string[] args)
    {
        if(args.Length == 1)
        {
            string printHelp;
            printHelp = 
                "---- COMMANDS ----\n" +
                "help :\n"+
                "Display a list of available commands\n"+
                "\n"+
                "help <command> :\n"+
                "Display information about the command\n"+
                "\n"+
                "random :\n"+
                "Give a random number between 0 and 100\n"+
                "\n" +
                "random <max> :\n" +
                "Give a random number between 0 and <max>\n" + 
                "\n" +
                "random <min> <max> :\n" +
                "Give a random number between <min> and <max>\n" +
                "\n" +
                "say <message> :\n" +
                "Print the message in the console\n";
            AddMessage(new ConsoleMessage(MessageType.INFO, printHelp));
        }
        else if(args.Length == 2)
        {
            switch(args[1])
            {
                case "help":
                    string printHelp;
                    printHelp =
                        "--- HELP ---\n" +
                        "help :\n" +
                        "You are stupid...\n"+
                        "\n"+
                        "Synthax : help\n"+
                        "          help <command>\n";
                    AddMessage(new ConsoleMessage(MessageType.INFO, printHelp));
                    break;

                case "say":
                    string printSay;
                    printSay =
                        "--- HELP ---\n" +
                        "say :\n" +
                        "Print a message in console\n" +
                        "\n" +
                        "Synthax : say <message>\n";
                    AddMessage(new ConsoleMessage(MessageType.INFO, printSay));
                    break;

                case "random":
                    string printRandom;
                    printRandom =
                        "--- HELP ---\n" +
                        "random :\n" +
                        "Give random number, just for fun\n" +
                        "\n" +
                        "Synthax : random\n" +
                        "          random <max>\n"+
                        "          random <min> <max>\n";
                    AddMessage(new ConsoleMessage(MessageType.INFO, printRandom));
                    break;

                default:
                    AddMessage(new ConsoleMessage(MessageType.ERROR, "\""+args[1]+"\" is not a valid command"));
                    break;
            }
        }
        else
        {
            AddMessage(new ConsoleMessage(MessageType.ERROR, "help : Bad usage"));
        }
    }

    public void commandSay(string[] args)
    {
        if(args.Length > 1)
        {
            string printString = args[1];
            for(int i=2; i<args.Length; i++)
            {
                printString += " " + args[i];
            }
            AddMessage(new ConsoleMessage(MessageType.INFO, printString));
        }
        else
        {
            AddMessage(new ConsoleMessage(MessageType.ERROR, "say : Bad usage"));
        }
    }

    public void commandRandom(string[] args)
    {
        Random randomObject = new Random();
        if(args.Length == 1)
        {
            int rdm = Random.Range(0, 101);
            AddMessage(new ConsoleMessage(MessageType.INFO, "Random[0,100] : " + rdm));
        }
        else if(args.Length == 2)
        {
            try
            {
                int maxRange = int.Parse(args[1]);
                int rdm = Random.Range(0, maxRange);
                AddMessage(new ConsoleMessage(MessageType.INFO, "Random[0,"+maxRange+"] : " + rdm));
            }
            catch
            {
                AddMessage(new ConsoleMessage(MessageType.ERROR, "random : Bad Usage"));
            }
        }
        else if(args.Length == 3)
        {
            try
            {
                int minRange = int.Parse(args[1]);
                int maxRange = int.Parse(args[2]);
                int rdm = Random.Range(minRange, maxRange);
                AddMessage(new ConsoleMessage(MessageType.INFO, "Random[" + minRange + "," + maxRange + "] : " + rdm));
            }
            catch
            {
                AddMessage(new ConsoleMessage(MessageType.ERROR, "random : Bad Usage"));
            }
        }
        else
        {
            AddMessage(new ConsoleMessage(MessageType.ERROR, "random : Bad usage"));
        }
    }
}
