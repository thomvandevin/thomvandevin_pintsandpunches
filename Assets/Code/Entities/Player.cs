using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    [HideInInspector]

    public int controllerNumber, chosenCharacter;
    public bool RESET;
    public Vector2 playerStartPosition;

    private Vector2 position;
    //public Vector2 GetPosition { get { return leprechaun.GetPosition; } set { position = value; } }

    //private Point gridPos;
    //public Point GetGridPosition { get { return gridPos; } }

    //private RespawnFlare respawnFlare;
    
    public GameObject leprechaunObject;
    public Leprechaun leprechaunScript;

    public Player()
    {

    }

    public void SetPlayer(int controllerNumber, int chosenCharacter, GameObject playerObject)
    {
        this.controllerNumber = controllerNumber;
        this.chosenCharacter = chosenCharacter;
        this.leprechaunObject = playerObject;
        RESET = false;

        playerStartPosition = new Vector2(-10 + (4 * controllerNumber), -1);

        leprechaunObject.AddComponent<Leprechaun>();
        leprechaunScript = leprechaunObject.GetComponent<Leprechaun>();
        leprechaunScript.SetLeprechaun(playerStartPosition, controllerNumber, chosenCharacter, playerObject, 0);
        Global.leprechauns.Add(leprechaunScript);

        //gridPos = leprechaun.GetGridPosition();
        //respawnFlare = new RespawnFlare(leprechaun.GetPosition, controllerNumber);

    }

    public void Update()
    {
        //leprechaunScript.Update();
        //respawnFlare.Update();
    }

    public void LateUpdate()
    {
        if (RESET)
            RESET = false;
    }
}
