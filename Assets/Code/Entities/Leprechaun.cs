using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;

public class Leprechaun : Entity {

    public enum PlayerStates
    {
        WALKING,
        JUMPING,
        IDLE,
        DASHING,
        ATTACKING,
        BLOCKING,
        HIT,
        DEAD,
        DRINKING,
        NONE
    }

    public enum DashTypes
    {
        TRIGGER,
        FORWARD,
        NONE
    }

    public Dictionary<string, bool> dicStringBool;
    public Dictionary<string, PlayerStates> dicStringState;
    public Dictionary<int, GamePad.Index> controllerIndex;
    public GamePad.Index gamePadIndex;

    public Player playerObject;
    public PlayerStates playerState;
    public DashTypes dashType;
    public Global.SortOfDrink sortOfDrink;
    public bool mirrored, isDashCooldown, onGround, skipNextMove, fallTroughBar, underBar, playerStateBool, maxDrunk, checkDrunkTimer;
    public bool isWalking, isDashing, isIdle, isJumping, isAttacking, isHit, isFalling, isBlocking, isDrinking, checkForState;
    public int controllerNumber, chosenCharacter, dashTimer, dashCooldown, punchTimer, playerStateTimer, jumpOnce, attackOnce, currentTile, kills;
    public int drinkAnimCounter, drunkness, maxDrunkness, drunkTimeMultiplier, drunkWalkTimer, drunkWalkResetTimer, drunkRandomSide;
    public string hitDirection, playerStateString;
    public float gravity, gravityCorrection, resistance, drunknessF, damageMultiplayer, groundRadius;
    public Vector2 startingPosition, velocity, previousPosition, maxVelocity, lastVelocity, drunkVelocity, respawnButtonPos;
    public Transform groundCheck;
    public LayerMask groundLayer;
    //public SoundEffect sfx_punch_hit, sfx_punch_miss, sfx_jump_down, sfx_jump, sfx_knockout, sfx_drink, sfx_drink_whiskey;
    //public SoundEffectInstance sfx_drink_whiskey_inst;
    //public RespawnButton respawnButton;
    //public DrunkBubbles drunkBubbles;

    public Animator animator;
    
    public Leprechaun()
    {

    }

    public void SetLeprechaun(Vector2 pos, int controllerNumber, int chosenCharacter, Player playerObject, int killsAmount)
    {
        this.controllerNumber = controllerNumber;

        //playerObject = playerobject;
        kills = killsAmount;

        this.chosenCharacter = chosenCharacter;
        startingPosition = pos;
        mirrored = false;
        onGround = false;
        groundRadius = 0.2f;
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck_player").transform;
        groundLayer = gameObject.GetComponent<LayerMaskPass>().GetLayerMask();
        gravity = 0.5f;
        resistance = 2f;
        velocity = new Vector2(0, 5);
        maxVelocity = new Vector2(3, 900f);
        lastVelocity = new Vector2(0, 2);
        skipNextMove = false;
        fallTroughBar = false;
        underBar = false;
        gravityCorrection = 0f;
        Direction = Facing.RIGHT;
        dashType = DashTypes.FORWARD;
        jumpOnce = 0;
        attackOnce = 0;
        punchTimer = 0;
        hitDirection = "JAWAT";
        sortOfDrink = Global.SortOfDrink.NONE;
        drinkAnimCounter = 0;
        drunkness = 0;
        drunknessF = 0f;
        damageMultiplayer = 0;
        maxDrunkness = 500;
        drunkTimeMultiplier = 300;
        drunkWalkTimer = 0;
        drunkWalkResetTimer = 0;
        maxDrunk = false;
        checkDrunkTimer = true;
        drunkVelocity = new Vector2(0, 0);
        respawnButtonPos = new Vector2(transform.position.x, transform.position.y - 40);
        //respawnButton = new RespawnButton(rbuttonPos);
        //drunkBubbles = new DrunkBubbles(GetPosition, chosenPlayerIndex);

        animator = GetComponent<Animator>();
        transform.position = pos;
        SetEntity(pos, "filler", 30, true);

        isJumping = false;
        isFalling = false;
        isWalking = false;
        isDashing = false;
        isJumping = false;
        isAttacking = false;
        isHit = false;
        isBlocking = false;
        isDrinking = false;
        isIdle = true;

        SetDictionary();

    }

