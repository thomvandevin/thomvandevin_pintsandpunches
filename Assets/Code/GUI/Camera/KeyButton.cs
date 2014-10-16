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
        if (GUI.Button(new Rect(20, 40, 120, 30), "PunchShake"))
        {
            Global.leprechauns[0].PunchShake(new Vector2(.1f, .1f), 10, .5f, false);
        } 
        if (GUI.Button(new Rect(20, 80, 120, 30), "Damage"))
        {
            Global.leprechauns[1].Damage(5);
        }
    }
}
