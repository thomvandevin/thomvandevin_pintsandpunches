using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    [HideInInspector]


    public Vector2 drinkPos;
    public int drinkTileRand;
    private int drinkTimer;

    public GameObject player1Object;
    public Player player1Script;

	// Use this for initialization
    void Start()
    {
        drinkTimer = Random.Range(200, 300);
        player1Object = Instantiate(Resources.Load("Entities/Player_1")) as GameObject;
        player1Object.AddComponent<Player>();
        player1Script = player1Object.GetComponent<Player>();
        player1Script.SetPlayer(1, 1, player1Object);
    }
	
	// Update is called once per frame
	void Update () 
    {
        player1Script.Update();

	}

    public void SpawnDrink()
    {
        drinkTileRand = Random.Range(0,4);
        //drinkPos = Global.tiles_Spawn[drinkTileRand].GetPosition;
        //Drink drink = new Drink(drinkPos);
        //Global.drinks.Add(drink);
    }
}
