using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour {

    [HideInInspector]
    public enum Character
    {
        LEPRECHAUN_01,
        LEPRECHAUN_USA,
        CLURICHAUN,
        FAR_DARRIG
    }

    public enum CharacterScript
    {
        Leprechaun,
        Leprechaun_USA,
        Clurichaun,
        FarDarrig
    }

    public Character chosenCharacter;
    private string loadCharacterString;

    public int controllerNumber;
    public bool RESET;
    public Vector2 playerStartPosition;

    //private RespawnFlare respawnFlare;
    
    public GameObject leprechaunObject;
    public Component leprechaunScript;

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
        switch (chosenCharacter)
        {
            case Character.LEPRECHAUN_01:
                        leprechaunScript = leprechaunObject.AddComponent<Leprechaun>();
                        Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                        leprechaunScript.GetComponent<Leprechaun>().SetLeprechaun(playerStartPosition, controllerNumber, chosenCharacter, gameObject, 0);
                break;
            case Character.LEPRECHAUN_USA:
                        leprechaunScript = leprechaunObject.AddComponent<Leprechaun_USA>();
                        Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                        leprechaunScript.GetComponent<Leprechaun_USA>().SetLeprechaun_USA(playerStartPosition, controllerNumber, chosenCharacter, gameObject, 0);
                break;
            case Character.CLURICHAUN:
                break;
            case Character.FAR_DARRIG:
                break;
            default:
                break;
        }

        Global.leprechauns.Add(leprechaunObject);

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
        switch (chosenCharacter)
        {
            case Character.LEPRECHAUN_01:
                leprechaunScript = leprechaunObject.AddComponent<Leprechaun>();
                leprechaunScript.GetComponent<Leprechaun>().SetLeprechaun(playerStartPosition, controllerNumber, chosenCharacter, gameObject, 0);
                break;
            case Character.LEPRECHAUN_USA:
                leprechaunScript = leprechaunObject.AddComponent<Leprechaun_USA>();
                leprechaunScript.GetComponent<Leprechaun_USA>().SetLeprechaun_USA(playerStartPosition, controllerNumber, chosenCharacter, gameObject, 0);
                break;
            case Character.CLURICHAUN:
                break;
            case Character.FAR_DARRIG:
                break;
            default:
                break;
        }
        Global.leprechauns.Add(leprechaunObject);

    }

    public GameObject GetCollisionObject(string collisionName, GameObject leprechaun)
    {
        if(leprechaun.GetComponent<Leprechaun>() != null)
        {
            Leprechaun script = leprechaunScript.GetComponent<Leprechaun>();

            if (collisionName == "groundCheck")
                return script.groundCheck;
            else if(collisionName == "punchCheck")
                return script.punchCheck;
            else if (collisionName == "bodyCheck")
                return script.bodyCheck;
            else if (collisionName == "wallCheck")
                return script.wallCheck;
        }
        else if (leprechaun.GetComponent<Leprechaun_USA>() != null)
        {
            Leprechaun_USA script = leprechaunScript.GetComponent<Leprechaun_USA>();

            if (collisionName == "groundCheck")
                return script.groundCheck;
            else if (collisionName == "punchCheck")
                return script.punchCheck;
            else if (collisionName == "bodyCheck")
                return script.bodyCheck;
            else if (collisionName == "wallCheck")
                return script.wallCheck;
        }

        return gameObject;
    }

    public Component GetLeprechaunScriptType()
    {
        return leprechaunScript;
    }

    public void LateUpdate()
    {
        if (RESET)
            RESET = false;
    }
}
