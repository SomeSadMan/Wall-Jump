
using UnityEngine;
using static States;



public class Player : MonoBehaviour
{
    private IMovable movement;
    private IState state;

    [SerializeField] private GameObject rayPos;
    
    public void Initialize(IMovable movable, IState state)
    {
        movement = movable;
        this.state = state;
    }
    
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private LayerMask jumpableGround;
    
    public  Rigidbody2D rb { get; set; }
    private BoxCollider2D coll;
    
    [Header("Jump Settings")]
    [SerializeField] private float yVelocity;
    [SerializeField] private float xVelocity;

    //=========================FLAGS========================\\
    private bool jump;
    private bool doubleJump;
    private bool canDoubleJump;
    private bool wallJump;
    private bool reversedWallJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        InputsChecking();
        state.UpdateState(this);
        Clinging();
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

        if (Input.GetButtonDown("Jump") && canDoubleJump && !IsGrounded())
        {
            doubleJump = true;
        }
    }
    
    private void FlagChecking()
    {
        if (jump)
        {
            //state = MovementState.PLayerJump;
            Flip(1);
            movement.Jump(xVelocity,yVelocity);
            jump = false;
            canDoubleJump = true;
        }

        if (doubleJump)
        {
            //state = MovementState.PLayerJump;
            Flip(-1);
            movement.DoubleJump(xVelocity,yVelocity);
            doubleJump = false;
            canDoubleJump = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f,
            Vector2.down, .1f, jumpableGround);
    }

    public void Clinging()
    {
        float direction = transform.localScale.x;
        RaycastHit2D hit = Physics2D.Raycast(rayPos.transform.position, Vector2.right * direction, 10f);
        float distance = Mathf.Abs(hit.point.x - rayPos.transform.position.x);
        Debug.Log(distance);
        Debug.DrawRay(rayPos.transform.position, Vector2.right * direction, Color.red);
        
    }
    
}
