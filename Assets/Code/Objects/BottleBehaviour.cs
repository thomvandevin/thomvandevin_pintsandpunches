using UnityEngine;
using System.Collections;

public class BottleBehaviour : MonoBehaviour {

    private SpriteRenderer sprite;
    private Sprite randomSprite;
    private int bottleNumber;
    private bool doOnce = true;

    public int NumberOfDifferentBottles = 16;

	// Use this for initialization
	void Start () 
	{
        sprite = gameObject.GetComponent<SpriteRenderer>();
        bottleNumber = Random.Range(1, NumberOfDifferentBottles + 1);

        randomSprite = Resources.Load<Sprite>("Sprites/Environment/Environment_01/Background/Bottles/Level_Pubcounter_Bottle_0" + bottleNumber.ToString("D2"));
        sprite.sprite = randomSprite;

        Global.environmentGlasses.Add(gameObject);
    }
	
	// Update is called once per frame
	void Update () 
	{
		//Check to see if screenshake was activated twice in the last 5 seconds. if yes, let the bottle drop (give rigidbody and let it drop)
    }

    public void KnockBottleOff()
    {
        gameObject.AddComponent<Rigidbody2D>();
        float rndX = Random.Range(-100,100);
        float rndY = Random.Range(0, 40);
        float rndT = Random.Range(30, 60);
        if (rndX > 0)
            rndT *= -1;

        gameObject.rigidbody2D.AddForce(new Vector2(rndX, rndY));
        gameObject.rigidbody2D.AddTorque(rndT);
        Global.environmentGlasses.Remove(gameObject);
        Destroy(gameObject, 1f);
    }
}
