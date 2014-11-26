using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;

public class Entity : MonoBehaviour {

    [HideInInspector]

    public enum Facing
    {
        LEFT,
        RIGHT
    }
    public Facing Direction;

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

    #region EDITABLE IN LEPRECHAUN
    public int dashCooldown, maxDrunkness, drunkTimeMultiplier, drunkWalkResetTimer, attackCooldownMax;
    public float gravity, gravityCorrection, resistance, damageMultiplayer, groundRadius;
    public Vector2 startingPosition, velocity, maxVelocity, drunkVelocity;
    #endregion


    public Player playerScript;
    public Player.Character chosenCharacter;
    public PlayerStates playerState;
    public DashTypes dashType;
    public Drink.SortOfDrink sortOfDrink;

    public Dictionary<string, bool> dicStringBool;
    public Dictionary<string, PlayerStates> dicStringState;
    public Dictionary<int, GamePad.Index> controllerIndex;
    public GamePad.Index gamePadIndex;

    private bool isDead;
    public bool IsDead { get { return isDead; } set { isDead = value; } }
    public bool isPlayer;
    public bool mirrored, attackDone, playerStateBool, maxDrunk, checkDrunkTimer, dustOnce, canDoubleJump;
    public bool isDashCooldown, skipNextMove, fallTroughBar, underBar, collidingWithWall;
    public bool onGround, isHit, isBlocking, isDashing, isAttacking, isDrinking;

    protected int maxHealth, health;
    public int GetHealth { get { return health; } set { health = value; } }
    public int drinkAnimCounter, drunkness, drunkWalkTimer, drunkRandomSide;
    public int controllerNumber, dashTimer, deathCounter, playerStateTimer, jumpOnce, attackOnce, attackCooldown;

    public float drunknessF;

    public Vector2 previousPosition, lastVelocity, respawnButtonPos;

    public string hitDirection, playerStateString;

    protected Animator top_animator, bottom_animator;
    public GameObject playerObject, respawnButton;
    public GameObject groundCheck, punchCheck, bodyCheck, wallCheck;
    public GameObject dustParticle;
    public LayerMask groundLayer;

	// Use this for initialization
	public Entity () 
    {

	}

