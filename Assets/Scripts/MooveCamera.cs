using UnityEngine;

public class MooveCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerpos;
    private Vector3 camerapos;
    private bool canMove = true;

    public Vector3 velocity = Vector3.zero;

    void Update()
    {
        CameraPosition();
        Vector3 playerPosition = player.transform.position;
        if (canMove)
        {
        if (playerPosition.x + 15 < camerapos.x)
            CameraMoovePos(0);
        if (playerPosition.x - 15 > camerapos.x)
            CameraMoovePos(1);
        if (playerPosition.y + 8 < camerapos.y)
            CameraMoovePos(2);
        if (playerPosition.y - 8 > camerapos.y)
             CameraMoovePos(3);
       }
    }

    void Start()
    {
        CameraPosition();
    }


    void CameraMoovePos(int pos)
    {
        if (!canMove) 
            return;
        Vector3 next = transform.position;
        if (pos == 0)
            next = transform.position + Vector3.left * 30;
        else if (pos == 1)
            next = transform.position + Vector3.right * 30;
        else if (pos == 2)
            next = transform.position + Vector3.down * 17;
        else if (pos == 3)
            next = transform.position + Vector3.up * 17;
        StartCoroutine(DisableMovementTemporarily(next));
    }
    void CameraPosition()
    {
        camerapos = transform.position;
    }

    private System.Collections.IEnumerator DisableMovementTemporarily(Vector3 next)
    {
        canMove = false;
        for (int i = 0; i <= 30; i++) {
            transform.position = Vector3.SmoothDamp(transform.position, next, ref velocity, 0.03f);          
            yield return new WaitForSeconds(0.02f);
        }
        canMove = true;        
    }
}