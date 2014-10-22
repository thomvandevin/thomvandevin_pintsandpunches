using UnityEngine;
using System.Collections;

public class PlayerIndicator : MonoBehaviour {

    public float hardness = 15f;

    private Animator anim;
    private Transform playerTransform;
    private Vector3 target;

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
    }
	
	// Update is called once per frame
	void Update () 
    {
        target = new Vector3(playerTransform.position.x, playerTransform.position.y + 1, playerTransform.position.z);
        gameObject.transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * hardness);

	}

}
