using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.XR;

public class SeekerController : Controller
{
    [Header("Movement")]
    [SerializeField] float patrolSpeed = 3.0f;
    [SerializeField] float aggroSpeed = 5.0f;
    [SerializeField] float aggroDistance = 20f;

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

        if (currentState.GetType() == typeof(AirPatrol) && Vector3.Distance(transform.position, Player.instance.transform.position) < aggroDistance)
        {
            AirSeekPlayer seekState = new AirSeekPlayer // Create new AirSeekPlayer class and pass through variables
            {
                speed = aggroSpeed,
                pathUpdateDelay = pathUpdateDelay,
                nextWaypointDistance = nextWaypointDistance
            };

            SetNextState(seekState);
        }

        if (currentState.GetType() == typeof(AirSeekPlayer) && Vector3.Distance(transform.position, Player.instance.transform.position) > aggroDistance * 1.4f)
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
