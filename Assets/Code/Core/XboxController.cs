using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XboxController : MonoBehaviour {

    public enum XboxButtons
    {
        A,
        B,
        X,
        Y,
        LT,
        RT,
        BACK,
        START,
        L3,
        R3
    }

    public Dictionary<XboxButtons, int> xboxStrings;

	// Use this for initialization
	void Start () 
    {
        SetDictionary();
	}

    public void SetDictionary()
    {
        xboxStrings = new Dictionary<XboxButtons, int>();
        xboxStrings.Add(XboxButtons.A, 0);
        xboxStrings.Add(XboxButtons.B, 1);
        xboxStrings.Add(XboxButtons.X, 2);
        xboxStrings.Add(XboxButtons.Y, 3);
        xboxStrings.Add(XboxButtons.LT, 4);
        xboxStrings.Add(XboxButtons.RT, 5);
        xboxStrings.Add(XboxButtons.BACK, 6);
        xboxStrings.Add(XboxButtons.START, 7);
        xboxStrings.Add(XboxButtons.L3, 8);
        xboxStrings.Add(XboxButtons.R3, 9);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool GetButton(int joystick, XboxButtons button)
    {
        return Input.GetButton(JoystickButton(joystick, button));
    }

    public bool GetButtonDown(int joystick, XboxButtons button)
    {
        return Input.GetButtonDown(JoystickButton(joystick, button));
    }

    public bool GetButtonUp(int joystick, XboxButtons button)
    {
        return Input.GetButtonUp(JoystickButton(joystick, button));
    }
    
    public float GetAxis(int joystick, string axis)
    {
        return Input.GetAxis(JoystickAxis(joystick, axis));
    }

    public string JoystickButton(int joystick, XboxButtons button)
    {
        string buttonString = xboxStrings[button].ToString();
        return "joystick" + joystick.ToString() + "button" + buttonString;
    }

    public string JoystickAxis(int joystick, string axis)
    {
        return "joystick" + joystick.ToString() + "" + axis.ToString() + "axis";
    }

}
