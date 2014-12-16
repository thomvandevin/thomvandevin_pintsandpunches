using UnityEngine;
using System.Collections;

public class MPUController : MonoBehaviour {

    public enum Axis
	{
        X,
        Y,
        Z
	}

    public enum Side
    {
        POSITIVE,
        NEGATIVE
    }

    public int controllerNumber = 0;
    
    private GameObject hardware;
    private MPUInputCollector mpuData;
    private float thresholdX = .35f;
    private float thresholdY = .65f;
    private float thresholdZ = .5f;

    private bool prevState9 = false;

	// Use this for initialization
	void Start () 
    {
        hardware = Instantiate(Resources.Load("Prefabs/Hardware/Hardware"), gameObject.transform.position, Quaternion.identity) as GameObject;
        hardware.transform.parent = gameObject.transform;
        mpuData = hardware.GetComponent<MPUInputCollector>();

	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    void LateUpdate()
    {
        prevState9 = GetDigital(9);
    }

    public float GetSensorValue(Axis axis)
    {
        //max values: -650,650
        if (axis == Axis.X){
            float xValue = 0f + mpuData.GetGyroXValue() / 650f;
            return xValue;
        }
        else if (axis == Axis.Y){
            float yValue = 0f + mpuData.GetGyroYValue() / 650f;
            return yValue;
        }
        else if (axis == Axis.Z)
        {
            float zValue = 0f + mpuData.GetGyroZValue() / 650f;
            return zValue;
        }
        else
            return 0;
    }

    public bool GetSensor(Axis axis, Side side)
    {
        if (axis == Axis.X)
        {
            if (side == Side.POSITIVE && GetSensorValue(axis) >= thresholdX)
                return true;
            else if (side == Side.NEGATIVE && GetSensorValue(axis) <= -thresholdX)
                return true;
            else
                return false;
        }
        else if (axis == Axis.Y)
        {
            if (side == Side.POSITIVE && GetSensorValue(axis) >= thresholdY)
                return true;
            else if (side == Side.NEGATIVE && GetSensorValue(axis) <= -thresholdY)
                return true;
            else
                return false;
        }
        else if (axis == Axis.Z)
        {
            if (side == Side.POSITIVE && GetSensorValue(axis) >= thresholdZ)
                return true;
            else if (side == Side.NEGATIVE && GetSensorValue(axis) <= thresholdZ)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public int GetDigitalValue(int port)
    {
        if (port == 9)
            return mpuData.GetDigitalValue(port);
        else
            return 999;
    }

    public bool GetDigital(int port)
    {
        if (port == 9)
            if (mpuData.GetDigitalValue(port) == 1)
                return true;
            else
                return false;
        else
            return false;
    }

    public bool GetDigitalPressed(int port)
    {
        if (port == 9 && prevState9 != GetDigital(9) && GetDigital(9) == true)
            return true;
        else 
            return false;

    }
}
