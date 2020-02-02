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

    private GameObject player;
    private PlayerModifier playerModifier;
    private PlayerHealth playerHealth;
    

    private void Start()
    {
        console = transform.GetChild(0).gameObject;
        console.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        playerModifier = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModifier>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
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
            inputField.MoveTextEnd(false);
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

            case "health":
                commandHealth(word);
                break;

            case "modifier":
                commandModifier(word);
                break;

            case "tp":
                commandTp(word);
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
                "health add <value>:\n" +
                "Add health point" +
                "\n" +
                "health remove <value>:\n" +
                "Remove health point" +
                "\n" +
                "health set <value>:\n" +
                "Set health point" +
                "\n" +
                "modifier <modifier> set <value> :\n" +
                "Modify a modifier value"+
                "\n"+
                "modifier <modifier> reset :\n" +
                "Restore a modifier to default value" +
                "\n" +
                "help :\n" +
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
                "Print the message in the console\n"+
                "\n"+
                "tp <x> <y> :\n"+
                "Teleport player at specified coordinates\n";
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

                case "health":
                    string printHealth;
                    printHealth =
                        "--- HELP ---\n" +
                        "health :\n" +
                        "Change health point value\n" +
                        "\n" +
                        "Synthax : health add <value>\n" +
                        "          health remove <value>\n" +
                        "          health set <value>\n";
                    AddMessage(new ConsoleMessage(MessageType.INFO, printHealth));
                    break;

                case "modifier":
                    string printModifier;
                    printModifier =
                        "--- HELP ---\n" +
                        "modifier :\n" +
                        "Change modifier value\n" +
                        "\n" +
                        "Synthax : modifier <modifier> set <value>\n" +
                        "          modifier <modifier> reset\n";
                    AddMessage(new ConsoleMessage(MessageType.INFO, printModifier));
                    break;

                case "tp":
                    string printTp;
                    printTp =
                        "--- HELP ---\n" +
                        "tp :\n" +
                        "Teleport player at coordinates\n" +
                        "\n" +
                        "Synthax : tp <x> <y>\n";
                    AddMessage(new ConsoleMessage(MessageType.INFO, printTp));
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

    public void commandModifier(string[] args)
    {
        if(args.Length >= 3)
        {
            switch(args[1])
            {
                case "speed":
                    switch(args[2])
                    {
                        case "set":
                            try
                            {
                                float value = float.Parse(args[3]);
                                playerModifier.SpeedModifier = value;
                                AddMessage(new ConsoleMessage(MessageType.INFO, "Player speed modifier set to " + value));
                            }
                            catch
                            {
                                AddMessage(new ConsoleMessage(MessageType.ERROR, "modifier : Bad usage"));
                            }
                            break;

                        case "reset":
                            playerModifier.SpeedModifier = 1.0f;
                            AddMessage(new ConsoleMessage(MessageType.INFO, "Player speed modifier reset"));
                            break;

                        default:
                            AddMessage(new ConsoleMessage(MessageType.ERROR, "modifier : Bad usage"));
                            break;
                    }
                    break;

                default:
                    AddMessage(new ConsoleMessage(MessageType.ERROR, "modifier : Bad usage"));
                    break;
            }
        }
        else
        {
            AddMessage(new ConsoleMessage(MessageType.ERROR, "modifier : Bad usage"));
        }
    }

    public void commandHealth(string[] args)
    {
        if (args.Length == 3)
        {
            switch (args[1])
            {
                case "set":
                    try
                    {
                        int value = int.Parse(args[2]);
                        playerHealth.CurrentHealth = value;
                        AddMessage(new ConsoleMessage(MessageType.INFO, "Player health set to " + value));
                    }
                    catch
                    {
                        AddMessage(new ConsoleMessage(MessageType.ERROR, "health : Bad usage"));
                    }
                    break;

                case "add":
                    try
                    {
                        int value = int.Parse(args[2]);
                        playerHealth.CurrentHealth += value;
                        AddMessage(new ConsoleMessage(MessageType.INFO, "Player health set to " + playerHealth.CurrentHealth));
                    }
                    catch
                    {
                        AddMessage(new ConsoleMessage(MessageType.ERROR, "health : Bad usage"));
                    }
                    break;

                case "remove":
                    try
                    {
                        int value = int.Parse(args[2]);
                        playerHealth.CurrentHealth -= value;
                        AddMessage(new ConsoleMessage(MessageType.INFO, "Player health set to " + playerHealth.CurrentHealth));
                    }
                    catch
                    {
                        AddMessage(new ConsoleMessage(MessageType.ERROR, "health : Bad usage"));
                    }
                    break;

                default:
                    AddMessage(new ConsoleMessage(MessageType.ERROR, "health : Bad usage"));
                    break;
            }
        }
        else
        {
            AddMessage(new ConsoleMessage(MessageType.ERROR, "health : Bad usage"));
        }
    }

    public void commandTp(string[] args)
    {
        if(args.Length == 3)
        {
            try
            {
                int xPos = int.Parse(args[1]);
                int yPos = int.Parse(args[2]);
                player.transform.position = new Vector3(xPos, yPos, 0f);
                AddMessage(new ConsoleMessage(MessageType.INFO, "Player teleported at ("+xPos+", "+yPos+")"));
            }
            catch
            {
                AddMessage(new ConsoleMessage(MessageType.ERROR, "tp : Bad usage"));
            }
        }
        else
        {
            AddMessage(new ConsoleMessage(MessageType.ERROR, "tp : Bad usage"));
        }
    }
}
