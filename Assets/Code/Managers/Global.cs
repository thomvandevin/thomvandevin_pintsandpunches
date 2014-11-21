using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;
using UnityEditor;

public class Global : MonoBehaviour {

    public static int NumberOfPlayers;
    public static bool enableDrinks = false;

    [HideInInspector]

    public static bool GAME_END;
    public static bool GAME_RESET = false;

    public static List<GameObject> players;
    public static List<GameObject> leprechauns;
    public static List<GameObject> drinks;
    public static List<int> playerCharacter;

    public static Dictionary<GameObject, string> lepGOlepScript;

    public static World WorldObject;

    public static int Screen_CharacterSelect = 0, Screen_MainGame = 1;

    void Awake()
    {
        DontDestroyOnLoad(this);
        Reset();
    }

    void OnLevelWasLoaded()
    {
        Reset();
    }
    
    static public void Reset()
    {        
        GAME_RESET = false;
        GAME_END = false;

        playerCharacter = new List<int>();

        if (Application.loadedLevel == Global.Screen_MainGame)
        {
            players = new List<GameObject>();
            leprechauns = new List<GameObject>();
            drinks = new List<GameObject>();

            lepGOlepScript = new Dictionary<GameObject, string>();

            WorldObject = GameObject.FindGameObjectWithTag("Global").AddComponent<World>();
        }

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
