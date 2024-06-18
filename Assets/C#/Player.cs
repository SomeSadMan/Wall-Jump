
using System.Collections;
using UnityEngine;

/// <summary>
/// Ctrl+K+C - закомментировать несколько строк , Ctrl+K+U раскомментировать несколько строк
/// </summary>

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    
    private Vector2 playerPosition;
    static public int playerLives = 3;


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

    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isJumping;
    private bool doubleJump;
    private bool doubleWallJump;
    private bool canNormalJump;
    

    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    
    void Start()
    {
        
        anim = GetComponent<Animator>();
        playerPosition = transform.position;
        canNormalJump = true;
        vecGravity = new Vector2(0, Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    
    void Update()
    {
        
        ClimbingAndJumpingFromWall();
        WallJump();
        Jump();
    }

    private void JumpTimeController()
    {
        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;
            rb.velocity += vecGravity * jumpPowerMultiplier * Time.deltaTime;

        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    private void Jump()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump") && canNormalJump)
        {

            rb.velocity = new Vector2(jumpSpeed, jumpPower);
            isJumping = true;
            jumpCounter = 0;
            doubleJump = true;
            Debug.Log("Jump");


        }

        if (doubleJump && !IsGrounded())
        {
            ReverseJump();
        }

        JumpTimeController();

    }

   

    private void  ReverseJump()
    {

        if (Input.GetButtonDown("Jump"))
       {
            rb.velocity = new Vector2(-jumpSpeed, jumpPower);
            doubleJump = false;
            isJumping = true;
            jumpCounter = 0;
            Debug.Log("ReverseJump");



        }

        JumpTimeController();

    }

    private void WallJump()
    {

        if (Input.GetButtonDown("Jump") && isWallSliding)
        {

            rb.velocity = new Vector2(wallJumpPower * -transform.localScale.x * wallJumpSpeed, wallJumpPower);
            isJumping = true;
            jumpCounter = 0;
            doubleWallJump = true;
            Debug.Log("WallJump");
            


        }

        if(!isWallSliding && doubleWallJump)
        {
            ReverseWallJump();
        }

        JumpTimeController();
    }

    private void ReverseWallJump()
    {

        if( Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(wallJumpPower * -transform.localScale.x * wallJumpSpeed, wallJumpPower);
            isJumping = true;
            jumpCounter = 0;
            doubleWallJump = false;
            Debug.Log("ReverseWallJump");
            
        }

        JumpTimeController();


    }

    private void ClimbingAndJumpingFromWall()
    {
        
        isTouchingWall = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right * transform.localScale.x, wallCheckDistance, wallLayer);
        
        
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
            anim.SetBool("climbing", true);

        }
        else
        {
            isWallSliding = false;
            anim.SetBool("climbing", false);

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
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
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
    

    public IEnumerator Respown(float duration)
    {
        anim.SetBool("death", true);
        rb.simulated = false;
        yield return new WaitForSeconds(duration);
        CameraFollowY.playerIsAlive = false;
        transform.position = playerPosition;
        playerLives--;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        Debug.Log(playerLives);
        anim.SetBool("death", false);
        rb.simulated = true;
        
    }

    public void PlayerDeath()
    {
        if(playerLives == 0)
        {
            //animation
            playerPrefab.SetActive(false);
        }  
    }

}
