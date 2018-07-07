using UnityEngine;

public class MouseMover : MonoBehaviour
{
    CharacterController characterController;

    Vector3 velocity;

    [SerializeField]
    float walkSpeed;

    Animator animator;

    // Use this for initialization
    void Start ()
    {
		
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

        if (characterController.isGrounded)
        {
            velocity = new Vector3(h2, 0.0f, v2);

            if (velocity.magnitude > 0.1f)
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
