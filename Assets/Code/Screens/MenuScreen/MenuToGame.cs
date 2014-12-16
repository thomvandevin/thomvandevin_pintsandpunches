using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using GamepadInput;

public class MenuToGame : MonoBehaviour {

    public bool useMPU1, useMPU2, useMPU3, useMPU4;
    public string stringMPU1, stringMPU2, stringMPU3, stringMPU4;
    public bool[] mpus = new bool[4];
    public string[] coms = new string[4];

    public List<GuiArduinoSerialScript> serials;

    public List<int> playerCharacter;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () 
    {
        playerCharacter = new List<int>();

        mpus[0] = useMPU1;
        mpus[1] = useMPU2;
        mpus[2] = useMPU3;
        mpus[3] = useMPU4;
        coms[0] = stringMPU1;
        coms[1] = stringMPU2;
        coms[2] = stringMPU3;
        coms[3] = stringMPU4;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.One) || Input.GetKeyDown(KeyCode.Return))
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
        GameObject.FindGameObjectWithTag("Hardware").GetComponent<GuiArduinoSerialScript>().getSerialPort.Close();

        Global.EarlyStart();
        int players = 0;
        for (int i = 0; i < playerCharacter.Count; i++)
        {
            if (playerCharacter[i] != 0)
                players++;

            Global.AddSelectedCharacter(playerCharacter[i]);
        }

        Global.SetNumberOfPlayers(players);
        Global.SetMPUs(mpus, coms);

        if (players == 0)
        {
            Application.LoadLevel(Global.Screen_CharacterSelect);
            Global.Reset();
        }
        else
            Application.LoadLevel(Global.Screen_MainGame);

        //print(playerCharacter[0].ToString() + " " +playerCharacter[1].ToString() + " " +playerCharacter[2].ToString());
    }

    public void LoadMainGameOnce()
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
        Global.SetMPUs(mpus, coms);

    }



}
