using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontFade : MonoBehaviour {

    Color color;

	// Use this for initialization
	void Start ()
    {
        color = Color.white;
	}
	
	// Update is called once per frame
	void Update ()
    {
        color.a = Mathf.Abs(Mathf.Sin(Time.time));
        GetComponent<Text>().color = color;
	}
}
