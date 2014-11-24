using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using GamepadInput;
using UnityEditor;

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
            Global.EarlyStart();
            int players = 0;
            for (int i = 0; i < playerCharacter.Count; i++)
            {
                if (playerCharacter[i] != 0)
                    players++;

                Global.AddSelectedCharacter(playerCharacter[i]);
            }

            Global.SetNumberOfPlayers(players);

            Application.LoadLevel("PintsAndPunches_MainGame");

            //print(playerCharacter[0].ToString() + " " +playerCharacter[1].ToString() + " " +playerCharacter[2].ToString());
        }

	}



}
