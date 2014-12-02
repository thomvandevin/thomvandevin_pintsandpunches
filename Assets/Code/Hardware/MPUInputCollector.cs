using UnityEngine;
using System.Collections;

public class MPUInputCollector : MonoBehaviour {

    public int[] analogInputs = new int[6];
    public int[] digitalInputs = new int[6];

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        //print(digitalInputs[0]);
	}

    public int GetGyroXValue()
    {
        return analogInputs[0];
    }

    public int GetGyroYValue()
    {
        return analogInputs[1];
    }

    public int GetGyroZValue()
    {
        return analogInputs[2];
    }

    public int GetDigitalValue(int port)
    {
        if (port == 9)
            return digitalInputs[0];
        else
            return 999;
    }
}
