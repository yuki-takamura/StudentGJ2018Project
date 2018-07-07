using UnityEngine;

public class CharacterMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Joy-Con(R)
        var h1 = Input.GetAxis("Horizontal 1");
        var v1 = Input.GetAxis("Vertical 1");

        // Joy-Con(L)
        var h2 = Input.GetAxis("Horizontal 2");
        var v2 = Input.GetAxis("Vertical 2");

        transform.position += new Vector3(h1, 0, v1);
	}
}
