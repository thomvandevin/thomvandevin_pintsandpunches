using UnityEngine;
using System.Collections;

public class Punchbag : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () 
    {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Hit(Vector2 punchPosition)
    {
        Vector3 punchDirection = Vector3.zero;
        if (punchPosition.x > transform.position.x)
            punchDirection.x = 1;
        else if (punchPosition.x < transform.position.x)
            punchDirection.x = -1;
        
        gameObject.rigidbody2D.AddForce(new Vector2(-punchDirection.x * 80 * 8, 800 / 3.5f));
        anim.SetTrigger("hit");
    }
}
