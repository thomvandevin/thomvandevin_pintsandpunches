using UnityEngine;
using System.Collections;

public class ScreenShakeGeneric : MonoBehaviour {

    private float shakeAmt;
    private Camera mainCamera;
    
	// Use this for initialization
	void Start () {
        mainCamera = transform.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartShaking(float amt)
    {
        shakeAmt = amt;
        mainCamera.GetComponent<AdvancedCamera>().Toggle(false);
        InvokeRepeating("CameraShake", 0, .2f);
        Invoke("StopShaking", 2f);

    }

    void CameraShake()
    {
        float randomAngle = Random.Range(-1, 1);
        Quaternion pp = transform.rotation;
        pp.z += randomAngle * shakeAmt;
        transform.rotation = pp;

    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.localRotation = Quaternion.identity;
    }
}
