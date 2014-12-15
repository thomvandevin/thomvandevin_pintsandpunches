using UnityEngine;
using System.Collections;

public class Sinusoide : MonoBehaviour {

    public float A = 0f;
    public float B = .1f;
    public float C = 10f;
    public float D = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 tempPos = gameObject.transform.position;
        float sin = (float)(A + B * Mathf.Sin(((2 * Mathf.PI) / Time.time)*C + D));
        tempPos.y += sin;
        gameObject.transform.position = tempPos;
        print(sin);
	}
}
