using UnityEngine;
using System.Collections;

public class RespawnButton : MonoBehaviour {

    public float hardness = 15f;

    private Transform playerTransform;
    private Vector3 target;

	// Use this for initialization
	void Start () 
    {
	
	}

    public void SetRespawnButton(GameObject playerObject)
    {
        playerTransform = playerObject.transform;
        target = new Vector3(playerTransform.position.x, playerTransform.position.y + .3f, playerTransform.position.z);
        
    }

    public void RemoveRespawnButton()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () 
    {
        target = new Vector3(playerTransform.position.x, playerTransform.position.y + .3f, playerTransform.position.z);
        gameObject.transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * hardness);
	}
}
