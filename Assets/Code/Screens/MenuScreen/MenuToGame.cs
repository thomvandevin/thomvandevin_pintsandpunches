using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using GamepadInput;

public class MenuToGame : MonoBehaviour {

    public List<int> playerCharacter;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () 
    {
        playerCharacter = new List<int>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.One))
        {
            if (Application.loadedLevel == Global.Screen_CharacterSelect)
            {
                LoadMainGame();
            }
            else if (Application.loadedLevel == Global.Screen_MainGame)
            {
                Application.LoadLevel(Global.Screen_CharacterSelect);
                Global.Reset();
            }
            else if (Application.loadedLevel == Global.Screen_WinScreen)
            {
                Application.LoadLevel(Global.Screen_CharacterSelect);
                Global.Reset();
            }

        }

	}

    public void LoadMainGame()
    {
        Global.EarlyStart();
        int players = 0;
        for (int i = 0; i < playerCharacter.Count; i++)
        {
            if (playerCharacter[i] != 0)
                players++;

            Global.AddSelectedCharacter(playerCharacter[i]);
        }

        Global.SetNumberOfPlayers(players);

        Application.LoadLevel(Global.Screen_MainGame);

        //print(playerCharacter[0].ToString() + " " +playerCharacter[1].ToString() + " " +playerCharacter[2].ToString());
    }



}
