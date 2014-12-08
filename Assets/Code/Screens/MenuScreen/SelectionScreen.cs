using UnityEngine;
using System.Collections;
using GamepadInput;

public class SelectionScreen : MonoBehaviour {

    public GamePad.Index playerIndex;

	// Use this for initialization
	void Start () 
    {
        GameObject characterImage = Instantiate(Resources.Load("Prefabs/Screens/MenuScreen/Selection_Character_Polaroid"), transform.position, Quaternion.identity) as GameObject;
        characterImage.transform.parent = gameObject.transform;
        SelectedCharacter sChar = characterImage.GetComponent<SelectedCharacter>();
        sChar.player = playerIndex;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
