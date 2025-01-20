using UnityEngine;
using System.Collections;  // Nécessaire pour utiliser les coroutines

public class CameraWaypointMover : MonoBehaviour
{
    public Transform waypoint_current;
    public Transform next_waypoint;
    public float speed = 2.0f;

	public float waitTime;
    private bool isMoving = false;
    private bool canMove = true;

    void Start()
    {
        if (waypoint_current != null)
        {
            transform.position = waypoint_current.position;
        }
    }

    void Update()
    {
        if (next_waypoint == null || !isMoving || !canMove)
			return;
        transform.position = Vector3.MoveTowards(transform.position, next_waypoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, next_waypoint.position) < 0.1f)
        {
            waypoint_current = next_waypoint; 
            next_waypoint = null;
            isMoving = false;
            StartCoroutine(WaitAndAllowMovement(waitTime));
        }
    }

    public void SetNextWaypoint(Transform newWaypoint)
    {
        if (isMoving || !canMove || newWaypoint == null)
			return;
        next_waypoint = newWaypoint;
        isMoving = true;
    }

    private IEnumerator WaitAndAllowMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
