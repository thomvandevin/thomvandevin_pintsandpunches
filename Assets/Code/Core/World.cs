using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    [HideInInspector]


    public Vector2 drinkPos;
    public int drinkTileRand;
    private int drinkTimer;

	// Use this for initialization
    void Start()
    {

        drinkTimer = Random.Range(200, 300);

    }
	
	// Update is called once per frame
	void Update () 
    {
        

	}

    public void SpawnDrink()
    {
        drinkTileRand = Random.Range(0,4);
        //drinkPos = Global.tiles_Spawn[drinkTileRand].GetPosition;
        //Drink drink = new Drink(drinkPos);
        //Global.drinks.Add(drink);
    }
}
