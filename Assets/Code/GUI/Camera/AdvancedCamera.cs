using UnityEngine;
using System.Collections;

public class AdvancedCamera : MonoBehaviour {

    public GameObject cameraLead;
    //public float moveHardness = 1.5f;
    //public float scaleHardness = .5f;

    public float xMargin = 1f;
    public float yMargin = 1f;
    public float xSmooth = 8f;
    public float ySmooth = 8f;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;

    private bool on = true;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(on)
        {
            //Vector3 pp = Vector3.MoveTowards(transform.position, cameraLead.transform.position, moveHardness * Time.deltaTime);
            //pp.z = -10;
            //transform.position = pp;
            //cameraScript.orthographicSize = 5f + scaleHardness * Mathf.Abs(Vector3.Distance(transform.position, cameraLead.transform.position));

            TrackPlayerMiddle();
        }

            
	}

    private void TrackPlayerMiddle()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckXMargin())
            targetX = Mathf.Lerp(transform.position.x, cameraLead.transform.position.x, xSmooth * Time.deltaTime);

        if (CheckYMargin())
            targetY = Mathf.Lerp(transform.position.y, cameraLead.transform.position.y, ySmooth * Time.deltaTime);

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }

    private bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - cameraLead.transform.position.x) > xMargin;
    }
    private bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - cameraLead.transform.position.y) > yMargin;
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

