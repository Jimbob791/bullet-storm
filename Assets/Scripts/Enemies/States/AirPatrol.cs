using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class AirPatrol : State
{
    public List<Vector2> patrolPositions = new List<Vector2>(); // List of positions to patrol between
    public float speed = 3.0f; // Movement speed
    public float pathUpdateDelay = 1.0f; // How often to update the path
    public float nextWaypointDistance = 1.5f; // How far the patroller should check for the next waypoint

    int positionIndex;
    int currentWaypoint = 0;

    float timeSinceUpdatedPath;

    Path path;
    Seeker seeker;

    public override void OnEnter(Controller controller)
    {
        base.OnEnter(controller);

        seeker = controller.GetComponent<Seeker>();

        positionIndex = 0;
        timeSinceUpdatedPath = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (timeSinceUpdatedPath <= 0) // Update path if delayed enough
        {
            UpdatePath();
            timeSinceUpdatedPath = pathUpdateDelay;
        }
        timeSinceUpdatedPath -= Time.deltaTime;

        if (path == null) { return; }
        if (currentWaypoint >= path.vectorPath.Count) // At end of the path
        {
            positionIndex = positionIndex + 1 >= patrolPositions.Count ? 0 : positionIndex + 1; // Iterate to next patrol point
            UpdatePath();
            return;
        }

        MoveAlongPath();

    }

    private void MoveAlongPath()
    {
        Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        rb.AddForce(direction * speed * Time.deltaTime);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        controller.facing = direction.x < 0 ? -1 : 1;
    }

    private void UpdatePath()
    {
        if (!seeker.IsDone()) { return; }
        seeker.StartPath(transform.position, (Vector3)patrolPositions[positionIndex], SetPath);
    }

    private void SetPath(Path p)
    {
        if (p.error) { return; }

        path = p;
        currentWaypoint = 0;
    }
}
