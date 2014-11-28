using UnityEngine;
using System.Collections;

public class GameElementManager : MonoBehaviour {

    private float shakiness = 0;
    public float Shakiness { get { return shakiness; } set { shakiness = value; } }

    public int maxShakiness = 200;
    public float shakeDivider = 100;

    private ClockBehaviour clock;

	// Use this for initialization
	void Start () 
    {
        clock = GameObject.Find("Clock").GetComponent<ClockBehaviour>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (shakiness > maxShakiness)
        {
            if (Global.environmentGlasses.Count > 0 || Global.environmentPaintings.Count > 0)
            {
                KnockGlassOrPaintingOff();
                int rnd = Random.Range(100, 150);
                AddShakiness(-rnd);
            }
            else
            {
                clock.ClockSpin(); 
                int rnd = Random.Range(100, 150);
                AddShakiness(-rnd);
            }
        }
        else if (shakiness > 1)
            shakiness -= shakiness / shakeDivider;
        else
            shakiness = 0;

	}

    public void AddShakiness(float shake)
    {
        shakiness += shake;
    }

    public void KnockGlassOrPaintingOff()
    {
        int rnd = Random.Range(1, 20);
        if (rnd <= 13)
        {
            if (Global.environmentGlasses.Count > 0)
                KnockOffGlass();
            else if (Global.environmentPaintings.Count > 0)
                KnockOffPainting();
        }
        else if (rnd > 13 && rnd <= 18)
        {
            if (Global.environmentPaintings.Count > 0)
                KnockOffPainting();
            else if (Global.environmentGlasses.Count > 0)
                KnockOffGlass();
        }
        else
            clock.ClockSpin();
    }

    public void KnockOffPainting()
    {
        if (Global.environmentPaintings.Count > 0)
        {

        }
    }

    public void KnockOffGlass()
    {
        if (Global.environmentGlasses.Count > 0)
        {
            int rnd = Random.Range(0, Global.environmentGlasses.Count + 1);
            Global.environmentGlasses[rnd].GetComponent<BottleBehaviour>().KnockBottleOff();

            int rnd2 = Random.Range(0, 100);
            if (rnd2 > 80 && Global.environmentGlasses.Count > 0)
            {
                int rnd3 = Random.Range(0, Global.environmentGlasses.Count + 1);
                Global.environmentGlasses[rnd3].GetComponent<BottleBehaviour>().KnockBottleOff();
            }

        }
    }

    public void SpinClock()
    {

    }
}
