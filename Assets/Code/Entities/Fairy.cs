﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;
using System;

public class Fairy : Entity
{


    public Fairy()
    {

    }

    public void SetFairy(Vector2 pos, int controllerNumber, Player.Character chosenCharacter, GameObject playerObject)
    {
        maxHealth = 4;//5
        gravity = 0.3f; //0.5f
        //gravityCorrection = 0f;
        //resistance = 2f;
        damageMultiplayer = 1;
        groundRadius = 0.2f;

        attackCooldownMax = 30; //50
        maxDrunkness = 500;
        drunkTimeMultiplier = 300;
        drunkWalkResetTimer = 0;

        velocity = new Vector2(0, 60);//0,60
        maxVelocity = new Vector2(60, 940); //80,840
        drunkVelocity = new Vector2(0, 0);

        dashType = DashTypes.FORWARD;

        #region DONT EDIT
        //#####################
        //#####DONT EDIT#######
        this.controllerNumber = controllerNumber;
        this.chosenCharacter = chosenCharacter;
        this.playerObject = playerObject;

        startingPosition = pos;
        transform.position = startingPosition;
        SetEntity(startingPosition, true);
        //#####DONT EDIT#######
        //#####################
        #endregion

    }



    void FixedUpdate()
    {
        if (!IsDead)
        {
            if (!isDrinking)
            {
                Movement();
            }
        }

        base.FixedUpdate();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

}
