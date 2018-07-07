using UnityEngine;

public class CharacterMover : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Joy-Con(R)
        var h1 = Input.GetAxis("Horizontal 1");
        var v1 = Input.GetAxis("Vertical 1");

        transform.position += new Vector3(h1, 0, v1);

        //TODO デバッグ用　あとで消す
        if (Input.GetKey(KeyCode.Joystick1Button10))
        {
            transform.position = Vector3.zero;
        }
	}
}
