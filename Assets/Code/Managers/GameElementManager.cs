using UnityEngine;
using System.Collections;

public class GameElementManager : MonoBehaviour {

    private float shakiness;
    public float Shakiness { get { return shakiness; } set { shakiness = value; } }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (shakiness > 400)
            KnockGlassOrPaintingOff();
        else if (shakiness > 1)
            shakiness -= shakiness / 5;
        else
            shakiness = 0;
	}

    public void AddShakiness(float shake)
    {
        shakiness += shake;
    }

    public void KnockGlassOrPaintingOff()
    {

    }

    public void KnockPaintingOff()
    {

    }

    public void KnockGlassOff()
    {

    }
}
