using UnityEngine;
using System.Collections;

public class HealthHUD : MonoBehaviour {

    private Animator anim;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Awake () 
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void SetHealthHud(int controller)
    {

        SetHealth(0);
        
        switch (controller)
        {
            case 1:
                sprite.color = new Color(0, 0, 255, 200);
                break;
            case 2:
                sprite.color = new Color(255, 0, 0, 200);
                break;
            case 3:
                sprite.color = new Color(0, 255, 0, 200);
                break;
            case 4:
                sprite.color = new Color(255, 255, 0, 200);
                break;
            default:
                sprite.color = new Color(0, 0, 255, 255);
                break;
        }

    }

    public void SetHealth(int health)
    {
        anim.SetInteger("Health", health);
    }
}
