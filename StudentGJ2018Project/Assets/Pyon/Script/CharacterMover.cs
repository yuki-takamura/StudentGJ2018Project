using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    CharacterController characterController;

    Vector3 velocity;

    [SerializeField]
    float walkSpeed = 1.0f;

    Animator animator;

	// Use this for initialization
	void Start ()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Joy-Con(R)
        var h1 = Input.GetAxis("Horizontal 1");
        var v1 = Input.GetAxis("Vertical 1");

        velocity = new Vector3(v1, 0.0f, -h1);

        //TODO デバッグ用　あとで消す
        if (Input.GetKey(KeyCode.Joystick1Button10))
        {
            transform.position = Vector3.zero;
        }

        if(characterController.isGrounded)
        {
            if(velocity.magnitude > 0.1f)
            {
                animator.SetFloat("Speed", velocity.magnitude);
                transform.LookAt(transform.position + velocity);
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * walkSpeed * Time.deltaTime);
	}
}
