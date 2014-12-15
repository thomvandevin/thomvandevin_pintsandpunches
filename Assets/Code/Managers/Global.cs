using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;

public class Global : MonoBehaviour {

    public static int NumberOfPlayers;
    public static bool enableDrinks = false;

    [HideInInspector]

    public static bool GAME_END = false;
    public static bool GAME_RESET = false;
    public static bool LATE_START = false;

    public static int playerWhoWon = 0;
    public static Player.Character characterThatWon;

    public static List<GameObject> players;
    public static List<GameObject> leprechauns;
    public static List<GameObject> drinks;
    public static List<int> playerCharacter;
    public static bool[] useMpus;
    public static string[] comMpus;

    public static List<GameObject> environmentGlasses;
    public static List<GameObject> environmentPaintings;

    public static Dictionary<GameObject, string> lepGOlepScript;

    public static World WorldObject;
    public static GameElementManager GameElements;

    public static int Screen_CharacterSelect = 0, Screen_MainGame = 1, Screen_WinScreen = 2;

    void Awake()
    {
        DontDestroyOnLoad(this);
        Reset();
    }

    void OnLevelWasLoaded()
    {
        if (Application.loadedLevel == Screen_CharacterSelect)
        {
            if(GameObject.FindGameObjectWithTag("Global") != null)
                Destroy(GameObject.FindGameObjectWithTag("Global"));

        }
        
        if (Application.loadedLevel == Screen_MainGame)
        {
            StartMainGame();
        }
    }

    public static void EarlyStart()
    {        
        playerCharacter = new List<int>();
        useMpus = new bool[4];
    }
    
    static public void Reset()
    {        
        GAME_RESET = false;
        GAME_END = false;
        LATE_START = false;
        
    }

    public static void StartMainGame()
    {
        players = new List<GameObject>();
        leprechauns = new List<GameObject>();
        drinks = new List<GameObject>();
        environmentGlasses = new List<GameObject>();
        environmentPaintings = new List<GameObject>();

        lepGOlepScript = new Dictionary<GameObject, string>();

        WorldObject = GameObject.FindGameObjectWithTag("Global").AddComponent<World>();
        GameElements = GameObject.FindGameObjectWithTag("Global").AddComponent<GameElementManager>();

        LATE_START = true;
    }

    public static void StartMainGameWOPlayers()
    {
        players = new List<GameObject>();
        leprechauns = new List<GameObject>();
        drinks = new List<GameObject>();
        environmentGlasses = new List<GameObject>();
        environmentPaintings = new List<GameObject>();

        lepGOlepScript = new Dictionary<GameObject, string>();

        //GameElements = GameObject.FindGameObjectWithTag("Global").AddComponent<GameElementManager>();

        LATE_START = true;
    }

    static public void SetNumberOfPlayers(int nr)
    {
        Global.NumberOfPlayers = nr;
    }

    static public void AddSelectedCharacter(int i)
    {
        Global.playerCharacter.Add(i);
    }

    static public void SetMPUs(bool[] mpus, string[] coms)
    {
        Global.useMpus = mpus;
        Global.comMpus = coms;
    }

    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

}
