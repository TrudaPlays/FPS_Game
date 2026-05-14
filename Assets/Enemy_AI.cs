using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 10f;
    public float stopRange = 15f;

    [Header("Patrol Settings")]
    public Transform[] waypoints; // waypoints for the enemy to patrol between when not chasing the player
    private int currentWaypointIndex = 0;

    private NavMeshAgent agent;
    public bool isChasing = false;

    [Header("Combat Settings")]
    public float damage = 10f;
    public float fireRate = 1.5f; // Seconds between shots
    private float nextFireTime;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        // Start moving to the first waypoint
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //CHASE LOGIC
        //calculates the distance between the enemy and the player
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= chaseRange)
        {
            isChasing = true;
        }
        else if (distance > stopRange)
        {
            isChasing = false;
            agent.ResetPath();//stops the agent where it is
        }

        if (isChasing)
        {
            //tell the NavMeshAgent to move toward the player
            agent.SetDestination(player.position);
            
        }
        else
        {
            Patrol();
        }
        // --- NEW: SHOOTING LOGIC ---
        // We check if the player is in range and if the cooldown timer is ready
        if (distance <= chaseRange && Time.time >= nextFireTime)
        {
            ShootPlayer();
            nextFireTime = Time.time + fireRate; // Reset the cooldown
        }


    }

    void ShootPlayer()
    {
        // Direction from enemy to player
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;

        Vector3 rayStart = transform.position + (direction * 1.5f);

        // The enemy shoots a ray directly at the player
        if (Physics.Raycast(transform.position, direction, out hit, chaseRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player's collider was hit!!");
                PlayerHealth playerHealth = hit.collider.GetComponentInParent<PlayerHealth>();
                playerHealth.TakeDamage(damage);
            }
        }
        // Draw the enemy's shot in red so you can see it
        Debug.DrawRay(transform.position, direction * chaseRange, Color.red, 0.2f);
    }

    void Patrol()
    {
        // If we have no waypoints, don't do anything
        if (waypoints.Length == 0) return;

        // Check if we are close to the current waypoint
        // agent.remainingDistance is the distance left on the path
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Move to the next waypoint in the array
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    //visual aid in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopRange);
    }
}
