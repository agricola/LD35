using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    [SerializeField] bool grounded = false;
    [SerializeField] Sprite transformation;
    [SerializeField] Feet feet;
    [SerializeField] GameObject shapeshift;

    [SerializeField] float speed = 1f;
    [SerializeField] float maxSpeed = 5;
    [SerializeField] float attackCooldown = 2;
    bool jumpCD = false;
    bool shapeshifted = false;
    bool shapeshiftCD = false;
    bool attackCD = false;
    Sprite humanform;
    Vector2 oldCollSize;
    Animator oldAnim;

    Rigidbody2D rB;
    FireProjectile fP;
    SpriteRenderer sR;
    Camera cam;
    BoxCollider2D col;
    Mana mana;
    Animator anim;
    RuntimeAnimatorController rAC;

    RuntimeAnimatorController oldRAC;

    public bool Grounded { get { return grounded; } set { grounded = value; } }
    public bool ShapeShifted { get { return shapeshifted; } }

    void Awake() {
        rB = GetComponent<Rigidbody2D>();
        fP = GetComponent<FireProjectile>();
        sR = GetComponent<SpriteRenderer>();
        humanform = sR.sprite;
        col = GetComponent<BoxCollider2D>();
        oldCollSize = col.size;
        mana = GetComponent<Mana>();
        anim = GetComponent<Animator>();
        oldRAC = anim.runtimeAnimatorController;

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update() {
        if (shapeshifted) if(!mana.SpendMana(0.6f)) Shapeshift();

        if (MouseAim().x < 0) {
            transform.localScale = new Vector2(-1,1);
        } else { transform.localScale = new Vector2(1, 1); }

        float Axis = Input.GetAxisRaw("Horizontal");
        if (Axis != 0 ) rB.velocity += new Vector2(Axis, 0) * speed * Time.deltaTime;

        if (Mathf.Abs(rB.velocity.x) > Mathf.Abs(maxSpeed)) {
            Vector2 v = rB.velocity;
            if (rB.velocity.x > 0) {
                v.x = maxSpeed;
            } else {
                v.x = -maxSpeed;
            }
            rB.velocity = v;
        }

        if (Axis != 0 && grounded) anim.SetBool("walking", true);
        else anim.SetBool("walking", false);
        /*
                if (Mathf.Abs(rB.velocity.x) > Mathf.Abs(speed) ) {
                    Vector2 v = rB.velocity;
                    if (v.x > 0) v.x = speed;
                    if (v.x < 0 ) v.x = -speed;
                    rB.velocity = v;
                }*/

        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && grounded && !jumpCD) {
            rB.AddForce(Vector2.up * 300);
            StartCoroutine(JumpCooldown());
            //Grounded = false;
        }

        if (Input.GetButton("Fire1")) {
            if (!shapeshifted) {
                FireBolt();
                anim.SetBool("shooting", true);
            } else {
                LungAttack();
            }
        }

        if (Input.GetKey(KeyCode.Space)) {
            Shapeshift();
        }

        if (!shapeshifted) {
            if (!grounded) anim.SetBool("inAir", true);
            else anim.SetBool("inAir", false);
        }
    }

    IEnumerator JumpCooldown() {
        jumpCD = true;
        yield return new WaitForSeconds(0.5f);
        jumpCD = false;
    }

    void FireBolt() {
        fP.ShootManaProjectile(MouseAim(),30);
    }

    void Shapeshift() {
        if (shapeshiftCD) return;
        shapeshifted = !shapeshifted;
        if (shapeshifted) {
            sR.sprite = shapeshift.GetComponent<SpriteRenderer>().sprite;
            //anim = shapeshift.GetComponent<Animator>();
            anim.runtimeAnimatorController = shapeshift.GetComponent<Animator>().runtimeAnimatorController;
            //Vector2 shapeshiftSize = oldCollSize;
            //shapeshiftSize.y = 1.5f;
            col.size = shapeshift.GetComponent<BoxCollider2D>().size;

            /*Vector2 v = new Vector3(0,0,0);
            v.y = 0.25f;
            feet.transform.localPosition = v;*/
            //feet.ChangeColliders(0.5f, -0.9f, -0.6f, -0.9f);
        } else {
            sR.sprite = humanform;
            anim.runtimeAnimatorController = oldRAC;
            col.size = oldCollSize;
            //feet.ChangeColliders(0.2f, -0.82f, -0.2f, -0.82f);
            //feet.transform.localPosition = new Vector3(0, 0, 0); ;
        }
        StartCoroutine(ShapeshiftCooldown());
    }

    IEnumerator ShapeshiftCooldown() {
        shapeshiftCD = true;
        yield return new WaitForSeconds(0.5f);
        shapeshiftCD = false;
    }

    void LungAttack() {
        if (attackCD) return;
        if (!mana.SpendMana(30)) return;
        anim.SetBool("attacking", true);
        Vector2 dir = MouseAim();
        //rB.AddForce(dir * 500);
        rB.velocity += dir * 7;
        fP.MeleeAttack(dir);
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown() {
        attackCD = true;
        yield return new WaitForSeconds(attackCooldown);
        attackCD = false;
    }

    public Vector2 MouseAim() {
        Vector2 pos =  cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        pos.Normalize();
        return pos;
    }

    public void EndAttack() {
        anim.SetBool("attacking", false);
    }
}
