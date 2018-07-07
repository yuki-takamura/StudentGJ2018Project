using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    CharacterController characterController;

    Vector3 velocity;

    [SerializeField]
    float walkSpeed;

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

        transform.position += new Vector3(h1, 0, v1);

        //TODO デバッグ用　あとで消す
        if (Input.GetKey(KeyCode.Joystick1Button10))
        {
            transform.position = Vector3.zero;
        }

        if(characterController.isGrounded)
        {
            velocity = new Vector3(h1, 0.0f, v1);

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
