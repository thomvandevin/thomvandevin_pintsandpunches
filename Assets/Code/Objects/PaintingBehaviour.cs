﻿using UnityEngine;
using System.Collections;

public class PaintingBehaviour : MonoBehaviour {

    public bool usingAnimation = false;
    public bool interactable = true;
    public int delayfrom, delaytill;

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
}
