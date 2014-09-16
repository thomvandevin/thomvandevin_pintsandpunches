using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public static XboxController XboxInput;

    void Awake()
    {
        DontDestroyOnLoad(this);
        Reset();
    }
    
    public static void Reset()
    {
        
        GAME_RESET = false;
        GAME_END = false;

        players = new List<Leprechaun>();
        drinks = new List<Drink>();

        XboxInput = new XboxController();

    }

}
