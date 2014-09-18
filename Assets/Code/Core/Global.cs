using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;

public class Global : MonoBehaviour {

    [HideInInspector]

    public enum SortOfDrink
    {
        ALE,
        CIDER,
        STOUT,
        WHISKEY,
        NONE
    };

    public enum PlayerIndex
    {
        PLAYER1,
        PLAYER2,
        PLAYER3,
        PLAYER4
    }

    public static bool GAME_END;
    public static bool GAME_RESET = false;

    public static List<Leprechaun> players;
    public static List<Drink> drinks;

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

        players = new List<Leprechaun>();
        drinks = new List<Drink>();

        WorldObject = gameObject.AddComponent<World>();

    }

}
