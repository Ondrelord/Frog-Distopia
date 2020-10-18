using UnityEngine;

public interface IMover 
{
    void MoveTo(Vector2 position);
    void Moving();
    bool StoppedMoving();
    IMovement GetMovement();

}
