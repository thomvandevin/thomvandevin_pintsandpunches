using UnityEngine;
using System.Collections;

public class AdvancedCamera : MonoBehaviour
{

    public GameObject cameraLead;
    //public float moveHardness = 1.5f;
    //public float scaleHardness = .5f;

    public bool asleep = false;

    public float xMargin = 1f;
    public float yMargin = 1f;
    public float xSmooth = 8f;
    public float ySmooth = 8f;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;

    public float zoomHardness = 1.0f;
    public int holdTime = 33;
    private int zoomTimer = 0;
    private bool on = true;
    private bool zoomIn = false, zoomOut = false, zoomHold = false;
    private Vector2 currentPosition, previousPosition, newPosition;
    private float currentScale, previousScale, newScale;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (on)
        {
            //Vector3 pp = Vector3.MoveTowards(transform.position, cameraLead.transform.position, moveHardness * Time.deltaTime);
            //pp.z = -10;
            //transform.position = pp;
            //cameraScript.orthographicSize = 5f + scaleHardness * Mathf.Abs(Vector3.Distance(transform.position, cameraLead.transform.position));

            TrackPlayerMiddle();
        }

    }

    void Update()
    {
        if (zoomIn || zoomOut || zoomHold)
        {
            if (zoomIn)
            {
                if(zoomHold)
                    ZoomInOnly();
                else
                    ZoomIn();
            }
            else if (zoomOut)
                ZoomOut();

            transform.position = new Vector3(currentPosition.x, currentPosition.y, transform.position.z);
            Camera.main.orthographicSize = currentScale;
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

    public void ScreenShake(float multiplier)
    {
        gameObject.ShakePosition(new Vector3(1, 1, 0) * multiplier, .35f, 0);
    }

    public void ZoomWithDelay(Vector2 target, float delay)
    {
        Invoke("Zoom", delay);
        newPosition = target;
    }
    
    public void Zoom()
    {
        currentPosition = transform.position;
        currentScale = Camera.main.orthographicSize;

        previousPosition = currentPosition;
        previousScale = currentScale;

        //newPosition = currentPosition;
        newScale = 3;
        zoomTimer = 0;

        Toggle(false);
        zoomIn = true;
        //zoomHold = true;

        SleepOn();
    }

    public void Zoom(Vector2 target)
    {
        currentPosition = transform.position;
        currentScale = Camera.main.orthographicSize;

        previousPosition = currentPosition;
        previousScale = currentScale;

        newPosition = target;
        newScale = 3;
        zoomTimer = 0;

        Toggle(false);
        zoomIn = true;

        SleepOn();

    }
    public void ZoomOnly()
    {
        currentPosition = transform.position;
        currentScale = Camera.main.orthographicSize;

        previousPosition = currentPosition;
        previousScale = currentScale;

        newPosition = currentPosition;
        newScale = 3;
        zoomTimer = 0;

        Toggle(false);
        zoomIn = true;
        zoomHold = true;

        SleepOn();
    }

    public void ZoomOnly(Vector2 target)
    {
        currentPosition = transform.position;
        currentScale = Camera.main.orthographicSize;

        previousPosition = currentPosition;
        previousScale = currentScale;

        newPosition = target;
        newScale = 3;
        zoomTimer = 0;

        Toggle(false);
        zoomIn = true;
        zoomHold = true;

        SleepOn();

    }


    private void ZoomIn()
    {
        float d1 = Vector2.Distance(currentPosition, newPosition);
        float d2 = currentScale - newScale;
        if (Mathf.Abs(d1) > .01f || Mathf.Abs(d2) > 0.01f || zoomTimer < holdTime)
        {
            zoomTimer++;
            float t = 1 - (1f/zoomTimer)*zoomHardness;
            currentPosition = Vector2.Lerp(currentPosition, newPosition, t);
            currentScale = Mathf.Lerp(currentScale, newScale, t);
        }
        else
        {
            currentPosition = newPosition;
            currentScale = newScale;
            zoomIn = false;
            zoomOut = true;
            zoomTimer = 0;
        }


        //print(currentPosition + " : " + previousPosition + " : " + newPosition);
    }

    private void ZoomInOnly()
    {
        float d1 = Vector2.Distance(currentPosition, newPosition);
        float d2 = currentScale - newScale;
        if (Mathf.Abs(d1) > .01f || Mathf.Abs(d2) > 0.01f)
        {
            zoomTimer++;
            float t = 1 - (1f / zoomTimer) * zoomHardness;
            currentPosition = Vector2.Lerp(currentPosition, newPosition, t);
            currentScale = Mathf.Lerp(currentScale, newScale, t);
        }
        else if(zoomIn)
        {
            currentPosition = newPosition;
            currentScale = newScale;
            zoomIn = false;
            zoomTimer = 0;
        }
    }

    private void ZoomOut()
    {
        float d1 = Vector2.Distance(currentPosition, previousPosition);
        float d2 = currentScale - previousScale;
        if (Mathf.Abs(d1) > .01f || Mathf.Abs(d2) > 0.01f)
        {
            zoomTimer++;
            float t = 1 - (1f / zoomTimer) * zoomHardness;
            currentPosition = Vector2.Lerp(currentPosition, previousPosition, t);
            currentScale = Mathf.Lerp(currentScale, previousScale, t);
        }
        else
        {
            currentPosition = previousPosition;
            currentScale = previousScale;
            zoomOut = false;
            SleepOff();
            Toggle(true);
        }

    }

    public void ZoomOutOnly()
    {
        zoomOut = true;
    }

    public void SleepOn()
    {
        Sleep.SleepOn();
        asleep = true;
    }

    public void SleepOff()
    {
        Sleep.SleepOff();
        asleep = false;
    }
}

