using UnityEngine;
using UnityEngine.UI;

public class PlayerEntry : MonoBehaviour
{
    [SerializeField]
    Text text = null;

    [SerializeField]
    bool isPlayerOne = true;

    public bool canPlayGame = false;

	// Use this for initialization
	void Start ()
    {
        canPlayGame = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isPlayerOne)
        {
            //Left
            if(Input.GetKeyDown(KeyCode.Joystick2Button0)
                || Input.GetKeyDown(KeyCode.Joystick2Button1)
                || Input.GetKeyDown(KeyCode.Joystick2Button2)
                || Input.GetKeyDown(KeyCode.Joystick2Button3))
            {
                canPlayGame = false;
            }
        }
        else
        {
            //Right
            if (Input.GetKeyDown(KeyCode.Joystick1Button0)
                || Input.GetKeyDown(KeyCode.Joystick1Button1)
                || Input.GetKeyDown(KeyCode.Joystick1Button2)
                || Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                canPlayGame = false;
            }
        }

        if(canPlayGame)
        {
            this.text.text = "準備OK!!!!";
        }
        else
        {
            this.text.text = "ボタンをおしてね";
        }
	}
}
