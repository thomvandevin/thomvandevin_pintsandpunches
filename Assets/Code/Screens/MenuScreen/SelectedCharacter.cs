using UnityEngine;
using System.Collections;
using GamepadInput;
using System;
using System.Linq;

public class SelectedCharacter : MonoBehaviour
{

    public Vector2 offset = new Vector2(0, 0);
    public Vector2 scale = new Vector2(.2f, 1);

    public float numberOfCharacters, hardness = 1.1f;

    public GamePad.Index player;
    private int playerIndexInt;
    public int GetPlayer { get { return playerIndexInt; } set { playerIndexInt = value; } }

    private Vector2 offset_temp;
    private Vector2 scale_temp;
    private Vector3 startPos;
    private bool pressOnce = false, resettingPos = false, resettingScale = false, characterSelected = false;

    private ColorFlash colorFlash;
    private GameObject selectButton;

    private MPUController mpuController;
    private string tempCom;
    private bool useMPU = false;
    public bool getMPU { get { return useMPU; } set { useMPU = value; } }

    //private Animator anim;
    private int selectedCharacter, joystickCounter;
    private Material sprite;

    public bool useKeyboard = false;

    // Use this for initialization
    void Start()
    {
        selectedCharacter = UnityEngine.Random.Range(1, 5);
        joystickCounter = selectedCharacter;
        offset = new Vector2((1 / numberOfCharacters) * selectedCharacter, 0);
        offset_temp = new Vector2((1 / numberOfCharacters) * selectedCharacter, 0);
        scale_temp = new Vector2((1 / numberOfCharacters), 1);
        startPos = gameObject.transform.position;

        playerIndexInt = GamePad.IndexToInt(player);

        sprite = gameObject.renderer.material;
        sprite.mainTexture.wrapMode = TextureWrapMode.Repeat;
        sprite.mainTextureOffset = offset;
        sprite.mainTextureScale = scale;

        colorFlash = gameObject.GetComponent<ColorFlash>();
        selectButton = Instantiate(Resources.Load("Prefabs/Screens/MenuScreen/Character_Select_SelectButton"), new Vector3(transform.position.x, transform.position.y, -.5f), Quaternion.identity) as GameObject;
        selectButton.transform.parent = gameObject.transform.parent;
        selectButton.SetActive(false);

        useMPU = GameObject.FindGameObjectWithTag("Global").GetComponent<MenuToGame>().mpus[playerIndexInt - 1];

        if (useMPU)
        {
            tempCom = gameObject.transform.parent.GetComponent<COMParser>().com;
            mpuController = gameObject.AddComponent<MPUController>();
            mpuController.controllerNumber = playerIndexInt;
        }

        Invoke("AddPlayerCharacter", 0.01f);
    }

