using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    private ParticleSystem smokeEffect;
    public Rigidbody2D rb;
    public Animator animator; 
    bool isFacingRight = true;

    [Header("Movement")]
    float direction = 0f;
    private float Direction;
    public float baseSpeed = 10f;
    public float currentSpeed;
    public float speedMultiplier =1.5f;
    float horizontalMovement;
    

    [Header("Jumping")]
    public float jumpHeight = 10f;
    public int maxJumps = 2;
    int jumpsRemaing;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f,0.05f);
    public LayerMask groundLayer;
    public LayerMask OneWayLayer;
    public LayerMask breakableGroundLayer;

    [Header("Runes")]
    private bool IsHolding = false;
    private float pressDuration;
    private float pressTime;
    public bool IsRolling = false;

    public bool IsCharging = false;

    public bool IsRebond = false;

    public bool IsDownCharge = false;

    public bool[] runes = {false,false,false,false};

    public bool RouladeRune = false;
    public bool ChargeRune = false;


    private float speedRuneMultiplier = 1f;
    private float jumpRuneMultiplier = 1f;
    //private float levitationRuneValue = 3f;

    [Header("Roulade")]
    public float Rollduration = 2f;
    public float RollSpeed = 10f;
    public float addSpeed = 1.5f;
    public float addJump = 1.5f;
    public float levitationValue = 1.5f;

    public Collider2D lCol;
    public Collider2D rCol;

    float duration;

    [SerializeField] AudioManager audio;
    public bool airborn = false;

    [SerializeField] Light2D light;

    // Update is called once per frame
    void Update()
    {
        if (IsRebond || IsRolling)
            return ;
        if (IsHolding)
            horizontalMovement = 0;
        rb.linearVelocity = new Vector2(horizontalMovement * currentSpeed * speedRuneMultiplier, rb.linearVelocityY);
        currentSpeed = baseSpeed;
        if (rb.linearVelocityX != 0 && !airborn && !audio.isPlayingWalk()) 
            audio.PlayWalk();
        else if (audio.isPlayingWalk() && rb.linearVelocityX == 0 && (Input.GetAxis("Horizontal") == 0 || airborn))
            audio.StopAudio();
        if (rb.linearVelocityY > 0 && !airborn) {
            airborn = true;
            audio.PlayJumpOS();
        }
        if (airborn && rb.linearVelocityY == 0 && GroundCheck()) {
            airborn = false;
            audio.PlayLandOS();
        }
        GroundCheck();
        Flip();
        
        animator.SetFloat("yVelocity", rb.linearVelocityY);
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);
        if (RouladeRune) {
            if (Input.GetKeyDown(KeyCode.C) && GroundCheck() && IsRolling == false)
            {
                rb.linearVelocity = Vector2.zero;
                pressTime = Time.time;
                IsHolding = true;
            }
            
            if (Input.GetKeyUp(KeyCode.C) && IsRolling == false)
            {
                if (IsHolding)
                {
                    pressDuration = Time.time - pressTime;
                    Direction = Input.GetAxis("Horizontal");
                    StartCoroutine(Roulade());
                    IsHolding = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.V) && IsRolling == false && IsDownCharge == false && !GroundCheck() && ChargeRune)
        {
            Vector2 direction;
            RaycastHit2D hit;
            Vector2 originPos = transform.position;
            direction = Vector2.down;
            hit = Physics2D.Raycast(originPos, direction, 1.0f, breakableGroundLayer);
            if (hit != null)
            {
                //rb.linearVelocity = Vector2.zero;
                rb.linearVelocity = new Vector2(rb.linearVelocityX , 20);
                StartCoroutine(DownCharge());
            }
        }
        Rune();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!IsHolding && !IsRebond && !IsRolling)
            horizontalMovement = context.ReadValue<Vector2>().x;
        else
            horizontalMovement = 0;
    }

    bool GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer | OneWayLayer | breakableGroundLayer))
        {
            jumpsRemaing = maxJumps;
            return true;
        }
        return false;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(jumpsRemaing>0 && !IsHolding && (GroundCheck() || IsRolling) && !IsRebond)
        {
            if(context.performed)
            {
                //Holding jump button  = max height.
                rb.linearVelocity = new Vector2(rb.linearVelocityX , jumpHeight * jumpRuneMultiplier);
                jumpsRemaing --;
                animator.SetTrigger("Jump");
            }
            else if(context.canceled)
            {
                //Tapping jump button = mid height.
                rb.linearVelocity = new Vector2(rb.linearVelocityX , rb.linearVelocityY * 0.5f * jumpRuneMultiplier);
                jumpsRemaing--;
                animator.SetTrigger("Jump");
            }
        
        }
         
    }

    private void Flip()
    {
        if (isFacingRight && horizontalMovement <0|| !isFacingRight && horizontalMovement >0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    
    private IEnumerator DownCharge()
    {
        IsDownCharge = true;
        Vector2 direction;
        RaycastHit2D hit;
        while (!GroundCheck())
        {
            Vector2 originPos = transform.position;
            direction = Vector2.down;
            hit = Physics2D.Raycast(originPos, direction, 1.0f, breakableGroundLayer);
            rb.linearVelocity = new Vector2(rb.linearVelocityX , -20);
            if (hit.collider != null)
            {
                Destroy(hit.collider.gameObject);
            }

            yield return null;
        }
        IsDownCharge = false;
                
    }

    private IEnumerator Roulade()
    {
        jumpsRemaing++;
        audio.PlayRoll();
        if (Direction > 0)
            direction  = 1;
        else if (Direction < 0)
            direction = -1;
        duration = 0f;
        if (pressDuration > 1.5)
            pressDuration = 1.5f;
        if (pressDuration < 0.5f)
            pressDuration = 0.5f;
        IsRolling = true;
        while (duration < pressDuration * Rollduration && IsRolling)
        {
            duration += Time.deltaTime;
            rb.linearVelocity = new Vector2(RollSpeed * direction * speedRuneMultiplier * (pressDuration - (duration / Rollduration)) , rb.linearVelocityY);
            yield return null;
        }
        IsRolling = false;
        GroundCheck();
    }

    private IEnumerator Rebond() {
        IsRebond = true;
        audio.PlayRollCollisionOS();
        float n_duration = 0f;
        rb.linearVelocity = new Vector2(RollSpeed * -direction * speedRuneMultiplier * 1.5f, rb.linearVelocity.y+1.5f*(RollSpeed-Rollduration));
        while (n_duration + duration < pressDuration * Rollduration) {
            if (!audio.isPlayingRollCollision() && !audio.isPlayingRoll())
                audio.PlayRoll();
            n_duration += (Time.deltaTime*2);
            rb.linearVelocity = new Vector2((RollSpeed/1.5f) * -direction * speedRuneMultiplier * 1.5f, rb.linearVelocity.y);
            yield return null;
        }
        GroundCheck();
        IsRebond = false;
        rb.linearVelocity = Vector2.zero;
    }

    void Rune()
    {
        if (runes[2]) // si il y a la rune speed active
            speedRuneMultiplier = addSpeed;
        else
            speedRuneMultiplier = 1f;
        if (runes[1]) // si il y a la rune jump active
            jumpRuneMultiplier = addJump;
        else
            jumpRuneMultiplier = 1f;
        if (runes[3]) // si il ya la rune levitation active
            light.pointLightOuterRadius = 7;
        else
            light.pointLightOuterRadius = 0.7f;
        if (runes[4]) // si il ya la rune levitation active
            rb.gravityScale = levitationValue;
        else
            rb.gravityScale = 5f;
    }

    //Gizmos aren't visible on the game execution (dev tool).
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    void OnTriggerEnter2D( Collider2D coll ) {
        if (coll.tag == "WallBreakable" && IsRolling && (coll.IsTouching(rCol) || coll.IsTouching(lCol))) {
            coll.GetComponent<AudioSource>().Play();
            Destroy(coll.gameObject);
            StopCoroutine(Roulade());
            IsRolling = false;
            IsRebond = true;
            StartCoroutine(Rebond());
        }
        if (coll.tag == "Breakable" && IsDownCharge) {
            coll.GetComponent<AudioSource>().Play();
            Destroy(coll.gameObject);
        }
        if (coll.tag == "DarkZone")
            light.enabled = true;
        
    }

    void OnTriggerExit2D( Collider2D coll ) {
        if (coll.tag == "DarkZone")
            light.enabled = false;
    }

}
