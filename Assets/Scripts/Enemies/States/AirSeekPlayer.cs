using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AirSeekPlayer : State
{
    public float speed = 3.0f; // Movement speed
    public float pathUpdateDelay = 1.0f; // How often to update the path
    public float nextWaypointDistance = 1.5f; // How far the patroller should check for the next waypoint

    int currentWaypoint = 0;

    float timeSinceUpdatedPath;

    Path path;
    Seeker seeker;

    public override void OnEnter(Controller controller)
    {
        base.OnEnter(controller);

        seeker = controller.GetComponent<Seeker>();

        timeSinceUpdatedPath = 0;
        nextWaypointDistance = 0.5f;
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

        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (currentWaypoint < 0 || currentWaypoint >= path.vectorPath.Count) { return; }
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
        seeker.StartPath(transform.position, Player.instance.transform.position, SetPath);
    }

    private void SetPath(Path p)
    {
        if (p.error) { return; }

        path = p;
        currentWaypoint = 0;
    }
}
