﻿using UnityEngine;
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
    private float threshold = .6f;

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
            if (side == Side.POSITIVE && GetSensorValue(axis) >= threshold)
                return true;
            else if (side == Side.NEGATIVE && GetSensorValue(axis) <= -threshold)
                return true;
            else
                return false;
        }
        else if (axis == Axis.Y)
        {
            if (side == Side.POSITIVE && GetSensorValue(axis) >= threshold)
                return true;
            else if (side == Side.NEGATIVE && GetSensorValue(axis) <= -threshold)
                return true;
            else
                return false;
        }
        else if (axis == Axis.Z)
        {
            if (side == Side.POSITIVE && GetSensorValue(axis) >= threshold)
                return true;
            else if (side == Side.NEGATIVE && GetSensorValue(axis) <= threshold)
                return true;
            else
                return false;
        }
        else
            return false;
    }
}
