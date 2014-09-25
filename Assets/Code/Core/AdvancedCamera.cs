using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AdvancedCamera : MonoBehaviour {

    public float playerWidth;

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
            print(highestValue);
            Vector3 lowestValue = playerList[Global.players.Count - 1].transform.position;
            print(lowestValue);

            newPosition += highestValue + lowestValue;
            newPosition.z = -20;
            if (newPosition.x > 4)
                newPosition.x = 4;
            if (newPosition.x < -4)
                newPosition.x = -4; 
            if (newPosition.y > 2)
                newPosition.y = 2; 
            if (newPosition.y < -2)
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
