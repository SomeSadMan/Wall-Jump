public interface IMovable
{
    public void Clinging();
    public void Jump(float xVelocity,float yVelocity);
    
    public void DoubleJump(float xVelocity,float yVelocity);
    
    public void WallJump(float xVelocity,float yVelocity);
    
    public void ReversedWallJump(float xVelocity,float yVelocity);
    
}