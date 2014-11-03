using UnityEngine;
using System.Collections;

public class KeyButton : MonoBehaviour {

    Rect position;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(RectanglePos(1), "PunchShake"))
        {
            Global.leprechauns[0].PunchShake(new Vector2(.1f, .1f), 10, .5f, false);
        }
        if (GUI.Button(RectanglePos(2), "Damage 1"))
        {
            GameObject.Find("Player 1").GetComponentInChildren<Leprechaun>().Damage(5);
        }
        if (GUI.Button(RectanglePos(3), "Damage 2"))
        {
            GameObject.Find("Player 2").GetComponentInChildren<Leprechaun>().Damage(5);
        }
    }

    Rect RectanglePos(int button)
    {
        return new Rect(20, 40 * button, 120, 30);
    }
}
