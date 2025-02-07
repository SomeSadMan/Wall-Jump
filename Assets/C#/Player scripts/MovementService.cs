
using UnityEngine;

public class MovementService : IMovable
{
    private readonly Rigidbody2D rigidbody2D;

    public MovementService(Rigidbody2D rigidbody2D)
    {
        this.rigidbody2D = rigidbody2D;
    }
    
    public void Jump(float xVelocity,float yVelocity)
    {
        rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
    }

    public void DoubleJump(float xVelocity,float yVelocity)
    {
        rigidbody2D.velocity = new Vector2(-xVelocity, yVelocity);
    }

    public void WallJump(float xVelocity,float yVelocity)
    {
    }

    public void ReversedWallJump(float xVelocity,float yVelocity)
    {
    }

    public void Clinging()
    {
        //RaycastHit hit = Physics2D.Raycast(,Vector2.right)
    }
}