    public void SetEntity(Vector2 pos, bool isPlayer)
    {
        health = maxHealth;

        this.isPlayer = isPlayer;
        isDead = false;

        playerScript = playerObject.GetComponent<Player>();
        top_animator = Global.getChildGameObject(gameObject, "Animation_top").GetComponent<Animator>();
        bottom_animator = Global.getChildGameObject(gameObject, "Animation_bottom").GetComponent<Animator>();

        mirrored = false;
        onGround = false;
        collidingWithWall = false;
        skipNextMove = false;
        fallTroughBar = false;
        underBar = false; 
        attackDone = false;
        dustOnce = false;
        maxDrunk = false;
        checkDrunkTimer = true;
        if (chosenCharacter == Player.Character.FAIRY)
            canDoubleJump = true;
        else
            canDoubleJump = false;

        isHit = false;
        isAttacking = false;
        isBlocking = false;
        isDashing = false;
        isDrinking = false;
        
        jumpOnce = 0;
        attackOnce = 0;
        attackCooldown = 0;
        drinkAnimCounter = 0;
        drunkness = 0;
        drunknessF = 0f;
        drunkWalkTimer = 0;
        deathCounter = 0;

        hitDirection = "filler";

        lastVelocity = new Vector2(0, 2);

        sortOfDrink = Drink.SortOfDrink.NONE;
        Direction = Facing.RIGHT;

        groundCheck = Global.getChildGameObject(gameObject, "GroundCheck");
        punchCheck = Global.getChildGameObject(gameObject, "PunchCheck");
        bodyCheck = Global.getChildGameObject(gameObject, "BodyCheck");
        wallCheck = Global.getChildGameObject(gameObject, "WallCheck");
        dustParticle = Global.getChildGameObject(gameObject, "Dust particle");
        groundLayer = playerObject.GetComponent<LayerMaskPass>().GetLayerMask();

        respawnButtonPos = new Vector2(transform.position.x, transform.position.y - 40);

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

	// Update is called once per frame
	public virtual void Update () {
        if (health <= 0)
            isDead = true;

        if (gameObject.transform.parent.gameObject.GetComponent<Player>().kills >= 5)
        {
            Global.WorldObject.GameWon(controllerNumber);
            //Global.WorldObject.winnerIndex = chosenPlayerIndex;
        }

        if (!IsDead && !isDrinking)
        {
            #region ATTACKING CODE

            if (GamePad.GetButtonDown(GamePad.Button.X, gamePadIndex)
                && onGround && !isDrinking)
            {
                foreach (GameObject d in Global.drinks.ToArray())
                {
                    Drink drinkScript = d.GetComponent<Drink>();

                    if (bodyCheck.collider2D.bounds.Intersects(drinkScript.drinkCollision.collider2D.bounds))
                    {
                        sortOfDrink = drinkScript.drinkType;
                        GetDrunk(sortOfDrink);
                        drinkScript.Remove();

                        isDrinking = true;
                        playerObject.rigidbody2D.velocity = Vector2.zero;
                        SetAnimation("isDrinking_1", true);
                        SetAnimation("isDrinking_2", true);
                        SetAnimation("typeOfDrink", drinkScript.drinkNumber);
                        //Invoke("NotDrinking", .4f);

                    }
                }
            }


            if (GamePad.GetButtonDown(GamePad.Button.X, gamePadIndex) && onGround &&
                attackOnce == -1 && !isDrinking && attackCooldown == 0)
            {
                //playerState = PlayerStates.ATTACKING;
                isAttacking = true;
                SetAnimation("isAttacking", true);
                attackOnce = 1;
                attackCooldown = attackCooldownMax;

                bool didHit = false;

                foreach (GameObject lep in Global.leprechauns.ToArray())
                {
                    Player p = lep.gameObject.transform.parent.GetComponent<Player>();
                    if (punchCheck.collider2D.bounds.Intersects(p.GetCollisionObject("bodyCheck", lep).collider2D.bounds) && lep != gameObject)
                    {
                        if (p.GetLeprechaunScriptType().GetType() == typeof(Leprechaun))
                        {
                            Leprechaun lepScript = (Leprechaun)p.leprechaunScript;
                            lepScript.GotHit(gameObject.transform.position, damageMultiplayer, gamePadIndex);
                            didHit = true;
                            lepScript.isHit = true;
                            lepScript.SetAnimation("isHit", true);
                            lepScript.Invoke("NotHit", .2f);
                        }
                        else if (p.GetLeprechaunScriptType().GetType() == typeof(Leprechaun_USA))
                        {
                            Leprechaun_USA lepScript = (Leprechaun_USA)p.leprechaunScript;
                            lepScript.GotHit(gameObject.transform.position, damageMultiplayer, gamePadIndex);
                            didHit = true;
                            lepScript.isHit = true;
                            lepScript.SetAnimation("isHit", true);
                            lepScript.Invoke("NotHit", .2f);
                        }
                        else if (p.GetLeprechaunScriptType().GetType() == typeof(Cluirichaun))
                        {
                            Cluirichaun lepScript = (Cluirichaun)p.leprechaunScript;
                            lepScript.GotHit(gameObject.transform.position, damageMultiplayer, gamePadIndex);
                            didHit = true;
                            lepScript.isHit = true;
                            lepScript.SetAnimation("isHit", true);
                            lepScript.Invoke("NotHit", .2f);
                        }
                        else if (p.GetLeprechaunScriptType().GetType() == typeof(FarDarrig))
                        {
                            FarDarrig lepScript = (FarDarrig)p.leprechaunScript;
                            lepScript.GotHit(gameObject.transform.position, damageMultiplayer, gamePadIndex);
                            didHit = true;
                            lepScript.isHit = true;
                            lepScript.SetAnimation("isHit", true);
                            lepScript.Invoke("NotHit", .2f);
                        }
                        else if (p.GetLeprechaunScriptType().GetType() == typeof(Fairy))
                        {
                            Fairy lepScript = (Fairy)p.leprechaunScript;
                            lepScript.GotHit(gameObject.transform.position, damageMultiplayer, gamePadIndex);
                            didHit = true;
                            lepScript.isHit = true;
                            lepScript.SetAnimation("isHit", true);
                            lepScript.Invoke("NotHit", .2f);
                        }

                    }
                }

                if (!didHit)
                {
                    int i = UnityEngine.Random.Range(1, 6);
                    gameObject.Stab(Resources.Load("Audio/SFX/Punch_Miss_" + i.ToString()) as AudioClip, 1f, 1f, 0f);
                }
                else
                {
                    int i = UnityEngine.Random.Range(1, 6);
                    gameObject.Stab(Resources.Load("Audio/SFX/Punch_Hit_" + i.ToString()) as AudioClip, 1f, 1f, 0f);
                }
            }

            if (attackOnce >= 1)
                attackOnce -= 1;
            else if (attackOnce == 0)
            {
                attackOnce = -1;
                attackDone = false;

                isAttacking = false;
                SetAnimation("isAttacking", false);
            }

            if (attackCooldown > 0)
                attackCooldown--;
            else if (attackCooldown < 0)
                attackCooldown = 0;

            #endregion

            #region BLOCKING CODE

            if (GamePad.GetButtonDown(GamePad.Button.B, gamePadIndex) && !isDashing && !isAttacking && !isDrinking)
            {
                isBlocking = true;
                SetAnimation("isBlocking", true);
            }

            if (GamePad.GetButtonUp(GamePad.Button.B, gamePadIndex))
            {
                isBlocking = false;
                SetAnimation("isBlocking", false);
            }

            #endregion

            #region DASHING CODE
            //if ((GamePad.GetTrigger(GamePad.Trigger.LeftTrigger, gamePadIndex) > 0.1 || GamePad.GetTrigger(GamePad.Trigger.LeftTrigger, gamePadIndex) > 0.1)
            //    && !isDashing && !isDashCooldown)
            //{
            //    isDashing = true;
            //    dashTimer = 0;
            //    //animation.GetAnimation.Start();
            //}

            //if (isDashing)
            //{
            //    if (dashTimer > 10)
            //    {
            //        dashCooldown = 120;
            //        isDashCooldown = true;
            //        isDashing = false;
            //    }

            //    //playerState = PlayerStates.DASHING;
            //    dashTimer++;
            //    Dash();
            //}

            //if (isDashCooldown)
            //{
            //    if (dashCooldown > 0)
            //        dashCooldown--;
            //    else
            //        isDashCooldown = false;
            //}
            #endregion

            #region JUMPING CODE

            if (onGround && (GamePad.GetButtonDown(GamePad.Button.A, gamePadIndex) || GamePad.GetKeyboardKeyDown(KeyCode.Space)) && !isAttacking && !isHit)
            {
                SetAnimation("grounded", false);
                playerObject.rigidbody2D.AddForce(new Vector2(0, maxVelocity.y));

            }

            if(!onGround && chosenCharacter == Player.Character.FAIRY && canDoubleJump && GamePad.GetButtonDown(GamePad.Button.A, gamePadIndex) && !isAttacking && !isHit)
            {
                Vector3 vel = playerObject.rigidbody2D.velocity;
                vel.y = 0;
                playerObject.rigidbody2D.velocity = vel;
                playerObject.rigidbody2D.AddForce(new Vector2(0, maxVelocity.y));
                canDoubleJump = false;
            }

            #endregion
        }
        else if (isDrinking)
        {
            if (bottom_animator.GetCurrentAnimatorStateInfo(0).IsName("Drink") &&
                bottom_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
                !bottom_animator.IsInTransition(0))
                NotDrinking(1, false);

            if (bottom_animator.GetCurrentAnimatorStateInfo(0).IsName("Drink") &&
                bottom_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
                !bottom_animator.IsInTransition(0))
                NotDrinking(2, true);
        }


        //ManageDrunkness();

        if (IsDead && deathCounter == 0)
        {
            SetAnimation("isDead", true);
            SetAnimation("grounded", true);
            SetAnimation("isHit", false);
            SetAnimation("isAttacking", false);
            SetAnimation("isBlocking", false);
            SetAnimation("isDrinking_1", false);
            SetAnimation("isDrinking_2", false);
            gameObject.Stab(Resources.Load("Audio/SFX/Knockout") as AudioClip, 1f, 1f, 0f);

            deathCounter++;
        }
        else if (IsDead && deathCounter == 1 &&
                bottom_animator.GetCurrentAnimatorStateInfo(0).IsName("KnockOut"))
        {
            SetAnimation("deathCounter", deathCounter);
            SetAnimation("isDead", false);
            Global.leprechauns.Remove(gameObject);

            deathCounter++;
        }
        else if (IsDead && deathCounter == 2)
        {
            RespawnButton();

            deathCounter++;
        }
        else if (IsDead && deathCounter == 3)
        {
            if (GamePad.GetButtonDown(GamePad.Button.X, gamePadIndex))
            {
                respawnButton.GetComponent<RespawnButton>().RemoveRespawnButton();
                playerScript.ResetPlayer(gameObject);
            }
        }

	}

    public void FixedUpdate()
    {

    }

    public virtual void Move(Vector2 pos)
    {

    }

    public void Flip()
    {
        if (Direction == Facing.LEFT)
            Direction = Facing.RIGHT;
        else if (Direction == Facing.RIGHT)
            Direction = Facing.LEFT;

        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    public void Movement()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.transform.position, groundRadius, groundLayer);
        SetAnimation("grounded", onGround);

        if (onGround && chosenCharacter == Player.Character.FAIRY)
            canDoubleJump = true;

        SetAnimation("vSpeed", playerObject.rigidbody2D.velocity.y);

        float move = 0;

        if (!isHit && !collidingWithWall && !isBlocking)
        {
            if (GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex).x > .3f)
                move = 1;
            else if (GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex).x < -.3f)
                move = -1;

