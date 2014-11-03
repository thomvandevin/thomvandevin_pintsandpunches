using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    [HideInInspector]
    public enum Character
    {
        LEPRECHAUN_01,
        LEPRECHAUN_USA,
        CLURICHAUN,
        FAR_DARRIG
    }

    private Character chosenCharacter;
    private string loadCharacterString;

    public int controllerNumber;
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

    public void SetPlayer(int controlNumber, Character chosenChar)
    {
        controllerNumber = controlNumber;
        chosenCharacter = chosenChar;
        switch (chosenCharacter)
        {
            case Character.LEPRECHAUN_01:
                loadCharacterString = "Prefabs/Entities/Leprechaun_01";
                break;
            case Character.LEPRECHAUN_USA:
                loadCharacterString = "Prefabs/Entities/Leprechaun_usa";
                break;
            case Character.CLURICHAUN:
                loadCharacterString = "Prefabs/Entities/Clurichaun_01";
                break;
            case Character.FAR_DARRIG:
                loadCharacterString = "Prefabs/Entities/FarDarrig_01";
                break;
            default:
                loadCharacterString = "Prefabs/Entities/Leprechaun_01";
                break;
        }

        RESET = false;

        playerStartPosition = new Vector2(-10 + (4 * controllerNumber), -1);
        gameObject.transform.position = playerStartPosition;

        leprechaunObject = Instantiate(Resources.Load(loadCharacterString), transform.position, Quaternion.identity) as GameObject;
        leprechaunObject.transform.parent = gameObject.transform;
        leprechaunScript = leprechaunObject.AddComponent<Leprechaun>();
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
        Vector3 deadLepPosition = deadLeprechaun.transform.position;
        Destroy(deadLeprechaun);

        GameObject RespawnAnimation = Instantiate(Resources.Load("Prefabs/Objects/HUD/Respawn_Animation"), transform.position, Quaternion.identity) as GameObject;
        RespawnAnimation.GetComponent<RespawnAnimation>().SetRespawnAnimation(gameObject);

        gameObject.transform.position = deadLepPosition;
        leprechaunObject = Instantiate(Resources.Load(loadCharacterString), transform.position, Quaternion.identity) as GameObject;

        leprechaunObject.transform.parent = gameObject.transform;
        leprechaunScript = leprechaunObject.AddComponent<Leprechaun>();
        leprechaunScript.SetLeprechaun(transform.position, controllerNumber, chosenCharacter, gameObject, kills);
        Global.leprechauns.Add(leprechaunScript);


    }

    public void LateUpdate()
    {
        if (RESET)
            RESET = false;
    }
}
