using UnityEngine;
using System.Collections;

public class SetText : MonoBehaviour {

    public string text = "";

    private TextMesh textm;

	// Use this for initialization
	void Start () 
    {
        textm = gameObject.GetComponent<TextMesh>();
        text = text.Replace("//", "\n");
	}
	
	// Update is called once per frame
	void Update () 
    {
        textm.text = text;
	}
}
