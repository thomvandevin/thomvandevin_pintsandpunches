using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    [HideInInspector]

    public Leprechaun leprechaun;

    public int controllerNumber, chosenCharacter;
    public bool RESET;
    public Vector2 playerStartPosition;

    private Vector2 position;
    //public Vector2 GetPosition { get { return leprechaun.GetPosition; } set { position = value; } }

    //private Point gridPos;
    //public Point GetGridPosition { get { return gridPos; } }

    //private RespawnFlare respawnFlare;

    public Player(int controllerNumber, int chosenCharacter)
    {
        this.controllerNumber = controllerNumber;
        this.chosenCharacter = chosenCharacter;
        RESET = false;

        playerStartPosition = new Vector2(75 * controllerNumber * 3, 456);
        leprechaun = new Leprechaun(playerStartPosition, controllerNumber, chosenCharacter, this, 0);
        Global.players.Add(leprechaun);

        //gridPos = leprechaun.GetGridPosition();
        //respawnFlare = new RespawnFlare(leprechaun.GetPosition, controllerNumber);
    }

    public void Update()
    {
        leprechaun.Update();
        //respawnFlare.Update();
    }

    public void LateUpdate()
    {
        if (RESET)
            RESET = false;
    }
}
