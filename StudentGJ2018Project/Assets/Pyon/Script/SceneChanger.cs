using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    [SerializeField]
    string nextSceneName;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //-の↓ボタン
        if (Input.GetKey(KeyCode.Joystick1Button1)
            || Input.GetKey(KeyCode.Joystick2Button1)
            || Input.GetKey(KeyCode.Z))
        {
            Debug.Log("osareta");
            SceneManager.LoadScene(nextSceneName);
        }
	}
}
