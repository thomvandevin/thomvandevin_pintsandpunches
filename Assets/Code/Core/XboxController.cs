using UnityEngine;
using System.Collections;

public class XboxController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool GetButton(int joystick, int button)
    {
        return Input.GetButton(JoystickButton(joystick, button));
    }

    public bool GetButtonDown(int joystick, int button)
    {
        return Input.GetButtonDown(JoystickButton(joystick, button));
    }

    public bool GetButtonUp(int joystick, int button)
    {
        return Input.GetButtonUp(JoystickButton(joystick, button));
    }
    
    public float GetAxis(int joystick, string axis)
    {
        return Input.GetAxis(JoystickAxis(joystick, axis));
    }

    public string JoystickButton(int joystick, int button)
    {
        return "joystick " + joystick.ToString() + " button " + button.ToString();
    }

    public string JoystickAxis(int joystick, string axis)
    {
        return "joystick " + joystick.ToString() + " " + axis.ToString() + " axis";
    }

}
