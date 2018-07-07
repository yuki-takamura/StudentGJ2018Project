using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontFade : MonoBehaviour
{
    bool isChangingScene;
    Color color;

	// Use this for initialization
	void Start ()
    {
        color = Color.white;
        isChangingScene = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1)
            || Input.GetKeyDown(KeyCode.Joystick2Button1)
            || Input.GetKeyDown(KeyCode.Z))
        {
            isChangingScene = true;
        }

        if (isChangingScene)
        {
            color.a = 0;
            GetComponent<Text>().color = color;
        }
        else
        {
            color.a = Mathf.Abs(Mathf.Sin(Time.time));
            GetComponent<Text>().color = color;
        }
	}
}
