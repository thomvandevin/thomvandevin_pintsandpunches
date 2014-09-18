using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    [HideInInspector]

    public enum Facing
    {
        LEFT,
        RIGHT
    }
    public Facing Direction;

    private int maxHealth, health;
    public int GetHealth { get { return health; } set { health = value; } }
    public int GetMaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    public bool isPlayer;

    private bool isDead;
    public bool IsDead { get { return isDead; } set { isDead = value; } }

	// Use this for initialization
	public Entity () 
    {

	}

    public void SetEntity(Vector2 pos, int maxHP)
    {
        maxHealth = maxHP;
        health = maxHealth;

        isPlayer = false;
        isDead = false;
    }

    public void SetEntity(Vector2 pos, string tex, int maxHp, bool isPlayer)
    {
        maxHealth = maxHp;
        health = maxHealth;

        this.isPlayer = isPlayer;
        isDead = false;
    }
	
	// Update is called once per frame
	public void Update () {
        if (health <= 0)
            isDead = true;

	}

    public virtual void Move(Vector2 pos)
    {

    }

    public void Damage(int value)
    {
        if (health > 0)
            health -= value;

        if (health < 0)
            health = 0;
    }
}
