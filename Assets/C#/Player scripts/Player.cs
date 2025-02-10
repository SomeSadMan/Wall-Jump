
using System;
using UnityEngine;
using static States;



public class Player : MonoBehaviour
{
    private IMovable movement;
    private IState state;
    
    public void Initialize(IMovable movable, IState state)
    {
        movement = movable;
        this.state = state;
    }
    
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask wallLayer;
    
    public  Rigidbody2D rb { get; set; }
    public BoxCollider2D coll { get; set; }
    
    [Header("Jump Settings")]
    [SerializeField] private float yVelocity;
    [SerializeField] private float xVelocity;

    //=========================FLAGS========================\\
    private bool jump;
    private bool doubleJump;
    private bool canDoubleJump;
    private bool wallJump;
    private bool reversedWallJump;
    internal bool IsSliding;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        InputsChecking();
        state.UpdateState(this);
        
    }

    private void FixedUpdate()
    {
        FlagChecking();
        
    }
    
    private void Flip(float direction) // пока можно оставить это , но потом надо будет переделать 
    {
       Vector3 scale = transform.localScale;
        scale.x = direction;
        transform.localScale = scale;

    }
    
    private void InputsChecking()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jump = true;
        }

        if (Input.GetButtonDown("Jump") && canDoubleJump && !IsGrounded() && !IsSliding)
        {
            doubleJump = true;
        }
        if (Input.GetButtonDown("Jump") && IsSliding && !IsGrounded())
        {
            wallJump = true;
        }
        
    }
    
    private void FlagChecking()
    {
        if (jump)
        {
            Flip(1);
            movement.Jump(xVelocity,yVelocity);
            jump = false;
            canDoubleJump = true;
        }

        if (doubleJump)
        {
            Flip(-1);
            movement.Jump(xVelocity,yVelocity);
            doubleJump = false;
            canDoubleJump = false;
        }
    
        if (wallJump)
        {
            movement.WallJump(xVelocity,yVelocity);
            wallJump = false;
            IsSliding = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f,
            Vector2.down, .1f, jumpableGround);
    }
    
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Wall"))
        {
            rb.drag = 50;
            IsSliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Wall"))
        {
            rb.drag = 0;
            IsSliding = false;
        }
    }

}
