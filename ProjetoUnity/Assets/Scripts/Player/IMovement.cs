using UnityEngine;

public interface IMovement
{
    public Vector2 GetSpeed();
    public Vector2 GetGravitySpeed();
    public void SetRotation(float rotation);
}
