using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RuneWheelButonControler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int ID;
    private Animator anim;
    public string RuneName;
    public TextMeshProUGUI runeText;
    public Image selectRune;
    private bool selected = false;
    public Sprite icon;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
           selectRune.sprite = icon;
           runeText.text = RuneName;
        }
    }

    public void Selected()
    {
        selected = true;
        RuneWheelControler.runeID = ID;
    }

    public void Deselected()
    {
        selected = false;
        RuneWheelControler.runeID = 0;
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
        runeText.text = RuneName;
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        runeText.text = "";
    }

}