    public void SetDictionary()
    {
        dicStringBool = new Dictionary<string, bool>();
        dicStringBool.Add("isWalking", isWalking);
        dicStringBool.Add("isJumping", isJumping);
        dicStringBool.Add("isIdle", isIdle);
        dicStringBool.Add("isDashing", isDashing);
        dicStringBool.Add("isAttacking", isAttacking);
        dicStringBool.Add("isHit", isHit);
        dicStringBool.Add("IsDead", IsDead);
        dicStringBool.Add("isBlocking", isBlocking);
        dicStringBool.Add("isDrinking", isDrinking);
        
        dicStringState = new Dictionary<string, PlayerStates>();
        dicStringState.Add("isWalking", PlayerStates.WALKING);
        dicStringState.Add("isJumping", PlayerStates.JUMPING);
        dicStringState.Add("isIdle", PlayerStates.IDLE);
        dicStringState.Add("isDashing", PlayerStates.DASHING);
        dicStringState.Add("isAttacking", PlayerStates.ATTACKING);
        dicStringState.Add("isHit", PlayerStates.HIT);
        dicStringState.Add("IsDead", PlayerStates.DEAD);
        dicStringState.Add("isBlocking", PlayerStates.BLOCKING);
        dicStringState.Add("isDrinking", PlayerStates.DRINKING);

        controllerIndex = new Dictionary<int, GamePad.Index>();
        controllerIndex.Add(0, GamePad.Index.Any);
        controllerIndex.Add(1, GamePad.Index.One);
        controllerIndex.Add(2, GamePad.Index.Two);
        controllerIndex.Add(3, GamePad.Index.Three);
        controllerIndex.Add(4, GamePad.Index.Four);
        gamePadIndex = controllerIndex[controllerNumber];
    }

    void FixedUpdate()
    {
        Movement();
    }

	// Update is called once per frame
	public void Update () 
    {
        //if (Global.XboxInput.GetButtonDown(1,0))
        //    Global.GAME_RESET = true;

        if (kills >= 5)
        {
            //Global.WorldObject.gameWon = true;
            //Global.WorldObject.winnerIndex = chosenPlayerIndex;
        }


        if (!IsDead && !isDrinking)
        {
            #region ATTACKING CODE

            if (GamePad.GetButtonDown(GamePad.Button.X, gamePadIndex))
            {
                foreach (Drink d in Global.drinks)
                {
                    //if (playerCollision_main.Intersects(d.drinkCollision_main))
                    //{
                    //    sortOfDrink = d.drinkType;
                    //    //sortOfDrink = Global.SortOfDrink.ALE;
                    //    GetDrunk(sortOfDrink);
                    //    Global.drinks.Remove(d);
                    //    isDrinking = true;
                    //    velocity.X = 0f;
                    //}
                }
            }

            if (punchTimer > 0)
                punchTimer--;
            else
            {
                if (GamePad.GetButtonDown(GamePad.Button.X, gamePadIndex) && !isJumping && !isDashing && attackOnce == 0 && !isDrinking)
                {
                    //playerState = PlayerStates.ATTACKING;
                    isAttacking = true;
                    punchTimer = 24;
                    attackOnce++;

                    bool didHit = false;

                    foreach (Leprechaun lep in Global.players)
                    {
                        //if (playerCollision_attack.Intersects(lep.playerCollision_main) && lep != this)
                        //{
                        //    lep.GotHit(GetPosition, damageMultiplayer, chosenPlayerIndex);
                        //    didHit = true;
                        //}
                    }

                    if (!didHit)
                    {
                        //sfx_punch_miss = Engine.Loader.SoundEfisJumoingfects["Punch_Miss_0" + Engine.Rand.Next(1, 6).ToString()];
                        //sfx_punch_miss.Play();
                    }
                    else
                    {
                        //sfx_punch_hit = Engine.Loader.SoundEffects["Punch_Hit_0" + Engine.Rand.Next(1, 6).ToString()];
                        //sfx_punch_hit.Play();
                    }
                }
            }

            if (GamePad.GetButtonUp(GamePad.Button.X, gamePadIndex))
            {
                if (attackOnce >= 1)
                    attackOnce = 0;

                isAttacking = false;
            }

            #endregion

            #region BLOCKING CODE

            if (GamePad.GetButtonDown(GamePad.Button.B, gamePadIndex) && !isJumping && !isDashing && !isAttacking && !isDrinking)
            {
                isBlocking = true;
                if (isWalking)
                    isWalking = false;
            }

            if (GamePad.GetButtonUp(GamePad.Button.B, gamePadIndex))
                isBlocking = false;

            #endregion

            #region DASHING CODE
            if ((GamePad.GetTrigger(GamePad.Trigger.LeftTrigger, gamePadIndex) > 0.1 || GamePad.GetTrigger(GamePad.Trigger.LeftTrigger, gamePadIndex) > 0.1)
                && !isDashing && !isDashCooldown)
            {
                isDashing = true;
                dashTimer = 0;
                //animation.GetAnimation.Start();
            }

            if (isDashing)
            {
                if (dashTimer > 10)
                {
                    dashCooldown = 120;
                    isDashCooldown = true;
                    isDashing = false;
                }

                //playerState = PlayerStates.DASHING;
                dashTimer++;
                Dash();
            }

            if (isDashCooldown)
            {
                if (dashCooldown > 0)
                    dashCooldown--;
                else
                    isDashCooldown = false;
            }
            #endregion

            #region JUMPING CODE

            if (onGround && (GamePad.GetButtonDown(GamePad.Button.A, gamePadIndex) || GamePad.GetKeyboardKeyDown(KeyCode.Space)))
            {
                animator.SetBool("grounded", false);
                rigidbody2D.AddForce(new Vector2(0, maxVelocity.y / 3f));
            }

            #endregion

        }

        //ManageDrunkness();

        if (IsDead)
        {
            //rbuttonPos = new Vector2(GetPosition.X + Global.PLAYER_SIZE / 2 + 10, GetPosition.Y + 40);
            //respawnButton.GetPosition = rbuttonPos;
            //respawnButton.Update();

            //if (state.Buttons.X == ButtonState.Pressed)
            //    playerObject.ResetPlayer(GetPosition, kills);

        }

	}

