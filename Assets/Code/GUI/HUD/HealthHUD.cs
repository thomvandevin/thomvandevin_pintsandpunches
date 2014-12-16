using UnityEngine;
using System.Collections;

public class HealthHUD : MonoBehaviour {

    private Animator anim;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () 
    {
        anim = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public void SetHealthHud(int character, int controller, float health)
    {
        SetCharacter(character);
        SetHealth(health);

        switch (controller)
        {
            case 1:
                sprite.color = Color.blue;
                break;
            case 2:
                sprite.color = Color.red;
                break;
            case 3:
                sprite.color = Color.green;
                break;
            case 4:
                sprite.color = Color.yellow;
                break;
            default:
                break;
        }

    }

    public void SetCharacter(int character)
    {
        anim.SetFloat("Character", character);
    }

    public void SetHealth(float health)
    {
        anim.SetFloat("Health", health);
    }
}
