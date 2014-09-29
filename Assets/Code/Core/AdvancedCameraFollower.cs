using UnityEngine;
using System.Collections;

public class AdvancedCameraFollower : MonoBehaviour {

    public GameObject cameraLead;
    public float moveHardness = 1.5f;
    public float scaleHardness = .5f;

    private bool on = true;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(on)
        {
            Vector3 pp = Vector3.MoveTowards(transform.position, cameraLead.transform.position, moveHardness * Time.deltaTime);
            pp.z = -10;
            transform.position = pp;
            //cameraScript.orthographicSize = 5f + scaleHardness * Mathf.Abs(Vector3.Distance(transform.position, cameraLead.transform.position));
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
