using UnityEngine;

public class MouseMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Joy-Con(L)
        var h2 = Input.GetAxis("Horizontal 2");
        var v2 = Input.GetAxis("Vertical 2");

        transform.position += new Vector3(h2, 0, v2);

        //TODO デバッグ用　あとで消す
        if (Input.GetKey(KeyCode.Joystick2Button11))
        {
            transform.position = Vector3.zero;
        }
    }
}
