using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    string nextSceneName;

    [SerializeField]
    float fadeSpeed;

    Camera mainCamera;

    float counter;

    bool isChangingScene = false;

    bool isOpendScene = false;

    float firstTime;

    // Use this for initialization
    void Start()
    {
        counter = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //-の↓ボタン
        if (Input.GetKeyDown(KeyCode.Joystick1Button1)
            || Input.GetKeyDown(KeyCode.Joystick2Button1)
            || Input.GetKeyDown(KeyCode.Space))
        {
            firstTime = Time.time;
            isChangingScene = true;
        }

        if (isChangingScene)
        {
            SceneManager.LoadScene(nextSceneName);
            counter = (Time.time - firstTime) * fadeSpeed;
        }
    }
}
