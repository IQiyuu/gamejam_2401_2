using UnityEngine;
using System.Collections;

public class EnnemyMouseMove : MonoBehaviour {

    [SerializeField] Transform Current_waypoint;
    [SerializeField] Transform Next_Waypoint;

    [SerializeField] float Speed;

    void Start() {
        transform.position = Current_waypoint.position;
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, Next_Waypoint.position, Speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, Next_Waypoint.position) < 1f) {
            Transform tmp = Current_waypoint;
            Current_waypoint = Next_Waypoint; 
            Next_Waypoint = tmp;
        }
    }
}
