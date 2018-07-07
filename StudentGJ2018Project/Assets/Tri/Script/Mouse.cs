using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    CharacterController characterController;

    Vector3 velocity;

    [SerializeField]
    float walkSpeed;

    Animator animator;


    string onePlayertV = "Vertical 1";
    string onePlayertH = "Horizontal 1";

    string twoPlayertV = "Vertical 2";
    string twoPlayertH = "Horizontal 2";

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Joy-Con(L)        
        float h2;
        float v2;
        if (GameSystem.Instance.NowSecondRound == false)
        {
            h2 = -Input.GetAxis(twoPlayertV);
            v2 = Input.GetAxis(twoPlayertH);

        }
        else
        {
            h2 = Input.GetAxis(onePlayertV);
            v2 = -Input.GetAxis(onePlayertH);
        }

        transform.position += new Vector3(h2 * 0.1f, 0, v2 * 0.1f);

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


