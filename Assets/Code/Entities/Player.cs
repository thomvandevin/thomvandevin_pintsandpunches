﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{

    [HideInInspector]
    public enum Character
    {
        LEPRECHAUN_01,
        LEPRECHAUN_USA,
        CLUIRICHAUN,
        FAR_DARRIG,
        FAIRY
    }

    public enum CharacterScript
    {
        Leprechaun,
        Leprechaun_USA,
        CLUIRICHAUN,
        FarDarrig,
        Fairy
    }

    public Character chosenCharacter;
    private string loadCharacterString;

    public int controllerNumber, kills;
    public bool RESET, useMpu = false;
    public Vector2 playerStartPosition;

    //private RespawnFlare respawnFlare;

    public GameObject leprechaunObject;
    public Component leprechaunScript;

    public GameObject playerIndicatorObject;
    public PlayerIndicator playerIndicatorScript;
    public GameObject healthHUD;
    public HealthHUD healthHUDScript;
    public MPUController mpuController;

    public bool usePunchbag = false;
    public GameObject punchBag;

    public Player()
    {
    }

    public void SetPlayer(int controlNumber, Character chosenChar, bool mpu, string com)
    {
        controllerNumber = controlNumber;
        chosenCharacter = chosenChar;
        switch (chosenCharacter)
        {
            case Character.LEPRECHAUN_01:
                loadCharacterString = "Prefabs/Entities/Leprechaun_01";
                
                healthHUD = Instantiate(Resources.Load("Prefabs/Objects/HUD/HUD_health_leprechaun01"), new Vector3(-10 + (4 * controllerNumber), -3, -9), Quaternion.identity) as GameObject;
                healthHUD.transform.parent = Camera.main.transform;
                healthHUDScript = healthHUD.GetComponentInChildren<HealthHUD>();
                healthHUDScript.SetHealthHud(controllerNumber);
                break;
            case Character.LEPRECHAUN_USA:
                loadCharacterString = "Prefabs/Entities/Leprechaun_usa";
                
                healthHUD = Instantiate(Resources.Load("Prefabs/Objects/HUD/HUD_health_leprechaunusa"), new Vector3(-10 + (4 * controllerNumber), -3, -9), Quaternion.identity) as GameObject;
                healthHUD.transform.parent = Camera.main.transform;
                healthHUDScript = healthHUD.GetComponentInChildren<HealthHUD>();
                healthHUDScript.SetHealthHud(controllerNumber);
                break;
            case Character.CLUIRICHAUN:
                loadCharacterString = "Prefabs/Entities/Cluirichaun";
                
                healthHUD = Instantiate(Resources.Load("Prefabs/Objects/HUD/HUD_health_cluirichaun"), new Vector3(-10 + (4 * controllerNumber), -3, -9), Quaternion.identity) as GameObject;
                healthHUD.transform.parent = Camera.main.transform;
                healthHUDScript = healthHUD.GetComponentInChildren<HealthHUD>();
                healthHUDScript.SetHealthHud(controllerNumber);
                break;
            case Character.FAR_DARRIG:
                loadCharacterString = "Prefabs/Entities/FarDarrig";
                
                healthHUD = Instantiate(Resources.Load("Prefabs/Objects/HUD/HUD_health_fardarrig"), new Vector3(-10 + (4 * controllerNumber), -3, -9), Quaternion.identity) as GameObject;
                healthHUD.transform.parent = Camera.main.transform;
                healthHUDScript = healthHUD.GetComponentInChildren<HealthHUD>();
                healthHUDScript.SetHealthHud(controllerNumber);
                break;
            case Character.FAIRY:
                loadCharacterString = "Prefabs/Entities/Fairy";
                
                healthHUD = Instantiate(Resources.Load("Prefabs/Objects/HUD/HUD_health_fairy"), new Vector3(-10 + (4 * controllerNumber), -3, -9), Quaternion.identity) as GameObject;
                healthHUD.transform.parent = Camera.main.transform;
                healthHUDScript = healthHUD.GetComponentInChildren<HealthHUD>();
                healthHUDScript.SetHealthHud(controllerNumber);
                break;
            default:
                loadCharacterString = "Prefabs/Entities/Leprechaun_01";
                
                healthHUD = Instantiate(Resources.Load("Prefabs/Objects/HUD/HUD_health_leprechaun01"), new Vector3(-10 + (5 * controllerNumber), -3, -9), Quaternion.identity) as GameObject;
                healthHUD.transform.parent = Camera.main.transform;
                healthHUDScript = healthHUD.GetComponentInChildren<HealthHUD>();
                healthHUDScript.SetHealthHud(controllerNumber);
                print("You done goofed hard.");
                break;
        }

        RESET = false;
        useMpu = mpu;
        if (useMpu)
        {
            COMParser compars = gameObject.AddComponent<COMParser>();
            compars.com = com;
            mpuController = gameObject.AddComponent<MPUController>();
        }
        kills = 0;



        playerStartPosition = new Vector3(-10 + (4 * controllerNumber), 0, (-.1f * controllerNumber));
        gameObject.transform.position = playerStartPosition;

        leprechaunObject = Instantiate(Resources.Load(loadCharacterString), transform.position, Quaternion.identity) as GameObject;
        leprechaunObject.transform.parent = gameObject.transform;
        switch (chosenCharacter)
        {
            case Character.LEPRECHAUN_01:
                leprechaunScript = leprechaunObject.AddComponent<Leprechaun>();
                Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                leprechaunScript.GetComponent<Leprechaun>().SetLeprechaun(playerStartPosition, controllerNumber, chosenCharacter, gameObject);
                break;
            case Character.LEPRECHAUN_USA:
                leprechaunScript = leprechaunObject.AddComponent<Leprechaun_USA>();
                Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                leprechaunScript.GetComponent<Leprechaun_USA>().SetLeprechaun_USA(playerStartPosition, controllerNumber, chosenCharacter, gameObject);
                break;
            case Character.CLUIRICHAUN:
                leprechaunScript = leprechaunObject.AddComponent<Cluirichaun>();
                Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                leprechaunScript.GetComponent<Cluirichaun>().SetCluirichaun(playerStartPosition, controllerNumber, chosenCharacter, gameObject);
                gameObject.GetComponent<BoxCollider2D>().size = leprechaunScript.GetComponent<Cluirichaun>().bodyCheck.GetComponent<BoxCollider2D>().size;
                gameObject.GetComponent<BoxCollider2D>().center = leprechaunScript.GetComponent<Cluirichaun>().bodyCheck.GetComponent<BoxCollider2D>().center;
                break;
            case Character.FAR_DARRIG:
                leprechaunScript = leprechaunObject.AddComponent<FarDarrig>();
                Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                leprechaunScript.GetComponent<FarDarrig>().SetFarDarrig(playerStartPosition, controllerNumber, chosenCharacter, gameObject);
                gameObject.GetComponent<BoxCollider2D>().size = leprechaunScript.GetComponent<FarDarrig>().bodyCheck.GetComponent<BoxCollider2D>().size;
                gameObject.GetComponent<BoxCollider2D>().center = leprechaunScript.GetComponent<FarDarrig>().bodyCheck.GetComponent<BoxCollider2D>().center;
                break;
            case Character.FAIRY:
                leprechaunScript = leprechaunObject.AddComponent<Fairy>();
                Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                leprechaunScript.GetComponent<Fairy>().SetFairy(playerStartPosition, controllerNumber, chosenCharacter, gameObject);
                gameObject.GetComponent<BoxCollider2D>().size = leprechaunScript.GetComponent<Fairy>().bodyCheck.GetComponent<BoxCollider2D>().size;
                gameObject.GetComponent<BoxCollider2D>().center = leprechaunScript.GetComponent<Fairy>().bodyCheck.GetComponent<BoxCollider2D>().center;
                break;
            default:
                break;
        }

        Global.leprechauns.Add(leprechaunObject);

        Vector3 playerIndPos = new Vector3(transform.position.x, transform.position.y + 1, 0);
        playerIndicatorObject = Instantiate(Resources.Load("Prefabs/Objects/HUD/Controller_indicator"), playerIndPos, Quaternion.identity) as GameObject;
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

    public void ResetPlayer(GameObject deadLeprechaun)
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
                Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                leprechaunScript.GetComponent<Leprechaun>().SetLeprechaun(deadLepPosition, controllerNumber, chosenCharacter, gameObject);
                break;
            case Character.LEPRECHAUN_USA:
                leprechaunScript = leprechaunObject.AddComponent<Leprechaun_USA>();
                Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                leprechaunScript.GetComponent<Leprechaun_USA>().SetLeprechaun_USA(deadLepPosition, controllerNumber, chosenCharacter, gameObject);
                break;
            case Character.CLUIRICHAUN:
                leprechaunScript = leprechaunObject.AddComponent<Cluirichaun>();
                Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                leprechaunScript.GetComponent<Cluirichaun>().SetCluirichaun(deadLepPosition, controllerNumber, chosenCharacter, gameObject);
                break;
            case Character.FAR_DARRIG:
                leprechaunScript = leprechaunObject.AddComponent<FarDarrig>();
                Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                leprechaunScript.GetComponent<FarDarrig>().SetFarDarrig(deadLepPosition, controllerNumber, chosenCharacter, gameObject);
                break;
            case Character.FAIRY:
                leprechaunScript = leprechaunObject.AddComponent<Fairy>();
                Global.lepGOlepScript.Add(leprechaunObject, leprechaunScript.ToString());
                leprechaunScript.GetComponent<Fairy>().SetFairy(deadLepPosition, controllerNumber, chosenCharacter, gameObject);
                break;
            default:
                break;
        }
        Global.leprechauns.Add(leprechaunObject);

    }

    public GameObject GetCollisionObject(string collisionName, GameObject leprechaun)
    {
        if (leprechaun.GetComponent<Leprechaun>() != null)
        {
            Leprechaun script = leprechaunScript.GetComponent<Leprechaun>();

            if (collisionName == "groundCheck")
                return script.groundCheck;
            else if (collisionName == "punchCheck")
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
        else if (leprechaun.GetComponent<Cluirichaun>() != null)
        {
            Cluirichaun script = leprechaunScript.GetComponent<Cluirichaun>();

            if (collisionName == "groundCheck")
                return script.groundCheck;
            else if (collisionName == "punchCheck")
                return script.punchCheck;
            else if (collisionName == "bodyCheck")
                return script.bodyCheck;
            else if (collisionName == "wallCheck")
                return script.wallCheck;
        }
        else if (leprechaun.GetComponent<FarDarrig>() != null)
        {
            FarDarrig script = leprechaunScript.GetComponent<FarDarrig>();

            if (collisionName == "groundCheck")
                return script.groundCheck;
            else if (collisionName == "punchCheck")
                return script.punchCheck;
            else if (collisionName == "bodyCheck")
                return script.bodyCheck;
            else if (collisionName == "wallCheck")
                return script.wallCheck;
        }
        else if (leprechaun.GetComponent<Fairy>() != null)
        {
            Fairy script = leprechaunScript.GetComponent<Fairy>();

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
