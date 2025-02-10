
using UnityEngine;

public class MovementService : IMovable
{
    private readonly Rigidbody2D rigidbody2D;
    private Transform transform;
    
    private bool isFacingRight = true;

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
        Debug.Log($"your direction: {isFacingRight}");
    }

    public void DoubleJump(float xVelocity,float yVelocity)
    {
        
    }

    public void WallJump(float xVelocity, float yVelocity)
    {
        rigidbody2D.drag = 0;

        // Определяем направление отталкивания
        float wallDirection = transform.localScale.x > 0 ? -xVelocity : xVelocity;

        // Прыжок (изменяем velocity перед scale)
        rigidbody2D.velocity = new Vector2(wallDirection, yVelocity);
        Debug.Log($"WallJump Velocity: {rigidbody2D.velocity}");

        // Меняем масштаб с небольшим задержкой
        FlipScale();
    }

// Метод для смены масштаба
    private void FlipScale()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;  // Просто инвертируем
        transform.localScale = scale;
    }


    public void ReversedWallJump(float xVelocity,float yVelocity)
    {
    }
    
    public void Clinging()
    {
        
    }
}