using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {
    [SerializeField] GameObject firedProjectile;
    [SerializeField] GameObject meleeAttack;
    [SerializeField] float speed = 5;
    [SerializeField] float cd = 0.5f;
    [SerializeField] int damage = 10;
    bool cooldown = false;

    GameObject atk;
    PlayerController pC;
    AudioManager au;

    Color y;
    Color r;

    public float CD { get { return cd; } set { cd = value; } }
    public float Speed { get { return speed; } set { speed = value; } }
    public bool Cooldown { get { return cooldown; } }

    public void ShootManaProjectile(Vector2 target, int cost) {
        if (cooldown) return;
        Mana mana = GetComponent<Mana>();
        mana.SpendMana(cost);
        CreateProjectile(target, y);
        StartCoroutine(CooldownShot());
    }

    public void ShootProjectile(Vector2 target) {
        if (cooldown) return;
        CreateProjectile(target, r);
        StartCoroutine(CooldownShot());
    }

    public void MeleeAttack(Vector2 target) {
        CreateMeleeAttack(target);
    }

    void Start() {
        pC = GetComponent<PlayerController>();
        y = new Color(0.98F, 0.95F, 0.21F, 0.75F);
        r = new Color(0.67F, 0.2F, 0.2F, 0.75F);
        au = GameObject.Find("Audio Source").GetComponent<AudioManager>();
    }

    void Update() {
        if (atk) {
            atk.transform.position = MeleeAttackPosition(pC.MouseAim());
            atk.transform.localScale = pC.transform.localScale;
            /*if (mouse.x > 0) {
                scale.x *= -1;
                atk.transform.localScale = scale;
            }*/
        }
    }

    void CreateProjectile(Vector2 target) {
        if (au) au.PlaySound(Sound.Shoot);
        GameObject shot = Instantiate(firedProjectile, transform.position, Quaternion.identity) as GameObject;
        Vector2 direction = target.normalized;

        Rigidbody2D rB = shot.GetComponent<Rigidbody2D>();
        rB.velocity = direction * speed;

        DamageCollider dmgC = shot.GetComponent<DamageCollider>();
        dmgC.Source = this.gameObject;
        dmgC.Damage = damage;
    }

    void CreateProjectile(Vector2 target, Color c) {
        if (au) au.PlaySound(Sound.Shoot);
        GameObject shot = Instantiate(firedProjectile, transform.position, Quaternion.identity) as GameObject;
        Vector2 direction = target.normalized;

        shot.GetComponent<SpriteRenderer>().color = c;

        Rigidbody2D rB = shot.GetComponent<Rigidbody2D>();
        rB.velocity = direction * speed;

        DamageCollider dmgC = shot.GetComponent<DamageCollider>();
        dmgC.Source = this.gameObject;
        dmgC.Damage = damage;
    }

    void CreateMeleeAttack(Vector2 target) {
        if (au) au.PlaySound(Sound.Shoot);
        Vector2 pos = MeleeAttackPosition(target);
        GameObject attack = Instantiate(meleeAttack, pos, Quaternion.identity) as GameObject;

        DamageCollider dmgC = attack.GetComponent<DamageCollider>();
        dmgC.Source = this.gameObject;

        atk = attack;
    }

    Vector2 MeleeAttackPosition(Vector2 target) {
        Vector2 displacement = target.normalized;
        displacement.y = 0;
        Vector2 pos = (Vector2)transform.position + displacement;
        return pos;
    }

    IEnumerator CooldownShot() {
        cooldown = true;
        yield return new WaitForSeconds(cd);
        cooldown = false;
        Animator anim = GetComponent<Animator>();
        if (anim && pC) if (!pC.ShapeShifted) anim.SetBool("shooting", false);
    }
}
