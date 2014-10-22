using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    [HideInInspector]

    public int controllerNumber, chosenCharacter;
    public bool RESET;
    public Vector2 playerStartPosition;

    //private RespawnFlare respawnFlare;
    
    public GameObject leprechaunObject;
    public Leprechaun leprechaunScript;

    public GameObject playerIndicatorObject;
    public PlayerIndicator playerIndicatorScript;

    public Player()
    {

    }

    public void SetPlayer(int controllerNumber, int chosenCharacter)
    {
        this.controllerNumber = controllerNumber;
        this.chosenCharacter = chosenCharacter;
        this.leprechaunObject = gameObject;
        RESET = false;

        playerStartPosition = new Vector2(-10 + (4 * controllerNumber), -1);

        leprechaunObject.AddComponent<Leprechaun>();
        leprechaunScript = leprechaunObject.GetComponent<Leprechaun>();
        leprechaunScript.SetLeprechaun(playerStartPosition, controllerNumber, chosenCharacter, gameObject, 0);
        Global.leprechauns.Add(leprechaunScript);

        playerIndicatorObject = Instantiate(Resources.Load("Prefabs/Objects/HUD/Controller_indicator"), new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity) as GameObject;
        playerIndicatorScript = playerIndicatorObject.GetComponent<PlayerIndicator>();
        playerIndicatorScript.SetIndicator(gameObject, controllerNumber);

        //gridPos = leprechaun.GetGridPosition();
        //respawnFlare = new RespawnFlare(leprechaun.GetPosition, controllerNumber);

    }

    public void Update()
    {
        //leprechaunScript.Update();
        //respawnFlare.Update();
    }

    public void ResetPlayer(GameObject deadLeprechaun, int kills)
    {
        Destroy(gameObject.GetComponent<Leprechaun>());
        leprechaunScript = leprechaunObject.GetComponent<Leprechaun>();
        leprechaunScript.SetLeprechaun(playerStartPosition, controllerNumber, chosenCharacter, gameObject, kills);
    }

    public void LateUpdate()
    {
        if (RESET)
            RESET = false;
    }
}
