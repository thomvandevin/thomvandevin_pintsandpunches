using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    [HideInInspector]

    public Vector2 drinkPos;
    private int drinkTimer;

	// Use this for initialization
    void Start()
    {

        for (int i = 1; i <= Global.NumberOfPlayers; i++)
        {
            GameObject playerObject = Instantiate(Resources.Load("Prefabs/Entities/Player")) as GameObject;
            Player playerScript = playerObject.AddComponent<Player>();
            playerScript.SetPlayer(i, Player.Character.LEPRECHAUN_01);
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

        foreach (GameObject d in Global.drinks)
        {
            d.GetComponent<Drink>().Update();
        }

        if (Global.drinks.Count < 4)
        {
            drinkTimer--;
            if (drinkTimer <= 0)
                SpawnDrink();
        }

	}

    public void GameWon(int controllerNumber)
    {
        print("Player " + controllerNumber.ToString() + " won!");
    }

    public void SpawnDrink()
    {
        //drinkTileRand = Random.Range(0,4);
        drinkPos = new Vector2(Random.Range(-5, 5), 0);

        GameObject drink = Instantiate(Resources.Load("Prefabs/Objects/Drinks/Pickup_Drink"), drinkPos, Quaternion.identity) as GameObject;
        Global.drinks.Add(drink);
        
        Vector2 randomForce = new Vector2(Random.Range(-200, 200), Random.Range(100, 200));
        drink.rigidbody2D.AddForce(randomForce);
        
        drinkTimer = Random.Range(400, 500);
    }
}
