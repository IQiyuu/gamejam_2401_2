using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightManager : MonoBehaviour
{
    public Light2D light;

    void OnTriggerEnter2D( Collider2D coll ) {
        if (coll.tag == "DarZone")
            light.enabled = true;
    }

    void OnTriggerExit2D( Collider2D coll ) {
        if (coll.tag == "DarkZone")
            light.enabled = false;
    }
}
