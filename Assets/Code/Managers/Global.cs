﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;

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
        if (Application.loadedLevel == Screen_CharacterSelect)
        {
            if(GameObject.FindGameObjectWithTag("Global") != null)
                Destroy(GameObject.FindGameObjectWithTag("Global"));

            //GameObject glob = Instantiate(Resources.Load("Prefabs/Managers/Global")) as GameObject;
        }
        
        if (Application.loadedLevel == Screen_MainGame)
        {
            players = new List<GameObject>();
            leprechauns = new List<GameObject>();
            drinks = new List<GameObject>();

            lepGOlepScript = new Dictionary<GameObject, string>();

            WorldObject = GameObject.FindGameObjectWithTag("Global").AddComponent<World>();

            print("jojojo");
        }

        Reset();
    }

    public static void EarlyStart()
    {        
        playerCharacter = new List<int>();
    }
    
    static public void Reset()
    {        
        GAME_RESET = false;
        GAME_END = false;
        
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
