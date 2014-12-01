using UnityEngine;
using System.Collections;

public class SelectionCountdown : MonoBehaviour
{

    public int maxWaitingTime = 2000;

    public int timer;
    private MenuToGame menuScript;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        timer = 1;
        menuScript = GameObject.FindGameObjectWithTag("Global").GetComponent<MenuToGame>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < maxWaitingTime)
        {
            timer++;
            float tfloat = ((float)timer)/maxWaitingTime;
            anim.SetFloat("timer", tfloat);
        }
        else if (timer >= maxWaitingTime)
            menuScript.LoadMainGame();


    }
}
