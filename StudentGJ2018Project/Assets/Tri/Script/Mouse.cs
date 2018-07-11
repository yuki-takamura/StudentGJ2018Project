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
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Joy-Con(L)        
        float h2;
        float v2;
        Vector2 moveAmount = Vector2.zero;

        if (GameSystem.Instance.NowSecondRound == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                moveAmount.x = 1;

            if (Input.GetKey(KeyCode.RightArrow))
                moveAmount.x = -1;

            if (Input.GetKey(KeyCode.UpArrow))
                moveAmount.y = 1;

            if (Input.GetKey(KeyCode.DownArrow))
                moveAmount.y = -1;

            h2 = -moveAmount.x;
            v2 = moveAmount.y;

        }
        else
        {
            if (Input.GetKey(KeyCode.D))
                moveAmount.x = -1;

            if (Input.GetKey(KeyCode.A))
                moveAmount.x = 1;

            if (Input.GetKey(KeyCode.S))
                moveAmount.y = -1;

            if (Input.GetKey(KeyCode.W))
                moveAmount.y = 1;

            h2 = -moveAmount.x;
            v2 = moveAmount.y;
        }

        transform.position += new Vector3(h2 * walkSpeed, 0, v2 * walkSpeed);


        velocity = new Vector3(h2, 0.0f,v2 );

        if (velocity.magnitude > 0.1f)
        {
            animator.SetFloat("Speed", velocity.magnitude);
            transform.LookAt(transform.position + velocity);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }


        return;

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


