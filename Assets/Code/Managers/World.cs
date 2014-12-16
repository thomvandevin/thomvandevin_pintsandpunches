using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    [HideInInspector]

    public Vector2 drinkPos;

    private int drinkTimer;

	// Use this for initialization
    void Start()
    {

        int SCP = 0;
        for (int i = 1; i <= Global.NumberOfPlayers; i++)
        {
            Player.Character sc;
            if (Global.playerCharacter[SCP] == 5)
                sc = Player.Character.LEPRECHAUN_01;
            else if (Global.playerCharacter[SCP] == 1)
                sc = Player.Character.LEPRECHAUN_USA;
            else if (Global.playerCharacter[SCP] == 2)
                sc = Player.Character.CLUIRICHAUN;
            else if (Global.playerCharacter[SCP] == 3)
                sc = Player.Character.FAR_DARRIG;
            else if (Global.playerCharacter[SCP] == 4)
                sc = Player.Character.FAIRY;
            else sc = Player.Character.LEPRECHAUN_01;

            GameObject playerObject = Instantiate(Resources.Load("Prefabs/Entities/Player")) as GameObject;
            Player playerScript = playerObject.AddComponent<Player>();
            playerScript.SetPlayer(i, sc, Global.useMpus[i-1], Global.comMpus[i-1]);
            playerObject.layer = 7 + i;
            playerObject.name = "Player " + i.ToString();
            Global.players.Add(playerObject);

            SCP++;
        }

        drinkTimer = Random.Range(200, 300);
        GameObject.FindGameObjectWithTag("CameraSmoother").GetComponent<AdvancedCameraFollower>().LateStart();
        GameObject.FindGameObjectWithTag("CameraSmoother").GetComponent<AdvancedCameraFollower>().Toggle(true);
    }

	// Update is called once per frame
	void Update () 
    {
        if (Application.loadedLevel == Global.Screen_MainGame)
        {
            foreach (GameObject p in Global.players)
            {
                p.GetComponent<Player>().Update();
            }

            if (Global.enableDrinks)
            {
                foreach (GameObject d in Global.drinks)
                {
                    d.GetComponent<Drink>().Update();
                }

                if (Global.drinks.Count < 4)
                {
                    drinkTimer--;
                    if (drinkTimer <= 0)
                        SpawnDrink();
                }
            }
        }

	}

    public void GameWon(int controllerNumber, Player.Character chosenCharacter)
    {
        Global.playerWhoWon = controllerNumber;
        Global.characterThatWon = chosenCharacter;
        Camera.main.GetComponent<AdvancedCamera>().Zoom(Vector2.zero);
        Application.LoadLevel(Global.Screen_WinScreen);

        print("Player " + controllerNumber.ToString() + " won!");
    }

    private void LoadWinScreen()
    {
        Application.LoadLevel(Global.Screen_WinScreen);
    }

    public void SpawnDrink()
    {
        //drinkTileRand = Random.Range(0,4);
        drinkPos = new Vector2(Random.Range(-5, 5), 0);

        GameObject drink = Instantiate(Resources.Load("Prefabs/Objects/Drinks/Pickup_Drink"), drinkPos, Quaternion.identity) as GameObject;
        Global.drinks.Add(drink);
        
        Vector2 randomForce = new Vector2(Random.Range(-200, 200), Random.Range(100, 200));
        drink.rigidbody2D.AddForce(randomForce);
        
        drinkTimer = Random.Range(400, 500);
    }
}