    private void AddPlayerCharacter()
    {
        if (GameObject.FindGameObjectWithTag("Global").GetComponent<MenuToGame>() == null)
            Invoke("AddPlayerCharacter", 0.01f);
        else
            GameObject.FindGameObjectWithTag("Global").GetComponent<MenuToGame>().playerCharacter.Add(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterSelected)
        {
            //print(mpuController.GetSensorValue(MPUController.Axis.X));


            if (useMPU && tempCom != "COM0")
            {
                if (mpuController.GetSensor(MPUController.Axis.X, MPUController.Side.POSITIVE) && !pressOnce)
                {
                    NextCharacter(-1);
                }
                else if (mpuController.GetSensor(MPUController.Axis.X, MPUController.Side.NEGATIVE) && !pressOnce)
                {
                    NextCharacter(1);
                }
                else if (!mpuController.GetSensor(MPUController.Axis.X, MPUController.Side.NEGATIVE) && !mpuController.GetSensor(MPUController.Axis.X, MPUController.Side.POSITIVE) && pressOnce)
                    pressOnce = false;

                if(mpuController.GetDigitalPressed(9))
                    SelectCharacter();

                //print(mpuController.GetDigital(9));
            }
            else if (useKeyboard)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    NextCharacter(-1);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    NextCharacter(1);
                }
                
                if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) && pressOnce)
                    pressOnce = false;

                if (Input.GetKeyDown(KeyCode.Space))
                    SelectCharacter();
            }
            else
            {
                if (GamePad.GetAxis(GamePad.Axis.LeftStick, player).x < -.3f && !pressOnce)
                {
                    NextCharacter(-1);
                }
                else if (GamePad.GetAxis(GamePad.Axis.LeftStick, player).x > .3f && !pressOnce)
                {
                    NextCharacter(1);
                }
                else if (GamePad.GetAxis(GamePad.Axis.LeftStick, player).x > -.3f && GamePad.GetAxis(GamePad.Axis.LeftStick, player).x < .3f && pressOnce)
                    pressOnce = false;

                if (GamePad.GetButtonDown(GamePad.Button.A, player))
                    SelectCharacter();
            }
                

            if (offset != offset_temp)
            {
                offset = Vector2.Lerp(offset, offset_temp, Time.deltaTime * hardness);
                if (Vector2.Distance(offset, offset_temp) < .001f)
                    offset = offset_temp;
            }
            else if (resettingScale)
                resettingScale = false;

            if (scale != scale_temp)
            {
                scale = Vector2.Lerp(scale, scale_temp, Time.deltaTime * hardness);
                if (Vector2.Distance(scale, scale_temp) < .001f)
                    scale = scale_temp;
            }

            if (resettingPos && Vector3.Distance(gameObject.transform.position, startPos) > 0.0001f)
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, startPos, Time.deltaTime * hardness);
            else if (resettingPos && Vector3.Distance(gameObject.transform.position, startPos) < 0.0001f)
            {
                resettingPos = false;
                gameObject.transform.position = startPos;
            }

            if (selectedCharacter > numberOfCharacters)
                selectedCharacter = 1;
            else if (selectedCharacter < 1)
                selectedCharacter = 5;

        }
        else if (useMPU && mpuController.GetDigitalPressed(9))
        {
            selectButton.SetActive(false);
            characterSelected = false;
            GameObject.FindGameObjectWithTag("Global").GetComponent<MenuToGame>().playerCharacter[playerIndexInt - 1] = 0;
            
        }
        else if (useKeyboard && Input.GetKeyDown(KeyCode.Space))
        {
            selectButton.SetActive(false);
            characterSelected = false;
            GameObject.FindGameObjectWithTag("Global").GetComponent<MenuToGame>().playerCharacter[playerIndexInt - 1] = 0;
        }
        else if (GamePad.GetButtonDown(GamePad.Button.A, player) || GamePad.GetButtonDown(GamePad.Button.B, player))
        {
            selectButton.SetActive(false);
            characterSelected = false;
            GameObject.FindGameObjectWithTag("Global").GetComponent<MenuToGame>().playerCharacter[playerIndexInt - 1] = 0;
        }

        sprite.mainTextureOffset = offset;
        sprite.mainTextureScale = scale;
    }

    private void NextCharacter(int side)
    {
        pressOnce = true;
        offset_temp.x += side * (1 / numberOfCharacters);
        gameObject.PunchPosition(new Vector3(side * 1.5f, 0, 0), .2f, 0);
        Invoke("ResetPosition", .2f);

        selectedCharacter += side;
        joystickCounter += side;
    }

    private void ResetPosition()
    {
        resettingPos = true;
    }

    private void SelectCharacter()
    {
        GameObject.FindGameObjectWithTag("Global").GetComponent<MenuToGame>().playerCharacter[playerIndexInt - 1] = selectedCharacter;
        scale_temp = new Vector2((1 / numberOfCharacters) - .05f, .6f);
        offset_temp = new Vector2(offset.x + .025f, offset.y + .3f);
        colorFlash.FlashToColor(new Color(2000, 2000, 2000, 1000), 0.0f, .1f);
        selectButton.SetActive(true);
        characterSelected = true;
        selectButton.transform.position = new Vector3(transform.position.x + UnityEngine.Random.Range(-.35f, .35f), transform.position.y - .3f + UnityEngine.Random.Range(-.35f, .35f), selectButton.transform.position.z);
        Invoke("ResetPosScale", .1f);
    }

    private void ResetScale()
    {
        resettingScale = true;
        scale_temp = new Vector2((1 / numberOfCharacters), 1);
    }

    private void ResetPosScale()
    {
        resettingScale = true;
        scale_temp = new Vector2((1 / numberOfCharacters), 1);
        //if(offset_temp.x > 1 || offset_temp.x < 0)
        //{
        //    float a = offset_temp.x % 1;
        //    int b = Mathf.RoundToInt(offset_temp.x);
        //    //float b = Math.Truncate(offset_temp.x * 1000) / 1000;
        //    if(a >= .5 && b > 0)
        //        offset_temp = new Vector2(b + 1 + (1 / numberOfCharacters) * selectedCharacter, 0);
        //    else if(a> .5 && b > 0)
        //        offset_temp = new Vector2(b + (1 / numberOfCharacters) * selectedCharacter, 0);
        //    else if (a <= .5 && b < 0)
        //        offset_temp = new Vector2(b - 1 + (1 / numberOfCharacters) * selectedCharacter, 0);
        //    else if (a < .5 && b < 0)
        //        offset_temp = new Vector2(b + (1 / numberOfCharacters) * selectedCharacter, 0);

        //    print(a.ToString() + " & " + b.ToString());
        //}
        //else
        //    offset_temp = new Vector2((1 / numberOfCharacters) * selectedCharacter, 0);
        offset_temp = new Vector2(((1 / numberOfCharacters) * joystickCounter), 0);
        resettingPos = true;
    }

}
