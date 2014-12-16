using UnityEngine;
using System.Collections;

public class ClockBehaviour : MonoBehaviour {

    public GameObject hours, minutes;

    public float spinHardness;

    private float hour, minute, hourRotation, minuteRotation;
    private int timer, maxTime;
    private bool clockSpin = false, clockReset = true;

    private Vector2 currentRotation, previousRotation;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (clockSpin)
        {
            if (timer >= maxTime)
            {
                if(clockReset)
                    ResetClock();
                else
                {
                    currentRotation = new Vector2(hourRotation, minuteRotation);
                    float distance = Vector2.Distance(currentRotation, previousRotation);
                    if (distance > 0.1f)
                    {
                        currentRotation = Vector2.Lerp(currentRotation, previousRotation, Time.deltaTime);
                    }
                    else
                        clockReset = true;
                }

            }
            else
            {
                timer++;

                hourRotation = -((360 / 12)/10  * spinHardness);
                minuteRotation = -((360 / 60)/10  * spinHardness);

                hours.RotateBy(new Vector3(0, 0, hourRotation), 0, 0);
                minutes.RotateBy(new Vector3(0, 0, minuteRotation), 0, 0);

            }

        } else
        {
            hour = System.DateTime.Now.Hour;
            minute = System.DateTime.Now.Minute;

            hourRotation = -((360 / 12) * hour + 225 + ((360 / 60) * minute / 12));
            minuteRotation = -((360 / 60) * minute + 315);

            hours.RotateTo(new Vector3(0, 0, hourRotation), 0, 0);
            minutes.RotateTo(new Vector3(0, 0, minuteRotation), 0, 0);
        }

	}

    public void ClockSpin()
    {
        clockSpin = true;
        maxTime = Random.Range(160, 190);
        previousRotation = new Vector2(hourRotation, minuteRotation);
    }

    public void ResetClock()
    {
        clockSpin = false;
        timer = 0;
    }
}
