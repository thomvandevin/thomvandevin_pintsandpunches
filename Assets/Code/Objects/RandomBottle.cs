using UnityEngine;
using System.Collections;

public class RandomBottle : MonoBehaviour {
	
    //hier een privaye spriterenderer genaamd sprite
	private SpriteRenderer sprite;
	private int bottleNumber;
	
	// Use this for initialization
	void Start () 
	{
        ////Randomly spawn one of the bottles
        ////grab a number from 1 to x (where xi s the amount of bottles) and spawn numbered bottle. 

		bottleNumber = Random.Range(1,17);
        //hier de spriterenderer koppelen, sprite = gameobject.getcomponent
		sprite = gameObject.GetComponent<SpriteRenderer>();
		
        //sprite.sprite = pad naar image toe + randomint .ToString()
		sprite.sprite = Resources.Load("/Sprites/Environment/Environment_01/Background/Bottles/Level_Pubcounter_Bottle_0" + bottleNumber.ToString("D2")) as Sprite;

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
