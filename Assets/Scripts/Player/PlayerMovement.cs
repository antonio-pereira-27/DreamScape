using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // References
    private CharacterController characterController;
    private Animator animator;

    // Variables
    [SerializeField]private float speed;
    private float runSpeed = 2.7f;
    private float walkSpeed = 1.5f;
    private float jumpSpeed = 0.4f;
    private float acelaration = 0.5f;
    private float gravity = -9.8f;

    private bool sprint = false;

    
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public bool isGrounded;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       Move();
    }

    private void Move(){
        // update the variable is grounded
        isGrounded = characterController.isGrounded;

        // if grounded stop applying gravity
        if(isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // get the input by user
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // calculate the direction with the inputs
        moveDirection = transform.right * x + transform.forward * z;

        if (isGrounded)
        {
            if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
                Run();
            else if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
                Walk();
            else if(moveDirection == Vector3.zero)
                Idle();

            if(Input.GetKey(KeyCode.Space))
                Jump();
        }

        moveDirection *= speed;

        characterController.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        animator.SetFloat("Velocity", speed);
        animator.SetInteger("x", (int)x);
        animator.SetBool("Sprint", sprint);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        animator.SetTrigger("Jump");
    }

    private void Idle()
    {
        
        speed = 0f;
    }

    private void Walk()
    {
        acelaration = 1.2f;
        if (speed < walkSpeed)
            speed += acelaration * Time.deltaTime;
        sprint = false;
        
    }

    private void Run()
    {
        //animator.SetTrigger("Sprint");
        sprint = true;
        acelaration = 3f;
        if (speed < runSpeed)
            speed += acelaration * Time.deltaTime;
        
    }
}
