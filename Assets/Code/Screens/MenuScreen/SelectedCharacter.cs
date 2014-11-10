using UnityEngine;
using System.Collections;

public class SelectedCharacter : MonoBehaviour {

    public Vector2 offset = new Vector2(0, 0);
    public Vector2 scale = new Vector2(1, 1);

    //private Animator anim;
    private int selectedCharacter;
    private Material sprite;

	// Use this for initialization
	void Start () 
    {
        selectedCharacter = 1;
        sprite = renderer.sharedMaterial;

        sprite.mainTexture.wrapMode = TextureWrapMode.Repeat;
        sprite.mainTextureOffset = offset;
        sprite.mainTextureScale = scale;

	}
	
	// Update is called once per frame
	void Update () 
    {
        //offset.x += .005f;

        sprite.mainTextureOffset = offset;
        sprite.mainTextureScale = scale;
	}
}
