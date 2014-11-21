using UnityEngine;
using System.Collections;

public class BottleBehaviour : MonoBehaviour {

    private SpriteRenderer sprite;
    private int randomBottle;

	// Use this for initialization
	void Start () 
	{
        sprite = gameObject.GetComponent<SpriteRenderer>();
        randomBottle = Random.Range(1,17);

        sprite.sprite = Resources.Load("Sprites/Environment/Environment_01/Background/Bottles/Level_PubCounter_Bottle_0" + randomBottle.ToString("D2")) as Sprite;
    }
	
	// Update is called once per frame
	void Update () 
	{
		//Check to see if screenshake was activated twice in the last 5 seconds. if yes, let the bottle drop (give rigidbody and let it drop)
	}
}
