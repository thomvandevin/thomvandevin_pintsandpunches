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

    private bool startRotating = false;
    private float hardness = 1;
    private int numberOfHits, side = 1;

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
            numberOfHits = Random.Range(1, 5);
        }

        if (pivot != null)
        {
            pivot.transform.parent = GameObject.FindGameObjectWithTag("Paintings").gameObject.transform;
            gameObject.transform.parent = pivot.transform;
            if(Mathf.Abs(Vector3.Distance(gameObject.transform.position, pivot.transform.position)) > 1)
                side = -1;
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


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pivot != null)
                KnockPainting();
        }

        if (startRotating)
            StartRotating();
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
    
    public void KnockPainting()
    {
        if (numberOfHits > 1)
        {
            startRotating = true;
            this.hardness = 1;
            Invoke("StopRotating", .05f);
            numberOfHits--;
        }
        else
            KnockPaintingOff();
    }

    public void KnockPainting(float hardness)
    {
        if (numberOfHits > 1)
        {
            startRotating = true;
            this.hardness = hardness;
            Invoke("StopRotating", .05f);
            numberOfHits--;
        }
        else
            KnockPaintingOff();
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

    private void StartRotating()
    {
        gameObject.transform.RotateAround(pivot.transform.position, Vector3.forward, Time.deltaTime * -200f * side);
        gameObject.transform.parent.gameObject.PunchRotation(-Vector3.forward * side * 20 * hardness, .5f, 0f);
    }

    private void StopRotating()
    {
        startRotating = false;
    }
}
