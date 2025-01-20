using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour {

    [SerializeField] bool[] Selected;
    [SerializeField]public List<int> Runes; // 0 => Jump - 1 => Speed - 2 => Lumiere - 3 => Levitation


    public List<int>   Quest_Objects;
    public int Rune_Index = 0;

    public float slowMotionTimeScale = 0.1f;

    [SerializeField] AudioSource source;

    [SerializeField] AudioClip right;
    [SerializeField] AudioClip left;
    [SerializeField] AudioClip pickUp;

    [SerializeField] Text text;
    [SerializeField] Image img;

    [SerializeField] List<Sprite> sprites;

    Vector2 last_checkpoint;

    [SerializeField] Light2D light;

    [SerializeField] bgAnimatorManager bgAnim;

    public Transform sprite_transform;
    public Animator anim;

    bool IsInSonicCollider = false;
    bool IsInDarkZone = false;

    bool IsInRuneCollider = false;
    bool IsInQuestObjectCollider = false;

    public GameObject sonicHair;
    public GameObject rune;
    public GameObject obj;

    [SerializeField] AudioManager audio;

    void Start() {
        Selected = gameObject.GetComponent<PlayerMovement>().runes;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E))
            NextRune();
        else if (Input.GetKeyDown(KeyCode.Q))
            PrecRune();
        if (Input.GetKeyDown(KeyCode.H))
            img.enabled = !img.enabled;
        img.sprite = sprites[Rune_Index];
        anim.SetInteger("Rune", Rune_Index);

        if (IsInSonicCollider && Input.GetKeyDown(KeyCode.F)) {
            Destroy(sonicHair.gameObject);
            sprite_transform.position += new Vector3(-0.1f, 0.4f, 0f);
            anim.SetBool("Sonic", true);
        }

        if (IsInRuneCollider && Input.GetKeyDown(KeyCode.F) && !Runes.Contains(rune.GetComponent<Id>().id)) {
            Runes.Add(rune.GetComponent<Id>().id);
            Destroy(rune.gameObject);
            if (Rune_Index == 0)
                Rune_Index = Runes[0];
            last_checkpoint = rune.transform.position;
            source.Stop();
            source.clip = pickUp;
            source.Play();
            IsInRuneCollider = false;
        }

        if (IsInQuestObjectCollider && Input.GetKeyDown(KeyCode.F) && !Quest_Objects.Contains(obj.GetComponent<Id>().id)) {
            Quest_Objects.Add(obj.GetComponent<Id>().id);
            Destroy(obj.gameObject);
            last_checkpoint = obj.transform.position;
            source.Stop();
            source.clip = pickUp;
            source.Play();
            IsInQuestObjectCollider = false;
        }

    }
    public void NextRune() {
        if (Runes.Count == 0)
            return ;
        int tmp = Rune_Index;
        if (Rune_Index < Runes.Count)
            Rune_Index++;
        else
            Rune_Index = 0;
        Selected[tmp] = false;
        Selected[Rune_Index] = true;
        source.Stop();
        source.clip = right;
        source.Play();
    }

    public void PrecRune() {
        if (Runes.Count == 0)
            return ;
        int tmp = Rune_Index;
        if (Rune_Index > 0)
            Rune_Index--;
        else
            Rune_Index = Runes.Count;
        Selected[tmp] = false;
        Selected[Rune_Index] = true;
        source.Stop();
        source.clip = left;
        source.Play();
    }

    public void    die()
    {
        light.enabled = false;
        transform.position = last_checkpoint;
        audio.PlayDeathOS();
    }


    void OnCollisionEnter2D( Collision2D coll ) {
        if (coll.collider.CompareTag("Enemy")){
            die();
        }
    }

    void OnTriggerEnter2D( Collider2D coll) {
        if (coll.tag == "Sonic") {
            IsInSonicCollider = true;
            text.enabled = true;
        }

        if (coll.CompareTag("Checkpoint")){
            last_checkpoint = coll.transform.position;
        }

        if (coll.CompareTag("DarkZone"))
            IsInDarkZone = true;

        if (coll.CompareTag("Rune") && !Runes.Contains(coll.GetComponent<Id>().id)) {
            IsInRuneCollider = true;
            rune = coll.gameObject;
        }

        if (coll.CompareTag("Quest_Object") && !Quest_Objects.Contains(coll.GetComponent<Id>().id)) {
            IsInQuestObjectCollider = true;
            obj = coll.gameObject;
        }
        bgAnim.trigger(coll);
    }


    void OnTriggerExit2D( Collider2D coll ) {
        if (coll.CompareTag("Sonic")) {
            IsInSonicCollider = false;
            text.enabled = false;
        }
        if (coll.CompareTag("DarkZone"))
            IsInDarkZone = false;
        if (coll.CompareTag("Rune"))
            IsInRuneCollider = false;
        if (coll.CompareTag("Quest_Object"))
            IsInQuestObjectCollider = false;
    }
}
