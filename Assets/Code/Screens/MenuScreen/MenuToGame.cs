using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using GamepadInput;
using UnityEditor;

public class MenuToGame : MonoBehaviour {

    public List<int> playerCharacter;

    public int SCPlayer1, SCPlayer2, SCPlayer3, SCPlayer4;

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


            Application.LoadLevel("PintsAndPunches_MainGame");
            Global.SetNumberOfPlayers(2);
        }

        //if(Application.loadedLevel == Global.Screen_MainGame)
        //{
        //}
	}


}
