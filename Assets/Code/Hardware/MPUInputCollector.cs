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
        //print(digitalInputs[0] + " : " + digitalInputs[1] + " : " + digitalInputs[2] + " : " + digitalInputs[3] + " : " + digitalInputs[4] + " : " + digitalInputs[5]);
        //print(analogInputs[0] + " : " + analogInputs[1] + " : " + analogInputs[2] + " : " + analogInputs[3] + " : " + analogInputs[4] + " : " + analogInputs[5]);
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
