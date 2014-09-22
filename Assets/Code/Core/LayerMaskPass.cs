using UnityEngine;
using System.Collections;

public class LayerMaskPass : MonoBehaviour {

    public LayerMask layermask;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public LayerMask GetLayerMask()
    {
        return layermask;
    }
}
