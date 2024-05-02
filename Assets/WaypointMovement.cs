using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Array to hold the reference to waypoints
    public float speed = 10f;      // Speed of movement
    private int currentWaypointIndex = 0;  // Index of the current waypoint
    private bool isMovingForward = true;   // Flag to track the direction of movement

    void Update()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned.");
            return;
        }

        // Move towards the current waypoint
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        // Get the direction towards the current waypoint
        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;

        // Move towards the waypoint
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // If the object is close enough to the current waypoint, move to the next waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            Debug.Log("Reached waypoint index: " + currentWaypointIndex); // Log the current waypoint index

            // Move to the next waypoint
            if (isMovingForward)
            {
                currentWaypointIndex++;
            }
            else
            {
                currentWaypointIndex--;
            }

            // Check if we've reached the end or the beginning of the waypoints array
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = waypoints.Length - 2;  // Set index to the second last waypoint
                isMovingForward = false; // Change direction to backward
                TurnAround(); // Turn around when reaching the last index
            }
            else if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 1; // Set index to the second waypoint
                isMovingForward = true; // Change direction to forward
                TurnAround(); // Turn around when reaching the first index
            }
        }
    }

    void TurnAround()
    {
        // Rotate the car 180 degrees around its up axis
        transform.Rotate(Vector3.up, 180f);
    }
}
