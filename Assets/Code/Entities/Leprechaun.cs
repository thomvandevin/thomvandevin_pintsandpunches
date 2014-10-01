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
    public bool mirrored, isDashCooldown, skipNextMove, fallTroughBar, underBar, playerStateBool, maxDrunk, checkDrunkTimer, collidingWithWall, attackDone;
    public bool onGround, isHit, isDrinking, isBlocking, isDashing, isAttacking;
    public int controllerNumber, chosenCharacter, dashTimer, dashCooldown, playerStateTimer, jumpOnce, attackOnce, currentTile, kills;
    public int drinkAnimCounter, drunkness, maxDrunkness, drunkTimeMultiplier, drunkWalkTimer, drunkWalkResetTimer, drunkRandomSide, attackCooldown;
    public string hitDirection, playerStateString;
    public float gravity, gravityCorrection, resistance, drunknessF, damageMultiplayer, groundRadius;
    public Vector2 startingPosition, velocity, previousPosition, maxVelocity, lastVelocity, drunkVelocity, respawnButtonPos;
    public GameObject groundCheck, punchCheck, bodyCheck, wallCheck;
    public LayerMask groundLayer;
    //public SoundEffect sfx_punch_hit, sfx_punch_miss, sfx_jump_down, sfx_jump, sfx_knockout, sfx_drink, sfx_drink_whiskey;
    //public SoundEffectInstance sfx_drink_whiskey_inst;
    //public RespawnButton respawnButton;
    //public DrunkBubbles drunkBubbles;

    public Animator animator;
    
    public Leprechaun()
    {

    }

    public void SetLeprechaun(Vector2 pos, int controllerNumber, int chosenCharacter, GameObject playerObject, int killsAmount)
    {
        this.controllerNumber = controllerNumber;

        //playerObject = playerobject;
        kills = killsAmount;

        this.chosenCharacter = chosenCharacter;
        startingPosition = pos;
        mirrored = false;
        onGround = false;
        groundRadius = 0.2f;
        groundCheck = Global.getChildGameObject(playerObject, "GroundCheck");
        groundLayer = gameObject.GetComponent<LayerMaskPass>().GetLayerMask();
        punchCheck = Global.getChildGameObject(playerObject, "PunchCheck");
        bodyCheck = Global.getChildGameObject(playerObject, "BodyCheck");
        wallCheck = Global.getChildGameObject(playerObject, "WallCheck");
        collidingWithWall = false;
        gravity = 0.5f;
        resistance = 2f;
        velocity = new Vector2(0, 60);
        //maxVelocity = new Vector2(70, 750); //OLD
        maxVelocity = new Vector2(40, 530);
        lastVelocity = new Vector2(0, 2);
        skipNextMove = false;
        fallTroughBar = false;
        underBar = false;
        gravityCorrection = 0f;
        Direction = Facing.RIGHT;
        dashType = DashTypes.FORWARD;
        jumpOnce = 0;
        attackOnce = 0;
        attackCooldown = 0;
        attackDone = false;
        hitDirection = "JAWAT";
        sortOfDrink = Global.SortOfDrink.NONE;
        drinkAnimCounter = 0;
        drunkness = 0;
        drunknessF = 0f;
        damageMultiplayer = 1;
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

        isHit = false;
        isAttacking = false;
        isBlocking = false;
        isDashing = false;
        isDrinking = false;

        SetDictionary();

    }

    public void SetDictionary()
    {
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
        KeepOnScreen();
        base.Update();

    }

	// Update is called once per frame
	public override void Update () 
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


            if (GamePad.GetButtonDown(GamePad.Button.X, gamePadIndex) && 
                animator.GetBool("grounded") == true && attackOnce == -1 && 
                !isDrinking && attackCooldown == 0)
            {

                print("swag");
                //playerState = PlayerStates.ATTACKING;
                isAttacking = true;
                animator.SetBool("isAttacking", true);
                attackOnce = 1;
                attackCooldown = Random.Range(40, 48);

                bool didHit = false;

                foreach (Leprechaun lep in Global.leprechauns)
                {
                    if (punchCheck.collider2D.bounds.Intersects(lep.bodyCheck.collider2D.bounds) && lep != this)
                    {
                        lep.GotHit(this.transform.position, damageMultiplayer, controllerIndex[controllerNumber]);
                        didHit = true;
                        lep.isHit = true;
                        lep.animator.SetBool("isHit", true);
                        lep.Invoke("NotHit", .15f);
                    }
                }

                if (!didHit)
                {
                    int i = Random.Range(1, 6);
                    iTween.Stab(gameObject, Resources.Load("Audio/SFX/Punch_Miss_" + i.ToString()) as AudioClip, 0f);
                }
                else
                {
                    int i = Random.Range(1, 6);
                    iTween.Stab(gameObject, Resources.Load("Audio/SFX/Punch_Hit_" + i.ToString()) as AudioClip, 0f);
                }
            }

            if (attackOnce >= 1)
                attackOnce -= 1;
            else if(attackOnce == 0)
            {
                attackOnce = -1;

                attackDone = false;
                isAttacking = false;
                animator.SetBool("isAttacking", false);
            }

            if (attackCooldown > 0)
                attackCooldown--;
            else if (attackCooldown < 0)
                attackCooldown = 0;
            
            #endregion

            #region BLOCKING CODE

            //if (GamePad.GetButtonDown(GamePad.Button.B, gamePadIndex) && !isJumping && !isDashing && !isAttacking && !isDrinking)
            //{
            //    isBlocking = true;
            //    if (isWalking)
            //        isWalking = false;
            //}

            //if (GamePad.GetButtonUp(GamePad.Button.B, gamePadIndex))
            //    isBlocking = false;

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

            if (onGround && (GamePad.GetButtonDown(GamePad.Button.A, gamePadIndex) || GamePad.GetKeyboardKeyDown(KeyCode.Space)) && !isAttacking && !isHit)
            {
                animator.SetBool("grounded", false);
                rigidbody2D.AddForce(new Vector2(0, maxVelocity.y));
            }

            #endregion
        }

        //ManageDrunkness();

        if (IsDead)
        {
            print("i be dead yo");
            //rbuttonPos = new Vector2(GetPosition.X + Global.PLAYER_SIZE / 2 + 10, GetPosition.Y + 40);
            //respawnButton.GetPosition = rbuttonPos;
            //respawnButton.Update();

            //if (state.Buttons.X == ButtonState.Pressed)
            //    playerObject.ResetPlayer(GetPosition, kills);

        }

	}


    public void Movement()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.transform.position, groundRadius, groundLayer);
        animator.SetBool("grounded", onGround);

        animator.SetFloat("vSpeed", rigidbody2D.velocity.y);

        float move = GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex).x;

        if (!isHit && !collidingWithWall)
        {
            if (GamePad.GetKeyboardKey(KeyCode.LeftArrow))
                move = -1;
            else if (GamePad.GetKeyboardKey(KeyCode.RightArrow))
                move = 1;

            if (!isAttacking)
            {
                animator.SetFloat("hSpeed", Mathf.Abs(move));
                rigidbody2D.velocity = new Vector2(move * (float)maxVelocity.x / 8, rigidbody2D.velocity.y);
            }

            if (move > 0 && Direction == Facing.LEFT)
                Flip();
            else if (move < 0 && Direction == Facing.RIGHT)
                Flip();
        }
        else if (collidingWithWall && onGround)
            collidingWithWall = false;
        
    }


    public void ManageDrunkness()
    {

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
                drunknessF -= Time.deltaTime;
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
            if (Direction == Facing.LEFT)
            {
                rigidbody2D.AddForce(new Vector2(-(10 * maxVelocity.x), 1));
                mirrored = true;
            }

            if (Direction == Facing.RIGHT)
            {
                rigidbody2D.AddForce(new Vector2(10 * maxVelocity.x, 1));
                mirrored = false;
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

    public void GotHit(Vector2 punchPosition, float damageM, GamePad.Index player)
    {
        Vector3 punchDirection = Vector3.zero;
        if (punchPosition.x > transform.position.x)
            punchDirection.x = 1;
        else if (punchPosition.x < transform.position.x)
            punchDirection.x = -1;

        if (!isBlocking)
        {
            isHit = true;
            if (punchDirection.x >= .5 && Direction == Facing.LEFT)
                Flip();
            else if (punchDirection.x <= -.5 && Direction == Facing.RIGHT)
                Flip();

        }
        gameObject.rigidbody2D.AddForce(new Vector2(-punchDirection.x * maxVelocity.x * 8, 100f));
        PunchShake(punchDirection, 1.7f, .4f, false);
        
        if (!isBlocking && !IsDead && ((int)(1 * damageM)) >= GetHealth)
        {
            Damage((int)(1 * damageM));

            switch (player)
            {
                case GamePad.Index.One:
                    Global.leprechauns[0].kills += 1;
                    break;
                case GamePad.Index.Two:
                    Global.leprechauns[1].kills += 1;
                    break;
                case GamePad.Index.Three:
                    Global.leprechauns[2].kills += 1;
                    break;
                case GamePad.Index.Four:
                    Global.leprechauns[3].kills += 1;
                    break;
                default:
                    break;
            }
        }
        else if (!isBlocking && !IsDead)
            Damage((int)(1 * damageM));

    }

    public void NotHit()
    {
        if (Mathf.Abs(rigidbody2D.velocity.x) > 10)
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x / 10, rigidbody2D.velocity.y);
        else
        {
            isHit = false;
            animator.SetBool("isHit", false);
        }
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

    private void KeepOnScreen()
    {
        //GameObject wallRight = GameObject.FindGameObjectWithTag("Collision_right");
        //GameObject wallLeft = GameObject.FindGameObjectWithTag("Collision_left");

        //if (wallCheck.transform.position.x > wallRight.transform.position.x)
        //    collidingWithWall = true;
        //else if(wallCheck.transform.position.x < wallLeft.transform.position.x)
        //    collidingWithWall = true;
    }
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Collision_left" || coll.gameObject.tag == "Collision_right")
            collidingWithWall = true;

    }

    public void PunchShake(Vector2 punchDirection, float hardness, float time, bool motionB)
    {
        punchDirection = (punchDirection/10)*hardness;
        iTween.PunchPosition(GameObject.FindGameObjectWithTag("MainCamera"), punchDirection, time);
        if(motionB)
        {
            MotionBlur motionBlur = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MotionBlur>();
            motionBlur.blurAmount = .8f;
            motionBlur.Invoke("StopBlur", .2f);
        }

    }

}