    public void Movement()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        animator.SetBool("grounded", onGround);

        animator.SetFloat("vSpeed", rigidbody2D.velocity.y);

        float move = GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex).x;
        if (GamePad.GetKeyboardKey(KeyCode.LeftArrow))
            move = -1;
        else if (GamePad.GetKeyboardKey(KeyCode.RightArrow))
            move = 1;

        animator.SetFloat("hSpeed", Mathf.Abs(move));
        rigidbody2D.velocity = new Vector2(move * (float)maxVelocity.x, rigidbody2D.velocity.y);

        if (move > 0 && Direction == Facing.LEFT)
            Flip();
        else if (move < 0 && Direction == Facing.RIGHT)
            Flip();
    }


    public void ManageDrunkness()
    {
        float time = (float)Time.deltaTime * (drunkTimeMultiplier * (1 + drunkness / 100));

        if (!isDrinking)
        {
            if (drunknessF <= 10)
                drunknessF = 10;
            else if (drunknessF >= maxDrunkness)
            {
                drunknessF = maxDrunkness - 1;
                maxDrunk = true;
            }
            else
            {
                drunknessF -= time;
                maxDrunk = false;
            }

            drunkness = (int)drunknessF;
        }

        damageMultiplayer = (int)((drunkness / (maxDrunkness / 10)) * .75f);
        if (damageMultiplayer < 1)
            damageMultiplayer = 1;

        //DrunkWalk();
    }

    public void Dash()
    {

        if (dashType == DashTypes.FORWARD)
        {
            if (Direction == Facing.RIGHT)
            {
                base.Move(new Vector2(10, 0));
                mirrored = false;
            }

            if (Direction == Facing.LEFT)
            {
                base.Move(new Vector2(-10, 0));
                mirrored = true;
            }
        }

        if (dashType == DashTypes.TRIGGER)
        {
            if (GamePad.GetTrigger(GamePad.Trigger.LeftTrigger, gamePadIndex) > 0.1)
            {
                if (Direction == Facing.LEFT)
                    base.Move(new Vector2(20, 0));
                else
                    base.Move(new Vector2(10, 0));
            }

            if (GamePad.GetTrigger(GamePad.Trigger.RightTrigger, gamePadIndex) > 0.1)
            {
                if (Direction == Facing.RIGHT)
                    base.Move(new Vector2(-20, 0));
                else
                    base.Move(new Vector2(-10, 0));
            }
        }
    }

    public void DrunkWalk()
    {
        if (drunkness > 200)
        {
            float time = (float)Time.deltaTime * 70;

            if (drunkWalkResetTimer > 0)
                drunkWalkResetTimer--;
            else if (drunkWalkResetTimer <= 0 && checkDrunkTimer == true)
            {
                checkDrunkTimer = false;
                drunkWalkTimer = Random.Range(3000 / (drunkness * (1 + (100 / drunkness))), 4500 / (drunkness * (1 + (100 / drunkness))));
                drunkRandomSide = Random.Range(0, 2);
            }

            if (!isAttacking && !isBlocking && !playerStateBool)
            {
                if (!IsDead && !isAttacking)
                {

                    if (drunkWalkTimer > 0 && checkDrunkTimer == false)
                    {
                        drunkWalkTimer--;
                        maxVelocity.x = Random.Range(3 - (drunkness / 2000), 4 + (drunkness / 1000));
                    }
                    else if (checkDrunkTimer == false)
                    {
                        drunkWalkResetTimer = Random.Range(8000 / drunkness, 12000 / drunkness);
                        checkDrunkTimer = true;
                    }

                }
            }

        }
        else
        {
            //drunkVelocity.X = 0;
            maxVelocity.x = 3;
            drunkWalkTimer = 0;
            drunkWalkResetTimer = 0;
        }

        // MAAK DAT DE SPELER LANGZAMER / SNELLER LOOPT OM DE ZOVEEL TIJD
        // BAZEER DE INTERVAL OP DRUNKNESS
    }

    public void GotHit(Vector2 punchPosition, float damageM, Global.PlayerIndex player)
    {
        Vector2 punchDirection = transform.position - new Vector3 (punchPosition.x, punchPosition.y,0);
        punchDirection.Normalize();

        if (!isBlocking)
        {
            isHit = true;
            if (punchDirection.x <= -.5 && Direction == Facing.LEFT)
                hitDirection = "Back";
            else if (punchDirection.x >= .5 && Direction == Facing.LEFT)
                hitDirection = "Front";
            else if (punchDirection.x <= -.5 && Direction == Facing.RIGHT)
                hitDirection = "Front";
            else if (punchDirection.x >= .5 && Direction == Facing.RIGHT)
                hitDirection = "Back";
        }
        velocity.x = punchDirection.x * 10;
        //Engine.Camera.ActivateShake(10f, 5f, punchDirection);

        if (!isBlocking && !IsDead && ((int)(1 * damageM)) >= GetHealth)
        {
            Damage((int)(1 * damageM));

            switch (player)
            {
                case Global.PlayerIndex.PLAYER1:
                    //Global.WorldObject.GetPlayer1.leprechaun.kills += 1;
                    break;
                case Global.PlayerIndex.PLAYER2:
                    //Global.WorldObject.GetPlayer2.leprechaun.kills += 1;
                    break;
                case Global.PlayerIndex.PLAYER3:
                    //Global.WorldObject.GetPlayer3.leprechaun.kills += 1;
                    break;
                case Global.PlayerIndex.PLAYER4:
                    //Global.WorldObject.GetPlayer4.leprechaun.kills += 1;
                    break;
                default:
                    break;
            }
        }
        else if (!isBlocking && !IsDead)
            Damage((int)(1 * damageM));

        //Console.WriteLine(IsDead);

    }

    public void GetDrunk(Global.SortOfDrink sortOfDrink)
    {
        switch (sortOfDrink)
        {
            case Global.SortOfDrink.ALE:
                drunknessF += 80;
                break;
            case Global.SortOfDrink.CIDER:
                drunknessF += 60;
                break;
            case Global.SortOfDrink.STOUT:
                drunknessF += 120;
                break;
            case Global.SortOfDrink.WHISKEY:
                drunknessF += 150;
                break;
            default:
                drunknessF += 40;
                break;
        }

    }

}
