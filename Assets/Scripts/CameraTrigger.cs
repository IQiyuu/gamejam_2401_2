using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform waypointToGo;
    public CameraWaypointMover cameraMover;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (cameraMover != null && waypointToGo != null)
            {
                cameraMover.SetNextWaypoint(waypointToGo);
            }
        }
    }
}
