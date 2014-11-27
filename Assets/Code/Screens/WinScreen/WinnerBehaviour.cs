using UnityEngine;
using System.Collections;

public class WinnerBehaviour : MonoBehaviour {

    private GameObject playerText, characterSprite;

	// Use this for initialization
	void Start () 
    {
        playerText = GameObject.FindGameObjectWithTag("WinScreen_Text");
        characterSprite = GameObject.FindGameObjectWithTag("WinScreen_Character");

        int character = 0;
        switch (Global.characterThatWon)
        {
            case Player.Character.LEPRECHAUN_01:
                character = 1;
                break;
            case Player.Character.LEPRECHAUN_USA:
                character = 2;
                break;
            case Player.Character.CLUIRICHAUN:
                character = 3;
                break;
            case Player.Character.FAR_DARRIG:
                character = 4;
                break;
            case Player.Character.FAIRY:
                character = 5;
                break;
            default:
                character = 1;
                break;
        }

        playerText.GetComponent<Animator>().SetInteger("playerWhoWon", Global.playerWhoWon);
        characterSprite.GetComponent<Animator>().SetInteger("characterThatWon", character);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
