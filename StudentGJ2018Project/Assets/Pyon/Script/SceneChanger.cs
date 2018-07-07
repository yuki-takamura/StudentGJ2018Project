using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //-の↓ボタン
        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            Debug.Log("osareta");
            SceneManager.LoadScene("Main");
        }
	}
}
