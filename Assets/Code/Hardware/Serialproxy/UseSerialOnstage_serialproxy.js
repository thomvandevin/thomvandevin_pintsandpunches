//
// Setting Variables to be used globally within the JavaScript
//

var InstanceObject : Transform;

var PositionContainer : GameObject;
var Cconnect;
var Manager;
var tempCom;

var InstanceCounter;
var StartingY;

var LocalAnalogArray;
var LocalDigitalArray;
var LocalErrorArray;

var JavaOutputArray;

var CountedInputs = 0;

var DisplayObjectArray = new Array();

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
{	
    tempCom = gameObject.transform.parent.parent.GetComponent("COMParser").com;

    if(tempCom != "COM0")
    {

        //set 'Cconnect' to be used as connector to the C# script.
        Cconnect = GetComponent("Arduino_serialprox_unity");
        Manager = GetComponent("MPUInputCollector");
	
        //count the amount of Inputs
        for(inputcounter = 0; inputcounter < pinConfig.length; inputcounter++) if(pinConfig[inputcounter] == "i") CountedInputs++;
	
        //make all outputs empty first.
        JavaOutputArray = [0,0,0,0,0,0,0,0,0,0,0,0];
        //JavaOutputArray = [0,0,0]; //string for testing
	
        //reset the InstanceCounter
        InstanceCounter = 0;
	
        //save the beginning position (the bottom/base)
        if(PositionContainer==null){
            StartingY = transform.position.y;
        }else{
            StartingY = PositionContainer.transform.position.y;
        }
        //Push the first cube into the DisplayObjectArray array.
        //Create the other cubes
        //Istantiate();
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
        //DisplayObjectMover();
        WriteValues();
    }
}

function Istantiate(){
	//First cube has the same location as the scriptable object.
	if(PositionContainer == null){
		var InternalObj : Transform = Instantiate(InstanceObject,
						Vector3(transform.position.x,
									transform.position.y,
									transform.position.z),
						Quaternion.identity);
		DisplayObjectArray.Push(InternalObj);
	}else{
		var ExternalObj : Transform = Instantiate(InstanceObject,
						Vector3(PositionContainer.transform.position.x,
									PositionContainer.transform.position.y,
									PositionContainer.transform.position.z),
						Quaternion.identity);
		DisplayObjectArray.Push(ExternalObj);
	}
	
	for(AddInstanceInt = 0; AddInstanceInt<CountedInputs-1; AddInstanceInt++){
		InstanceCounter++;
		var newObj : Transform = Instantiate(InstanceObject,
						Vector3(DisplayObjectArray[0].transform.position.x-(14.0*(InstanceCounter%3)),
									DisplayObjectArray[0].transform.position.y/*+(12.0*counter)*/,
									DisplayObjectArray[0].transform.position.z+(14.0*(InstanceCounter/3))),
						Quaternion.identity); //other options are: Random.rotation);//PositionContainer.transform.rotation); //Quaternion.identity = zero
		DisplayObjectArray.Push(newObj);
	}
}

function DisplayObjectMover(){
	for(MoveInstanceInt = 0; MoveInstanceInt<DisplayObjectArray.length; MoveInstanceInt++){
		DisplayObjectArray[MoveInstanceInt].transform.position.y = StartingY+(14.0*(LocalDigitalArray[MoveInstanceInt]));
		DisplayObjectArray[MoveInstanceInt].transform.Rotate(0,(LocalAnalogArray[MoveInstanceInt]/102.4),0);
	}
}

function WriteValues() 
{
    Manager.analogInputs = LocalAnalogArray;
    Manager.digitalInputs = LocalDigitalArray;
    //print(LocalDigitalArray.length);
}