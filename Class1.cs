using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaypointMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Array to hold the reference to waypoints
    public float speed = 10f;  // Speed of movement
    private int currentWaypointIndex = 0;  // Index of the current waypoint

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
            currentWaypointIndex++;
            Debug.Log("Reached waypoint index: " + currentWaypointIndex);
            // If we've reached the last waypoint, loop back to the first waypoint
            if (currentWaypointIndex >= waypoints.Length || currentWaypointIndex == 0)
            {
                currentWaypointIndex = 0;
                transform.Rotate(Vector3.up, 180f);

            }
        }
    }
}
