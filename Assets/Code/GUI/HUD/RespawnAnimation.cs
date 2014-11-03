using UnityEngine;
using System.Collections;

public class RespawnAnimation : MonoBehaviour {

    private Transform playerTransform;
    private Vector3 target;

    private int controllerNumber;
    private Animator anim;
	// Use this for initialization
    void Start()
    {
    }
 
    public void SetRespawnAnimation(GameObject playerObject)
    {
        playerTransform = playerObject.transform;
        gameObject.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - .1f, -1);

        this.controllerNumber = playerObject.GetComponent<Player>().controllerNumber;
        anim = GetComponent<Animator>();
        anim.SetInteger("controller_number", controllerNumber);
	}

	
	// Update is called once per frame
	void Update () 
    {
	    if( anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .8f)
        {
            Destroy(gameObject);              
        }
	}
}
