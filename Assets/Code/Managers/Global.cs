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

    public static List<GameObject> players;
    public static List<GameObject> leprechauns;
    public static List<GameObject> drinks;
    public static List<int> playerCharacter;

    public static List<GameObject> environmentGlasses;
    public static List<GameObject> environmentPaintings;

    public static Dictionary<GameObject, string> lepGOlepScript;

    public static World WorldObject;
    public static GameElementManager GameElements;

    public static int Screen_CharacterSelect = 0, Screen_MainGame = 1;

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
    }

    public static void EarlyStart()
    {        
        playerCharacter = new List<int>();
    }
    
    static public void Reset()
    {        
        GAME_RESET = false;
        GAME_END = false;
        LATE_START = false;
        
    }

    static public void SetNumberOfPlayers(int nr)
    {
        Global.NumberOfPlayers = nr;
    }

    static public void AddSelectedCharacter(int i)
    {
        Global.playerCharacter.Add(i);
    }

    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

}
