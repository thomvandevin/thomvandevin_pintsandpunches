//
// Setting Variables to be used globally within the JavaScript
//

var Cconnect;
var Manager;

var LocalAnalogArray;
var LocalDigitalArray;
var LocalErrorArray;

var JavaOutputArray;

var CountedInputs = 0;

var tempCom;

var pinConfig : Array =
[
	// COPY-PASTE PIN CONFIGURATION BELOW ALSO TO ARDUINO --
	"i",  // Pin 2  'i' for in 'o' for out
	"i",  // Pin 3  'i' for in 'o' for out or 'p' for pwm
	"i",  // Pin 4  'i' for in 'o' for out
	"i",  // Pin 5  'i' for in 'o' for out or 'p' for pwm
	"i",  // Pin 6  'i' for in 'o' for out or 'p' for pwm
	"i",  // Pin 7  'i' for in 'o' for out
	"o",  // Pin 8  'i' for in 'o' for out
	"o",  // Pin 9  'i' for in 'o' for out or 'p' for pwm
	"o",  // Pin 10 'i' for in 'o' for out or 'p' for pwm
	"o",  // Pin 11 'i' for in 'o' for out or 'p' for pwm
	"o",  // Pin 12 'i' for in 'o' for out
	"o"   // Pin 13 LedPin 'i' for in 'o' for out 
];

function Start()
{	//set 'Cconnect' to be used as connector to the C# script.
    tempCom = gameObject.transform.parent.parent.GetComponent("COMParser").com;

    if(tempCom != "COM0")
    {

        Cconnect = GetComponent("GuiArduinoSerialScript");
        Manager = GetComponent("MPUInputCollector");
	
        //count the amount of Inputs
        for(inputcounter = 0; inputcounter < pinConfig.length; inputcounter++) if(pinConfig[inputcounter] == "i") CountedInputs++;
	
        //make all outputs empty first.
        JavaOutputArray = [0,0,0,0,0,0,0,0,0,0,0,0];
        //JavaOutputArray = [0,0,0]; //string for testing
	
        //reset the InstanceCounter
        InstanceCounter = 0;
	
        //save the beginning position (the bottom/base)

        //Push the first cube into the DisplayObjectArray array.
        //Create the other cubes
                
    }
}

function Update()
{

    if(tempCom != "COM0")
    {

        //
        // Receiving the Arrays that represent the INPUTS
        //
        LocalAnalogArray = Cconnect.AnalogReceive();
        LocalDigitalArray = Cconnect.DigitalReceive();
        LocalErrorArray = Cconnect.ErrorReceive();
	
        //Cconnect.arrayprinter(LocalDigitalArray, "LocalDigitalArray:"); //prints the received arrays
        //Cconnect.arrayprinter(LocalAnalogArray, "LocalAnalogArray:"); //prints the received arrays
	
		
        //Dont use: JavaOutputArray = [8]; //this will reset the entire output array!
        //According to pinConfig, your pins are either PWM, or OUTPUTS. if pinConfig made something an INPUT, and you set that pin, NOTHING will happen.
        JavaOutputArray[2] = 282;
        JavaOutputArray[1] = 8;
	
        //make sure the variables are within bounds.
        for (i = 0; i < JavaOutputArray.Length; i++){
            if(JavaOutputArray[i] > 255){
                JavaOutputArray[i] = 255;
            }else if(JavaOutputArray[i] < 0){
                JavaOutputArray[i] = 0;
            }
        }
	
        //
        // Sending the Array to C#, after which C# sends it to the Arduino as OUTPUTS (not working yet)
        //Cconnect.OutputSender(JavaOutputArray);
        WriteValues();

    }
}

//function DisplayObjectMover(){
//	for(MoveInstanceInt = 0; MoveInstanceInt<DisplayObjectArray.length; MoveInstanceInt++){
//		DisplayObjectArray[MoveInstanceInt].transform.position.y = StartingY+(14.0*(LocalDigitalArray[MoveInstanceInt]));
//		DisplayObjectArray[MoveInstanceInt].transform.Rotate(0,(LocalAnalogArray[MoveInstanceInt]/102.4),0);
//	}
//}

function WriteValues() 
{
    Manager.analogInputs = LocalAnalogArray;
}