using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AdvancedCamera : MonoBehaviour {

    public float playerWidth;
    public float margin;

    private Vector3 newPosition;
    private bool on = true;
    private List<GameObject> playerList;

	// Use this for initialization
	void Start () {
        playerList = new List<GameObject>();
        newPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if (on)
        {
            newPosition = Vector3.zero;

            foreach (GameObject p in Global.players)
            {
                playerList.Add(p);
            }

            playerList.OrderBy(p => p.transform.position);
            Vector3 highestValue = playerList[0].transform.position;
            Vector3 lowestValue = playerList[Global.players.Count - 1].transform.position;

            newPosition += highestValue + lowestValue;
            newPosition.z = -20; 

            //if (newPosition.x > 0)
            //    newPosition.x += playerWidth;
            //else if (newPosition.x < 0)
            //    newPosition.x -= playerWidth;

            if (newPosition.x > margin)
                newPosition.x = margin;
            if (newPosition.x < -margin)
                newPosition.x = -margin; 
            if (newPosition.y > margin/2)
                newPosition.y = margin/2; 
            if (newPosition.y < -(margin/2))
                newPosition.y = -(margin/2);

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
