using UnityEngine;
using System.Collections;

public class ScreenShakeSimple : MonoBehaviour
{
    private Camera mainCamera;

    Vector3 originalCameraPosition;

    float shakeAmt = 0;
    Vector2 shakeDirection = new Vector2(0, 0);

    void Start()
    {
        mainCamera = transform.GetComponent<Camera>();
        originalCameraPosition = mainCamera.transform.position;
    }

    public void StartShaking(Vector2 direction, float amt)
    {
        shakeAmt = amt;
        shakeDirection = direction;
        InvokeRepeating("CameraShake", 0, .05f);
        InvokeRepeating("StopShaking", 1, .01f);

    }

    void CameraShake()
    {
        float quakeAmtX = shakeDirection.x * (Mathf.Abs(Random.value * shakeAmt * 2 - shakeAmt));
        float quakeAmtY = shakeDirection.y * (Mathf.Abs(Random.value * shakeAmt * 3 - shakeAmt));

        Vector3 pp = mainCamera.transform.position;
        pp = Vector3.Lerp(transform.position, new Vector3(quakeAmtX, quakeAmtY, -10), 0.01f);
        mainCamera.transform.position = pp;

    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        float d = Vector3.Distance(transform.position, originalCameraPosition);

        if (Mathf.Abs(d) > .001)
            transform.position = Vector3.MoveTowards(transform.position, originalCameraPosition, .01f);
        else
        {
            transform.position = originalCameraPosition;
            CancelInvoke("StopShaking");
        }

        print(d);
    }

}