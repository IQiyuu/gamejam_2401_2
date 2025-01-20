using UnityEngine;
using System.Collections;
public class DropPlatform : MonoBehaviour
{
    bool playerHere = false;
    [SerializeField] Collider2D col;


    void Update() {
        if (Input.GetKeyDown(KeyCode.S) && playerHere)
            StartCoroutine(Drop());
    }

    private IEnumerator Drop() {
        col.isTrigger = true;
        yield return new WaitForSeconds(0.2f);
        col.isTrigger = false;
    }

    void OnTriggerEnter2D( Collider2D coll ) {
        if (coll.tag == "Player")
            playerHere = true;
    }

    void OnTriggerExit2D( Collider2D coll ) {
        if (coll.tag == "Player")
            playerHere = false;
    }


}
