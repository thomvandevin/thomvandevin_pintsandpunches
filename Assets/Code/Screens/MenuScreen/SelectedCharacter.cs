using UnityEngine;
using System.Collections;
using GamepadInput;

public class SelectedCharacter : MonoBehaviour {

    public Vector2 offset = new Vector2(0, 0);
    public Vector2 scale = new Vector2(.2f, 1);

    public float numberOfCharacters, hardness = 1.1f;

    public GamePad.Index player;

    private Vector2 offset_temp = new Vector2(0, 0);
    private Vector2 scale_temp;
    private Vector3 startPos;
    private bool pressOnce = false, punchOnce = false, resettingPos = false, resettingScale = false;
    

    //private Animator anim;
    private int selectedCharacter;
    private Material sprite;

	// Use this for initialization
	void Start () 
    {
        selectedCharacter = Random.Range(1, 5);
        offset = new Vector2((1 / numberOfCharacters) * selectedCharacter, 0);
        offset = new Vector2((1 / numberOfCharacters), 1);
        scale_temp = new Vector2((1 / numberOfCharacters), 1);
        startPos = gameObject.transform.position;

        sprite = gameObject.renderer.material;
        sprite.mainTexture.wrapMode = TextureWrapMode.Repeat;
        sprite.mainTextureOffset = offset;
        sprite.mainTextureScale = scale;

	}
	
	// Update is called once per frame
	void Update () 
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

        if (offset != offset_temp)
        {
            offset = Vector2.Lerp(offset, offset_temp, Time.deltaTime * hardness);
            if (Vector2.Distance(offset, offset_temp) < .001f)
                offset = offset_temp;
        }

        if (scale != scale_temp)
        {
            scale = Vector2.Lerp(scale, scale_temp, Time.deltaTime * hardness);
            if (Vector2.Distance(scale, scale_temp) < .001f)
                scale = scale_temp;
        }

        if (resettingPos && Vector3.Distance(gameObject.transform.position, startPos) < 0.0001f)
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, startPos, Time.deltaTime * hardness);
        else if (resettingPos && Vector3.Distance(gameObject.transform.position, startPos) > 0.0001f)
        {
            resettingPos = false;
            gameObject.transform.position = startPos;
        }

        if (selectedCharacter > numberOfCharacters)
            selectedCharacter = 1;
        else if (selectedCharacter < 1)
            selectedCharacter = 5;

        if(player == GamePad.Index.One)
            print(selectedCharacter);


        sprite.mainTextureOffset = offset;
        sprite.mainTextureScale = scale;
	}

    private void NextCharacter(int side)
    {
        pressOnce = true;
        offset_temp.x += side*(1 / numberOfCharacters);
        gameObject.PunchPosition(new Vector3(side*1.5f, 0, 0), .2f, 0);
        Invoke("ResetPosition", .2f);

        selectedCharacter += side;
    }

    private void ResetPosition()
    {
        resettingPos = true;
    }

    private void SelectCharacter()
    {
        scale_temp = new Vector2((1 / numberOfCharacters) - .1f, .1f);
        offset_temp = new Vector2((1 / (numberOfCharacters / .1f)) * selectedCharacter, (1 / (numberOfCharacters / .1f)) * selectedCharacter);
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
        resettingPos = true;
        offset_temp = new Vector2(-(1 / (numberOfCharacters / .1f)) * selectedCharacter, -(1 / (numberOfCharacters / .1f)) * selectedCharacter);
    }


}
