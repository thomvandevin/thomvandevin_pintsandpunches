using UnityEngine;
using System.Collections;
using Uniduino;

public class MPU6050 : MonoBehaviour {

    public Arduino arduino;

    public int pinValue;
    public float spinSpeed = 0.2f;

    private GameObject cube;

	// Use this for initialization
	void Start () 
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);

        cube = GameObject.Find("Cube");
	}

    void ConfigurePins()
    {
        arduino.pinMode(2, PinMode.I2C);
        arduino.reportAnalog(2, 1);
        arduino.pinMode(3, PinMode.I2C);
        arduino.reportAnalog(3, 1);
    }
	
	// Update is called once per frame
	void Update () 
    {
        pinValue = arduino.analogRead(2);
        cube.transform.rotation = Quaternion.Euler(0, pinValue * spinSpeed, 0);  
        
	}
}
