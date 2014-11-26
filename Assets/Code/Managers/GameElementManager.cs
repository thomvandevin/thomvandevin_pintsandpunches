using UnityEngine;
using System.Collections;

public class GameElementManager : MonoBehaviour {

    private float shakiness = 0;
    public float Shakiness { get { return shakiness; } set { shakiness = value; } }

    public int maxShakiness = 200;
    public float shakeDivider = 100;

	// Use this for initialization
	void Start () 
    {
	
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
        int rnd = Random.Range(1, 8);
        if (rnd <= 5)
        {
            if (Global.environmentGlasses.Count > 0)
                KnockOffGlass();
            else if (Global.environmentPaintings.Count > 0)
                KnockOffPainting();
        }
        else
        {
            if (Global.environmentPaintings.Count > 0)
                KnockOffPainting();
            else if (Global.environmentGlasses.Count > 0)
                KnockOffGlass();
        }
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
                Global.environmentGlasses[rnd].GetComponent<BottleBehaviour>().KnockBottleOff();
            }

        }
    }
}
