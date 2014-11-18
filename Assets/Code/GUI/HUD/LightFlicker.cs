﻿using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

    public float hardness = 1;
    public float offset = 0;

    private Light light;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        light.range += Mathf.Sin(Time.time * hardness + offset);
	}
}