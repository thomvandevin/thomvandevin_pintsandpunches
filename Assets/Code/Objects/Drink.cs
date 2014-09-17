﻿using UnityEngine;
using System.Collections;

public class Drink : MonoBehaviour {

    public Global.SortOfDrink drinkType;

    //public DrinkFlare flare;
    public bool showFlare;
    public int randomDrink, flareTimer;
    public string flareType;

    public Vector2 velocity;
    public float gravity;
    //public SoundEffect sfx_drink_floor;

    private bool isGrabbed;
    public bool IsGrabbed { get { return isGrabbed; } set { isGrabbed = value; } }

	// Use this for initialization
	void Start () {
        randomDrink = Random.Range(0,4);
        flareTimer = Random.Range(20, 100);

        velocity = new Vector2(0, 4);
        gravity = 0.8f;

        //drinkCollision_main = new Rectangle((int)GetPosition.X - 16, (int)GetPosition.Y - 32, 32, 64);

        switch (randomDrink)
        {
            case 0:
                drinkType = Global.SortOfDrink.ALE;
                break;
            case 1:
                drinkType = Global.SortOfDrink.CIDER;
                break;
            case 2:
                drinkType = Global.SortOfDrink.STOUT;
                break;
            case 3:
                drinkType = Global.SortOfDrink.WHISKEY;
                break;
            default:
                drinkType = Global.SortOfDrink.ALE;
                break;
        }

        //sfx_drink_floor = Engine.Loader.SoundEffects["Drink_floor_" + Engine.Rand.Next(1, 4).ToString()];

        //flare = new Flare(transform.position);
        showFlare = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (showFlare)
        {
            //if (flare.getAnimation().GetAnimation.FrameIndex >= flare.numberOfFrames - 1)
            //{
            //    //flare.getAnimation().GetAnimation.Stop();
            //    flareTimer = Random.Range(120, 300);
            //    showFlare = false;
            //}
        }
        else if (!showFlare)
        {
            flareTimer--;

            if (flareTimer == 0)
            {
                showFlare = true;
                //flare.startFlare(flareType);
            }
        }

        //flare.Update();
        //flare.GetPosition = GetPosition;

        //if(showFlare)
        //  flare.renderer.enabled = true;
        //else
        //  flare.renderer.enabled = false;
	}
}
