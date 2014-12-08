using UnityEngine;
using System.Collections;

public class PaintingBehaviour : MonoBehaviour {

    public bool usingAnimation = false;
    public bool interactable = true;
    public int delayfrom, delaytill;
    public GameObject pivot;

    private int randomDelay, timer = 0;
    private Animator anim;
    private bool triggerOnce = false;

	void Start () 
    {
        if(usingAnimation)
        {
            randomDelay = Random.Range(delayfrom, delaytill);
            anim = gameObject.GetComponent<Animator>();
        }

        if(interactable)
        {
            Global.environmentPaintings.Add(gameObject);
        }

        if (pivot != null)
        {
            pivot.transform.parent = GameObject.FindGameObjectWithTag("Paintings").gameObject.transform;
            gameObject.transform.parent = pivot.transform;
        }

	}
	
	void Update () 
    {
        if(usingAnimation)
        {
            if (timer >= randomDelay)
            {
                StartAnimation("startAnimation");
                randomDelay = Random.Range(delayfrom, delaytill);
                timer = 0;
            }
            else
                timer++;

            if (triggerOnce && anim.IsInTransition(0))
                ResetBool("startAnimation");
        }


	}

    private void StartAnimation(string booleann)
    {
        anim.SetBool(booleann, true);
        triggerOnce = true;
    }

    private void ResetBool(string booleann)
    {
        anim.SetBool(booleann, false);
        triggerOnce = false;
    }

    public void KnockPaintingOff()
    {
        gameObject.AddComponent<Rigidbody2D>();
        float rndX = Random.Range(-100, 100);
        float rndY = Random.Range(0, 40);
        float rndT = Random.Range(30, 60);
        if (rndX > 0)
            rndT *= -1;

        gameObject.rigidbody2D.AddForce(new Vector2(rndX, rndY));
        gameObject.rigidbody2D.AddTorque(rndT);
        Global.environmentPaintings.Remove(gameObject);
        Destroy(gameObject, 1f);
    }
}
