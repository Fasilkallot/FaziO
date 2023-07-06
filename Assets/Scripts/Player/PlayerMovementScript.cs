using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    PlayerControls controls;
    Rigidbody2D rb;
    Animator animator;
    MovementState state = MovementState.idle;

    [SerializeField] float speed = 400;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] AudioSource jumpAudio; 

    float direction = 0;
    bool isGrounded;
    float numberOfJumps = 0;

    public bool isFacingRight = true;


    private void Awake()
    {
        animator = GetComponent<Animator>();
       controls = new PlayerControls();
        controls.Enable();
        controls.Land.Jump.performed +=  Jump;
        gameObject.transform.position = GameManager.lastCheckPoint;
    }

    private void OnDestroy()
    {
        controls.Land.Jump.performed -=  Jump;
    }



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.Instance.player = this;
    }
    private void FixedUpdate()
    {
        Movement();
        UpdateAnimationState();
    }
    void Movement()
    {
        direction = controls.Land.Move.ReadValue<float>();
        rb.velocity = new Vector2(direction *  speed * Time.fixedDeltaTime,rb.velocity.y);


        if(isFacingRight && direction < 0 || !isFacingRight && direction > 0 )
        {
            Flip();
        }


    }
    void UpdateAnimationState()
    {
        if (Math.Abs(direction) > 0.1) state = MovementState.running;
        else state = MovementState.idle;

        if(rb.velocity.y > 0.1f) state = MovementState.jumping;
        if (rb.velocity.y < -0.1f) state = MovementState.falling;

        animator.SetInteger("state",(int)state);


    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
    private void Jump(InputAction.CallbackContext context)
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position , 0.1f, ground);
        if (isGrounded)
        {
            numberOfJumps = 0;
            rb.velocity = new Vector2(rb.velocity.x,jumpForce );
            numberOfJumps++;
            jumpAudio.Play();
        }
        else if(numberOfJumps == 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            numberOfJumps = 0;
            jumpAudio.Play();
        }
    }

}
public enum MovementState { idle,running,jumping,falling}
