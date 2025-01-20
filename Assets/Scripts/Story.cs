using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Story : MonoBehaviour
{
    [SerializeField] Text Begin;
    [SerializeField] Text Happy_End;
    [SerializeField] Text Bad_End;


    [SerializeField] Player player;
    void OnTriggerEnter2D(Collider2D coll) {
        if (player.Runes.Count < 4)
            Begin.enabled = true;
        else {
            Begin.enabled = false;
            if (player.Runes.Contains(4))
                Bad_End.enabled = true;
            else
                Happy_End.enabled = false;
        }
    }
    void OnTriggerExit2D(Collider2D coll) {
        Begin.enabled = false;
        Happy_End.enabled = false;
        Bad_End.enabled = false;
    }
}
