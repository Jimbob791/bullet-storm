using System.Collections.Generic;
using UnityEngine;

public class ShooterController : Controller
{
    [Header("Projectiles")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;

    [Header("Movement")]
    [SerializeField] float patrolSpeed = 3.0f;
    [SerializeField] float aggroSpeed = 5.0f;
    [SerializeField] float shootDistance = 20f;

    [Header("Pathfinding")]
    [SerializeField] float pathUpdateDelay = 1.0f;
    [SerializeField] List<Vector2> patrolPositions = new List<Vector2>();
    [SerializeField] float nextWaypointDistance = 2;

    Animator anim;
    Rigidbody2D rb;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        AirPatrol patrolState = new AirPatrol // Create new AirPatrol class and pass through variables
        {
            patrolPositions = patrolPositions,
            speed = patrolSpeed,
            pathUpdateDelay = pathUpdateDelay,
            nextWaypointDistance = nextWaypointDistance
        };

        SetState(patrolState);
    }

    protected override void Update()
    {
        base.Update();

        //anim.SetBool("seeking", currentState.GetType() == typeof(AirSeekPlayer));
        transform.localScale = new Vector3(facing, 1, 1);

        if (currentState.GetType() == typeof(AirPatrol) && Vector3.Distance(transform.position, Player.instance.transform.position) < shootDistance)
        {
            AirShootProjectile shootState = new AirShootProjectile // Create new AirShootProjectile class and pass through variables
            {
                projectilePrefab = projectilePrefab,
                projectileSpeed = projectileSpeed
            };

            SetNextState(shootState);
        }

        if (currentState.GetType() == typeof(AirShootProjectile) && currentState.scaledTime > 6f/12f)
        {
            AirPatrol patrolState = new AirPatrol // Create new AirPatrol class and pass through variables
            {
                patrolPositions = patrolPositions,
                speed = patrolSpeed,
                pathUpdateDelay = pathUpdateDelay,
                nextWaypointDistance = nextWaypointDistance
            };

            SetNextState(patrolState);
        }
    }
}
