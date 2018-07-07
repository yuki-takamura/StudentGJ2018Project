using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    CharacterController characterController;

    Vector3 velocity;

    [SerializeField]
    float walkSpeed;

    Animator animator;

    void Start () {
        characterController = GetComponent<CharacterController>();
	}

    void Update()
    {
        // Joy-Con(L)
        var h2 =-Input.GetAxis("Vertical 2");
        var v2 = Input.GetAxis("Horizontal 2");

        transform.position += new Vector3(h2*0.1f, 0, v2 * 0.1f);

        return;
        //TODO デバッグ用　あとで消す
        if (Input.GetKey(KeyCode.Joystick2Button11))
        {
            transform.position = Vector3.zero;
        }

        if (characterController.isGrounded)
        {
            velocity = new Vector3(h2, 0.0f, v2);

            if (velocity.magnitude > 0.1f)
            {
                transform.LookAt(transform.position + velocity);
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * walkSpeed * Time.deltaTime);
    }
}


