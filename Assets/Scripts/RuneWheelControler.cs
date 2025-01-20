using UnityEngine;
using UnityEngine.UI;

public class RuneWheelControler : MonoBehaviour
{
    public Animator anim;
    private bool runeWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int runeID;
    public GameObject Player;

    public PlayerMovement playermovement;
    public Player playerrune;
    void Start()
    {
        playermovement = Player.GetComponent<PlayerMovement>();
        playerrune = Player.GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            runeWheelSelected = !runeWheelSelected;
        }

        if (runeWheelSelected)
        {
            anim.SetBool("OpenRuneWheel", true);

        }
        else
        {
            anim.SetBool("OpenRuneWheel", false);
        }

        switch (runeID)
        {

            case 0:
                selectedItem.sprite = noImage;
                break;
            case 1:
                if (playerrune.Runes.Contains(1))
                {
                    Debug.Log("Speed");
                    playermovement.runes[2] = true;
                    playermovement.runes[1] = false;
                    playermovement.runes[3] = false;
                    break;
                }
                break;
            case 2:
                if (playerrune.Runes.Contains(0))
                {
                    Debug.Log("Jump");
                    playermovement.runes[1] = true;
                    playermovement.runes[2] = false;
                    playermovement.runes[3] = false;
                    break;
                }
                break;
            case 3:
                if (playerrune.Runes.Contains(3))
                {
                    Debug.Log("Levitation");
                    playermovement.runes[1] = false;
                    playermovement.runes[2] = false;
                    playermovement.runes[3] = true;
                    break;
                }
                Debug.Log("Levitation");
                break;
            case 4:
                if (playerrune.Runes.Contains(2))
                {
                    Debug.Log("Lumiere");
                    playermovement.runes[4] = true;
                    playermovement.runes[2] = false;
                    playermovement.runes[1] = false;
                    playermovement.runes[3] = false;
                    break;
                }
                break;
        }
    }
}
