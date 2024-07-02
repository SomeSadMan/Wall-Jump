using System.Collections;
using UnityEngine;



public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Sprite[] sprites;
    
    
    private Vector2 playerPosition;
    public static int PlayerLives = 3;


    [Header("Jump Settings")]
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpPowerMultiplier;

    [Header("WallJump Settings")]
    [SerializeField] private float wallJumpSpeed;
    [SerializeField] private float wallJumpPower;

    [Header("Walls Settings")]
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float wallSlideSpeed;

    [Header("Jump Layers")]
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask wallLayer;

    private float jumpCounter;
    private Vector2 vecGravity;

    private bool reverseWallJumping;
    private bool wallJumping;
    private bool reverseJumping;
    private bool jumping;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isJumping;
    private bool doubleJump;
    private bool doubleWallJump;
    private bool canNormalJump;
    

    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private static readonly int Climbing = Animator.StringToHash("climbing");
    private static readonly int Death = Animator.StringToHash("death");

    void Start()
    {

        
        playerPosition = transform.position;
        canNormalJump = true;
        vecGravity = new Vector2(0, Physics2D.gravity.y);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    
    void Update()
    {
        InputManager();
    }

    void FixedUpdate()
    {
        Jump();
        ReverseJump();
        WallJump();
        ReverseWallJump();
        ClimbingAndJumpingFromWall();
    }

    private void InputManager()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded() && canNormalJump)
        {
            jumping = true;
        }

        if (Input.GetButtonDown("Jump") && doubleJump && !IsGrounded())
        {
            reverseJumping = true;
        }

        if (Input.GetButtonDown("Jump") && isWallSliding)
        {
            wallJumping = true;
        }

        if (Input.GetButtonDown("Jump") && !isWallSliding && doubleWallJump && !IsGrounded())
        {
            reverseWallJumping = true;
        }
    }
    

    private void JumpTimeController()
    {
        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;
            rb.velocity += vecGravity * (jumpPowerMultiplier * Time.deltaTime);

        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    private void Jump()
    {
        if (jumping)
        {
            rb.velocity = new Vector2(jumpSpeed, jumpPower);
            jumping = false;
            isJumping = true;
            jumpCounter = 0;
            doubleJump = true;
            Debug.Log("Jump");
        }
        JumpTimeController();
    }

   

    private void  ReverseJump()
    {

        if (reverseJumping)
        {
            rb.velocity = new Vector2(-jumpSpeed, jumpPower);
            reverseJumping = false;
            doubleJump = false;
            isJumping = true;
            jumpCounter = 0;
            Debug.Log("ReverseJump");
        }
        JumpTimeController();
    }

    
    private void WallJump()
    {

        if ( wallJumping )
        {

            rb.velocity = new Vector2(wallJumpPower * -transform.localScale.x * wallJumpSpeed, wallJumpPower);
            wallJumping = false;
            isJumping = true;
            jumpCounter = 0;
            doubleWallJump = true;
            Debug.Log("WallJump");
        }
        JumpTimeController();
    }

    private void ReverseWallJump()
    {
        if(reverseWallJumping)
        {
            rb.velocity = new Vector2(wallJumpPower * -transform.localScale.x * wallJumpSpeed, wallJumpPower);
            reverseWallJumping = false;
            isJumping = true;
            jumpCounter = 0;
            doubleWallJump = false;
            Debug.Log("ReverseWallJump");
        }
        JumpTimeController();
    }

    private void ClimbingAndJumpingFromWall()
    {
        
        isTouchingWall = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, 
            Vector2.right * transform.localScale.x, wallCheckDistance, wallLayer);
        
        
        
        if(isTouchingWall && !IsGrounded() )
        {
            canNormalJump = false;
        }
        else if ( !isTouchingWall && IsGrounded() )
        {
            canNormalJump = true;
        }
        

        if (isTouchingWall && !isJumping)
        {
            isWallSliding = true;
            anim.SetBool(Climbing, true);

        }
        else
        {
            isWallSliding = false;
            anim.SetBool(Climbing, false);

        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            
        }

        if (rb.velocity.x > 1f) { Flip(1f); }
        if (rb.velocity.x < -1f) { Flip(-1f); }

    }

   

    private void Flip(float direction)
    {
       Vector3 scale = transform.localScale;
        scale.x = direction;
        transform.localScale = scale;

    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f,
            Vector2.down, .1f, jumpableGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            
            StartCoroutine(Respown(1f)); 
        }
        else if (collision.CompareTag("Bound"))
        {
            
            StartCoroutine(Respown(1f));
        } 

    }
    

    private IEnumerator Respown(float duration)
    {
        anim.SetBool(Death, true);
        rb.simulated = false;
        yield return new WaitForSeconds(duration);
        CameraFollowY.playerIsAlive = false;
        transform.position = playerPosition;
        PlayerLives--;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        Debug.Log(PlayerLives);
        anim.SetBool(Death, false);
        rb.simulated = true;
        
    }

    public void PlayerDeath()
    {
        if(PlayerLives == 0)
        {
            //animation
            playerPrefab.SetActive(false);
        }  
    }

}
