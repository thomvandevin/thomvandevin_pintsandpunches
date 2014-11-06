using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;
using UnityEditor;

public class Global : MonoBehaviour {

    public const int NumberOfPlayers = 2;

    [HideInInspector]

    public static bool GAME_END;
    public static bool GAME_RESET = false;

    public static List<GameObject> players;
    public static List<Leprechaun> leprechauns;
    public static List<GameObject> drinks;

    public static World WorldObject;

    void Awake()
    {
        DontDestroyOnLoad(this);
        Reset();
    }
    
    public void Reset()
    {
        
        GAME_RESET = false;
        GAME_END = false;

        if (EditorApplication.currentScene == "Assets/Content/Resources/Scenes/PintsAndPunches_MainGame.unity")
        {
            players = new List<GameObject>();
            leprechauns = new List<Leprechaun>();
            drinks = new List<GameObject>();

            WorldObject = gameObject.AddComponent<World>();
        }

    }


    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

}
