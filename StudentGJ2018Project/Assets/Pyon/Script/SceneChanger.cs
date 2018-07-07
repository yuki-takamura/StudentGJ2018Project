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
        mainCamera = Camera.main;
        mainCamera.GetComponent<SC_CustomPostEffectBehaviour>().postEffectMaterial.SetFloat("_Threshold", 0);
        counter = 0;

        firstTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //-の↓ボタン
        if (Input.GetKeyDown(KeyCode.Joystick1Button1)
            || Input.GetKeyDown(KeyCode.Joystick2Button1)
            || Input.GetKeyDown(KeyCode.Z))
        {
            isChangingScene = true;
        }

        if (isChangingScene)
        {
            counter = (Time.time - firstTime) * fadeSpeed;
            mainCamera.GetComponent<SC_CustomPostEffectBehaviour>().postEffectMaterial.SetFloat("_Threshold", counter);
        }

        if (mainCamera.GetComponent<SC_CustomPostEffectBehaviour>().postEffectMaterial.GetFloat("_Threshold") >= 1)
            SceneManager.LoadScene(nextSceneName);
    }
}
