using UnityEngine;
using System.Collections;

public class MPUInputCollector : MonoBehaviour {

    public int[] analogInputs = new int[6];

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {

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
}
