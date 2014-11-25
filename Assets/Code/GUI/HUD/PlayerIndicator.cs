using UnityEngine;
using System.Collections;

public class PlayerIndicator : MonoBehaviour {

    public float hardness = 15f;

    private Animator anim;
    private Transform playerTransform;
    private Vector3 target;
    private float playerCorrection = 0;

	// Use this for initialization
	void Start () 
    {

	}

    public void SetIndicator(GameObject playerObject, int controller)
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetInteger("controllerNumber", controller);

        playerTransform = playerObject.transform;
        target = new Vector3(playerTransform.position.x, playerTransform.position.y + 1, playerTransform.position.z);

        if (playerObject.GetComponent<Player>().chosenCharacter == Player.Character.FAR_DARRIG)
            playerCorrection = .35f;

        if (playerObject.GetComponent<Player>().chosenCharacter == Player.Character.LEPRECHAUN_01)
            playerCorrection = .1f;

        if (playerObject.GetComponent<Player>().chosenCharacter == Player.Character.LEPRECHAUN_USA)
            playerCorrection = .2f;

        if (playerObject.GetComponent<Player>().chosenCharacter == Player.Character.CLUIRICHAUN ||
            playerObject.GetComponent<Player>().chosenCharacter == Player.Character.FAIRY)
            playerCorrection = -.1f;
    }
	
	// Update is called once per frame
	void Update () 
    {
        target = new Vector3(playerTransform.position.x, playerTransform.position.y + 1 + playerCorrection, playerTransform.position.z);
        gameObject.transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * hardness);

	}

}
