using UnityEngine;
using System.Collections;

public class KeyButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 40, 80, 20), "Button"))
        {
            Global.leprechauns[0].Screenshake(new Vector2(.1f, .1f), 10, .5f);
        }
    }
}
