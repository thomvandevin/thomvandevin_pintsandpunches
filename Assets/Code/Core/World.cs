using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    [HideInInspector]


    public Vector2 drinkPos;
    public int drinkTileRand;
    private int drinkTimer;

    public int numberOfPlayers = Global.NumberOfPlayers;

    private Player player;

	// Use this for initialization
    void Start()
    {

        for(int i = 1; i <= numberOfPlayers; i++)
        {
            GameObject playerObject = Instantiate(Resources.Load("Entities/Player_1")) as GameObject;
            playerObject.AddComponent<Player>();
            Player playerScript = playerObject.GetComponent<Player>();
            playerScript.SetPlayer(i, 1, playerObject);
            playerObject.layer = 7 + i;
            playerObject.name = "Player " + i.ToString();
            Global.players.Add(playerObject);
        }

        drinkTimer = Random.Range(200, 300);
    }
	
	// Update is called once per frame
	void Update () 
    {
        foreach (GameObject p in Global.players)
        {
            p.GetComponent<Player>().Update();
        }

	}

    public void SpawnDrink()
    {
        drinkTileRand = Random.Range(0,4);
        //drinkPos = Global.tiles_Spawn[drinkTileRand].GetPosition;
        //Drink drink = new Drink(drinkPos);
        //Global.drinks.Add(drink);
    }
}
