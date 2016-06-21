using UnityEngine;
using System.Collections;

public class DamageCollider : MonoBehaviour {
    [SerializeField] GameObject source;
    [SerializeField] int damage = 25;
    [SerializeField] int delayTime = 10;
    [SerializeField] bool anim = false;

    Damageable dmg;

    public GameObject Source { get { return source; } set { source = value; } }
    public int Damage { get { return damage; } set { damage = value; } }

    void Start() {
        if (!anim) StartCoroutine(DelayDestruction(delayTime));
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "MeleeAttack") { Destroy(this.gameObject); return; }
        dmg = collision.gameObject.GetComponent<Damageable>();
        if (!dmg) return;
        if (!Source) {
            dmg.TakeDamage(damage);
            if (!anim) StartCoroutine(DelayDestruction(0.15f));
            return;
        }
        if (collision.gameObject.tag != Source.tag && collision.gameObject != source) {
            dmg.TakeDamage(damage);
            if (!anim) StartCoroutine(DelayDestruction(0.15f));
        }
    }

    void DestroyProjectile() {
        Destroy(gameObject);
    }

    IEnumerator DelayDestruction(float delay) {
        yield return new WaitForSeconds(delay);
        DestroyProjectile();
    }

    public void DestroyOnAnimationEnd() {
        DestroyProjectile();
    }
}
