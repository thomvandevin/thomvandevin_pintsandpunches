using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

    public float hardness = 1;
    public float offset = 0;

    private Light lightt;

	// Use this for initialization
	void Start () {
        lightt = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        lightt.range += Mathf.Sin(Time.time * hardness + offset);
	}
}
