using UnityEngine;
using System.Collections;

public class AdvancedCameraFollower : MonoBehaviour {

    public GameObject cameraLead;
    public float moveHardness = 1.5f;
    public float scaleHardness = .5f;

    private bool on = true;
    private Camera cameraScript;

	// Use this for initialization
	void Start ()
    {
        cameraScript = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(on)
        {
            transform.position = Vector3.MoveTowards(transform.position, cameraLead.transform.position, moveHardness * Time.deltaTime);
            cameraScript.orthographicSize = 3.5f + scaleHardness * Mathf.Abs(Vector3.Distance(transform.position, cameraLead.transform.position));
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
