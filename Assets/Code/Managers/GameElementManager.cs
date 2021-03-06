﻿using UnityEngine;
using System.Collections;

public class GameElementManager : MonoBehaviour {

    private float shakiness = 0;
    public float Shakiness { get { return shakiness; } set { shakiness = value; } }

    public int maxShakiness = 200;
    public float shakeDivider = 100;

    private ClockBehaviour clock;
    private AdvancedCamera acamera;
    private GameObject bigEventObject;

    private int bigEventTimer, bigEventMaxTimer;
    private bool bigEvent = false;

	// Use this for initialization
	void Start () 
    {
        clock = GameObject.Find("Clock").GetComponent<ClockBehaviour>();

        acamera = Camera.main.GetComponent<AdvancedCamera>();
        bigEventTimer = 0;
        bigEventMaxTimer = Random.Range(200, 400);
        bigEventObject = Global.getChildGameObject(Camera.main.gameObject, "Clash Event");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (shakiness > maxShakiness)
        {
            SmallEvent();
        }
        else if (shakiness > 1)
            shakiness -= shakiness / shakeDivider;
        else
            shakiness = 0;

        if(!bigEvent)
        {
            if (bigEventTimer < bigEventMaxTimer)
                bigEventTimer++;
            else if (bigEventTimer >= bigEventMaxTimer)
            { }
                //BigEvent();
        } else
        {
            UpdateBigEvent();
        }
        
	}

    public void AddShakiness(float shake)
    {
        shakiness += shake;
    }

    public void SmallEvent()
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
            float rndmf = Random.Range(.9f, 1.5f);
            int rnd = Random.Range(0, Global.environmentPaintings.Count + 1);
            Global.environmentPaintings[rnd].GetComponent<PaintingBehaviour>().KnockPainting(rndmf);

            int rnd2 = Random.Range(0, 100);
            if (rnd2 > 80 && Global.environmentPaintings.Count > 0)
            {
                int rnd3 = Random.Range(0, Global.environmentPaintings.Count + 1);
                Global.environmentPaintings[rnd3].GetComponent<PaintingBehaviour>().KnockPainting();
            }
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

    public void BigEvent()
    {
        bigEvent = true;
        acamera.ZoomOnly();
        bigEventObject.SetActive(true);
    }

    public void UpdateBigEvent()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ResetBigEvent();
    }

    public void ResetBigEvent()
    {
        bigEvent = false;
        bigEventTimer = 0;
        bigEventMaxTimer = Random.Range(20000, 30000);
        acamera.ZoomOutOnly();
        bigEventObject.SetActive(false);
        BigEventEffect();
    }

    public void BigEventEffect()
    {
        int rnd = Random.Range(5, 15);
        for (int i = 0; i < rnd; i++)
        {
            SmallEvent();
        }
        acamera.ScreenShake(rnd / 5);
    }

}
