using UnityEngine;
using System.Collections;

public class AdvancedCamera : MonoBehaviour {

    private Vector3 newPosition;
    private bool on = true;

	// Use this for initialization
	void Start () {
        newPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if (on)
        {
            newPosition = Vector3.zero;

            foreach (GameObject p in Global.players)
            {
                newPosition += p.transform.position;
            }

            newPosition = (newPosition / Global.players.Count);
            newPosition.z = -20;
            if (newPosition.x > 4)
                newPosition.x = 4;
            if (newPosition.x < -4)
                newPosition.x = -4; 
            if (newPosition.y > 2)
                newPosition.y = 2; 
            if (newPosition.y > -2)
                newPosition.y = -2;
            transform.position = newPosition / 2;
        }
	}

    public void Toggle()
    {
        on = !on;
    }

    public void Toggle(bool state)
    {
        on = state;
    }
}
