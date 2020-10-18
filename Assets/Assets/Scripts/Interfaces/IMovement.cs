using UnityEngine;

public interface IMovement
{
    // moves object towards position
    void Move(Vector2 position);
    // continual movement for update loop.
    void Moving();
    bool Stopped();
    void SetMovementSpeed(float speed);
    float GetMovementSpeed();
    // returns position of current waypoint if set
    Vector2 GetWaypointPosition();
}
