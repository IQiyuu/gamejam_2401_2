using UnityEngine;

public class bgAnimatorManager : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void trigger( Collider2D coll ) {
        if (coll.tag == "SpeedZone")
            anim.SetInteger("map", 1);
        if (coll.tag == "JumpZone")
            anim.SetInteger("map", 2);
        if (coll.tag == "Outdoor")
            anim.SetInteger("map", 0);
        if (coll.tag == "LumZone")
            anim.SetInteger("map", 3);
        if (coll.tag == "DarkZone")
            anim.SetInteger("map", 4);
        if (coll.tag == "UnderCity")
            anim.SetInteger("map", 5);
        if (coll.tag == "LegZone")
            anim.SetInteger("map", 6);
    }
}
