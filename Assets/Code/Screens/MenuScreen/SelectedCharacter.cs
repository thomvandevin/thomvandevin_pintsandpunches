using UnityEngine;
using System.Collections;

public class SelectedCharacter : MonoBehaviour {

    private Animator anim;
    private int selectedCharacter;

	// Use this for initialization
	void Start () 
    {
        anim = GetComponent<Animator>();
        selectedCharacter = Random.Range(1, 5);

        anim.SetInteger("selectedCharacter", selectedCharacter);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
