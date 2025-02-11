
using UnityEngine;

public class MovementService : IMovable
{
    private readonly Rigidbody2D rigidbody2D;
    private Transform transform;
    
    

    public MovementService(Rigidbody2D rigidbody2D, Transform transform )
    {
        this.rigidbody2D = rigidbody2D;
        this.transform = transform;
        
    }
    
    public void Jump(float xVelocity,float yVelocity)
    {
        rigidbody2D.drag = 0;
        float direction = transform.localScale.x > 0 ? xVelocity : -xVelocity;
        
        rigidbody2D.velocity = new Vector2( direction , yVelocity); 
        
        Debug.Log($"Jump Velocity: {rigidbody2D.velocity}");
        
    }

    public void DoubleJump(float xVelocity,float yVelocity)
    {
        
    }

    public void WallJump(float xVelocity, float yVelocity)
    {
        rigidbody2D.drag = 0;
        float wallDirection = transform.localScale.x > 0 ? -xVelocity : xVelocity;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0 );
        rigidbody2D.AddForce(new Vector2(wallDirection, yVelocity), ForceMode2D.Impulse);
    }
    
    public void ReversedWallJump(float xVelocity,float yVelocity)
    {
    }
    
    public void Clinging()
    {
        
    }
}