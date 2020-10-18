using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectMoveHandler : MonoBehaviour ,IMovement
{ 
    private Rigidbody2D rb;

    public float movementSpeed;
    private bool waypointSet;
    private Vector2 waypoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Command to move object.
    public void Move(Vector2 position)
    {
        // new waypoint
        waypointSet = true;
        waypoint = position;
        Moving();
    }

    // Continual move towards waypoint for update loop.
    public void Moving()
    {
        // if theres no waypoint set, stop moving
        if (!waypointSet)
            return;

        // move closer to waypoint
        Vector2 dir = Utils.GetDirection(transform.position, waypoint);
        rb.MovePosition(rb.position + dir * movementSpeed * Time.deltaTime);

        // near waypoint
        if (Vector2.Distance(rb.position, waypoint) < 0.5f)
            waypointSet = false;
    }

    public bool Stopped() => !waypointSet;

    public void SetMovementSpeed(float speed) => movementSpeed = speed;

    public float GetMovementSpeed() => movementSpeed;

    public Vector2 GetWaypointPosition() => waypoint;
}
