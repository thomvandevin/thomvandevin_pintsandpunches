using UnityEngine;
using System.Collections;

public class Player_parser : MonoBehaviour {

    public int controllerNumber;
    public Player.Character character;
    public bool mpu = false;
    public string com;

    private Player p;

    void Awake()
    {
        Global.EarlyStart();
        Global.StartMainGameWOPlayers();
    }

	// Use this for initialization
	void Start () 
    {
        p = gameObject.AddComponent<Player>();
        p.SetPlayer(controllerNumber, character, mpu, com);

        p.gameObject.layer = 7 + controllerNumber;
        p.gameObject.name = "Player " + controllerNumber.ToString();
        Global.players.Add(p.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
