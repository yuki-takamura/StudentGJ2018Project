using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActiveSceneManager : MonoBehaviour
{
    [SerializeField]
    Text text1 = null;

    [SerializeField]
    Text text2 = null;
    
    [SerializeField]
    GameObject child;

    [SerializeField]
    bool isPlayerOne = true;

    public bool[] canPlayGame = new bool[2];

    [SerializeField]
    string nextSceneName;

    bool isOk = false;

    void Start()
    {
        canPlayGame[0] = false;
        canPlayGame[1] = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Joystick2Button0)
                || Input.GetKeyDown(KeyCode.Joystick2Button1)
                || Input.GetKeyDown(KeyCode.Joystick2Button2)
                || Input.GetKeyDown(KeyCode.Joystick2Button3))
        {
            canPlayGame[0] = true;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0)
                || Input.GetKeyDown(KeyCode.Joystick1Button1)
                || Input.GetKeyDown(KeyCode.Joystick1Button2)
                || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            canPlayGame[1] = true;
        }

        if(canPlayGame[0])
        {
            text1.text = "準備OK!!!";
        }
        else
        {
            text1.text = "ボタンをおしてね";
        }

        if (canPlayGame[1])
        {
            text2.text = "準備OK!!!";
        }
        else
        {
            text2.text = "ボタンをおしてね";
        }

        if(isOk)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0)
                || Input.GetKeyDown(KeyCode.Joystick2Button1)
                || Input.GetKeyDown(KeyCode.Joystick2Button2)
                || Input.GetKeyDown(KeyCode.Joystick2Button3)
                || Input.GetKeyDown(KeyCode.Joystick1Button0)
                || Input.GetKeyDown(KeyCode.Joystick1Button1)
                || Input.GetKeyDown(KeyCode.Joystick1Button2)
                || Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }

        if(canPlayGame[0] && canPlayGame[1])
        {
            isOk = true;
        }
    }
}
