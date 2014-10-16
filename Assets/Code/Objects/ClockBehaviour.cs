using UnityEngine;
using System.Collections;

public class ClockBehaviour : MonoBehaviour {

    public GameObject hours, minutes;

    private float hour, minute, hourRotation, minuteRotation;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        hour = System.DateTime.Now.Hour;
        minute = System.DateTime.Now.Minute;
        
        hourRotation = -((360/12) * hour + 225);
        minuteRotation = -((360 / 60) * minute + 315);

        hours.RotateTo(new Vector3(0, 0, hourRotation), 0, 0);
        minutes.RotateTo(new Vector3(0, 0, minuteRotation), 0, 0);

	}
}
