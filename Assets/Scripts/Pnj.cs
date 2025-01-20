using UnityEngine;
using UnityEngine.UI;
public class Pnj : MonoBehaviour {

    [SerializeField] Collider2D hitbox;
    [SerializeField] Collider2D chatbox;

    [SerializeField] Text dial;

    [SerializeField] int QuestId;

    public float killDist;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            if (col.GetComponent<Player>().Quest_Objects.Contains(QuestId)) {
                if (QuestId == 0) {
                    dial.text = "Amazing ! Take this innate rune.\nThe Rolling rune.\nBackseat: 'C' to use, you can hold to roll farther may break some walls plus you can combine with wilds rune.";
                    col.GetComponent<PlayerMovement>().RouladeRune = true;
                }
                else {
                    dial.text = "My dude thanks, take this Stomping rune. ' V ' to use.";
                    col.GetComponent<PlayerMovement>().ChargeRune = true;
                }
            }
            if (col.IsTouching(chatbox) && !col.IsTouching(hitbox))
                dial.gameObject.SetActive(true);

            if (col.IsTouching(hitbox) && col.GetComponent<PlayerMovement>().IsRolling)
                Destroy(gameObject);

        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Player" && !col.IsTouching(chatbox))
            dial.gameObject.SetActive(false);
    }
}
