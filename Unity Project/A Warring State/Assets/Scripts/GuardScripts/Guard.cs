using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public GameManager gameManager;
    public float speed = 5;
    public float waitTime = 0.3f;
    public float turnSpeed = 90;
    public Transform pathHolder;
    public Light spotLight;
    public float viewDistance;
    public float viewAngle;
    public Transform player;
    public LayerMask viewMask;
    public Color originalColor;
    public GuardAnimationManager animManager;
    public Transform visionChecker;
    public Collider playercollider;

    public VisibilityBarScript visibilityBar;
    public float maxVisibilityDuration = 0.5f;
    public float currentVisibility;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentVisibility = 0f;
        visibilityBar.SetMaxDuration(maxVisibilityDuration);
        visibilityBar.SetVisibility(currentVisibility);
        originalColor = spotLight.color;
        viewAngle = spotLight.spotAngle;
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, waypoints[i].y + 1f, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            spotLight.color = Color.red;
            if (currentVisibility < maxVisibilityDuration)
            {
                currentVisibility += 2 * Time.deltaTime;
                visibilityBar.SetVisibility(currentVisibility);
            } else if (currentVisibility >= maxVisibilityDuration)
            {
                //Debug.Log("You failed");
                gameManager.EndGame();
            }
        }
        else
        {
            spotLight.color = originalColor;
            if (currentVisibility > 0f)
            {
                currentVisibility -= 2 * Time.deltaTime;
                visibilityBar.SetVisibility(currentVisibility);
            }
            else
            {
                currentVisibility = 0f;
                visibilityBar.SetVisibility(currentVisibility);
            }
        }
    }

    
    private bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, player.position, out hit);

            //Debug.Log("You're in my viewing distance!");
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle/2f)
            {
                
                if (!Physics.Linecast(transform.position, player.position, out hit, viewMask))
                {
                    Debug.Log("I can see you");
                    return true;
                    
                }
                
            }
        }

        return false;
    }

    /*public Collider IsPlayerInLineOfSight()
    {
        Vector3 dirToPlayer = player.position - transform.position;
        
        
    }*/

    

    IEnumerator FollowPath(Vector3[] waypoints) {
        transform.position = waypoints [1];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints [targetWaypointIndex];
        transform.LookAt (targetWaypoint);

        while (true) {
            transform.position = Vector3.MoveTowards (transform.position, targetWaypoint, speed * Time.deltaTime);
            animManager.AdjustAnimationtoMovement(false);
            if (transform.position == targetWaypoint) {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints [targetWaypointIndex];
                animManager.AdjustAnimationtoMovement(true);
                yield return new WaitForSeconds (waitTime);
                animManager.AdjustAnimationtoMovement(false);
                yield return StartCoroutine (TurnToFace (targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget) {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2 (dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f) {
            float angle = Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.1f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
        
    }
    
    
    //Code based on code from https://github.com/SebLague/Intro-to-Gamedev
}
