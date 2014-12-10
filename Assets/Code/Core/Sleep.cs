using UnityEngine;
using System.Collections;

public class Sleep : MonoBehaviour {

    public static void SleepOn()
    {
        Time.timeScale = 0;
    }

    public static void SleepOff()
    {
        Time.timeScale = 1;
    }

    public void SleepTimer(float sleepTime)
    {
        SleepOn();
        Invoke("SleepOff", sleepTime);
    }
}