            if(Mathf.Abs(move) >= 1)
            {
                if(!dustOnce && onGround)
                {
                    dustOnce = true;
                    dustParticle.GetComponent<Animator>().SetBool("triggerOnce", true);
                } 
                else if(dustOnce && !onGround)
                    dustParticle.GetComponent<Animator>().SetBool("triggerOnce", false);
            }
            else if (dustOnce)
            {
                dustOnce = false;
                dustParticle.GetComponent<Animator>().SetBool("triggerOnce", false);
            }

            if (!isAttacking)
            {
                SetAnimation("hSpeed", Mathf.Abs(move));
                playerObject.rigidbody2D.velocity = new Vector2(move * (float)maxVelocity.x / 8, playerObject.rigidbody2D.velocity.y);
            }

            if (move > 0 && Direction == Facing.LEFT)
                Flip();
            else if (move < 0 && Direction == Facing.RIGHT)
                Flip();
        }
        else if (collidingWithWall && onGround)
            collidingWithWall = false;

    }

    public void Damage(int value)
    {
        if (health > 0)
            health -= value;

        if (health < 0)
            health = 0;
    }

    public void Dash()
    {

        if (dashType == DashTypes.FORWARD)
        {
            if (Direction == Facing.LEFT)
            {
                playerObject.rigidbody2D.AddForce(new Vector2(-(10 * maxVelocity.x), 1));
                mirrored = true;
            }

            if (Direction == Facing.RIGHT)
            {
                playerObject.rigidbody2D.AddForce(new Vector2(10 * maxVelocity.x, 1));
                mirrored = false;
            }

        }

        if (dashType == DashTypes.TRIGGER)
        {
            if (GamePad.GetTrigger(GamePad.Trigger.LeftTrigger, gamePadIndex) > 0.1)
            {
                if (Direction == Facing.LEFT)
                    Move(new Vector2(20, 0));
                else
                    Move(new Vector2(10, 0));
            }

            if (GamePad.GetTrigger(GamePad.Trigger.RightTrigger, gamePadIndex) > 0.1)
            {
                if (Direction == Facing.RIGHT)
                    Move(new Vector2(-20, 0));
                else
                    Move(new Vector2(-10, 0));
            }
        }
    }

    public void PunchShake(Vector2 punchDirection, float hardness, float time, bool motionB)
    {
        punchDirection = (punchDirection / 10) * hardness;
        iTween.PunchPosition(GameObject.FindGameObjectWithTag("MainCamera"), punchDirection, time);
        if (motionB)
        {
            MotionBlur motionBlur = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MotionBlur>();
            motionBlur.blurAmount = .8f;
            motionBlur.Invoke("StopBlur", .2f);
        }

    }
    
    protected void RespawnButton()
    {
        respawnButton = Instantiate(Resources.Load("Prefabs/Objects/HUD/Respawn_Button"), new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Quaternion.identity) as GameObject;
        respawnButton.GetComponent<RespawnButton>().SetRespawnButton(playerObject);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Collision_left" || coll.gameObject.tag == "Collision_right")
            collidingWithWall = true;

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
        playerObject.rigidbody2D.AddForce(new Vector2(-punchDirection.x * maxVelocity.x * 8, maxVelocity.y / 3.5f));
        PunchShake(punchDirection, 1.7f, .4f, false);

        GameObject particles = Instantiate(Resources.Load("Prefabs/Objects/Particles/Particles_BloodAndGore"), transform.position - (punchDirection / 1.3f), Quaternion.identity) as GameObject;
        if (punchDirection.x == 1)
        {
            Vector3 rot = particles.transform.localEulerAngles;
            rot.z += 180;
            particles.transform.localEulerAngles = rot;
        }
        Destroy(particles.gameObject, 1f);


        if (!isBlocking && !IsDead && ((int)(1 * damageM)) >= health)
        {
            Damage((int)(1 * damageM));

            switch (player)
            {
                case GamePad.Index.One:
                    Global.leprechauns[0].gameObject.transform.parent.gameObject.GetComponent<Player>().kills += 1;
                    break;
                case GamePad.Index.Two:
                    Global.leprechauns[1].gameObject.transform.parent.gameObject.GetComponent<Player>().kills += 1;
                    break;
                case GamePad.Index.Three:
                    Global.leprechauns[2].gameObject.transform.parent.gameObject.GetComponent<Player>().kills += 1;
                    break;
                case GamePad.Index.Four:
                    Global.leprechauns[3].gameObject.transform.parent.gameObject.GetComponent<Player>().kills += 1;
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
        if (Mathf.Abs(playerObject.rigidbody2D.velocity.x) > 10)
        {
            playerObject.rigidbody2D.velocity = new Vector2(playerObject.rigidbody2D.velocity.x / 10, rigidbody2D.velocity.y);
            Invoke("NotHit", .01f);
        }
        else
        {
            isHit = false;
            SetAnimation("isHit", false);
        }
    }

    public void NotDrinking(int number, bool end)
    {
        SetAnimation("isDrinking_" + number.ToString(), false);
        if (end)
            isDrinking = false;

    }
    
    public void GetDrunk(Drink.SortOfDrink sortOfDrink)
    {
        switch (sortOfDrink)
        {
            case Drink.SortOfDrink.ALE:
                drunknessF += 80;
                break;
            case Drink.SortOfDrink.CIDER:
                drunknessF += 60;
                break;
            case Drink.SortOfDrink.STOUT:
                drunknessF += 120;
                break;
            case Drink.SortOfDrink.WHISKEY:
                drunknessF += 150;
                break;
            default:
                drunknessF += 40;
                break;
        }
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

    public void DrunkWalk()
    {
        if (drunkness > 200)
        {

            if (drunkWalkResetTimer > 0)
                drunkWalkResetTimer--;
            else if (drunkWalkResetTimer <= 0 && checkDrunkTimer == true)
            {
                checkDrunkTimer = false;
                drunkWalkTimer = UnityEngine.Random.Range(3000 / (drunkness * (1 + (100 / drunkness))), 4500 / (drunkness * (1 + (100 / drunkness))));
                drunkRandomSide = UnityEngine.Random.Range(0, 2);
            }

            if (!isAttacking && !isBlocking && !playerStateBool)
            {
                if (!IsDead && !isAttacking)
                {

                    if (drunkWalkTimer > 0 && checkDrunkTimer == false)
                    {
                        drunkWalkTimer--;
                        maxVelocity.x = UnityEngine.Random.Range(3 - (drunkness / 2000), 4 + (drunkness / 1000));
                    }
                    else if (checkDrunkTimer == false)
                    {
                        drunkWalkResetTimer = UnityEngine.Random.Range(8000 / drunkness, 12000 / drunkness);
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


    #region SET ANIMATION STUFF

    public void SetAnimation(string name, int value)
    {
        top_animator.SetInteger(name, value);
        bottom_animator.SetInteger(name, value);
    }
    public void SetAnimation(string name, float value)
    {
        top_animator.SetFloat(name, value);
        bottom_animator.SetFloat(name, value);
    }
    public void SetAnimation(string name, bool value)
    {
        top_animator.SetBool(name, value);
        bottom_animator.SetBool(name, value);
    }

    #endregion

}
