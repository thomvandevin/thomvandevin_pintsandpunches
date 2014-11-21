using UnityEngine;
using System.Collections;

public class RandomBottle : MonoBehaviour {
	
	private SpriteRenderer sprite;
    private Sprite randomSprite;
	private int bottleNumber;
	
	// Use this for initialization
	void Start () 
	{
        sprite = gameObject.GetComponent<SpriteRenderer>();
		bottleNumber = Random.Range(1,17);

        randomSprite = Resources.Load<Sprite>("Sprites/Environment/Environment_01/Background/Bottles/Level_Pubcounter_Bottle_0" + bottleNumber.ToString("D2"));
        sprite.sprite = randomSprite;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
