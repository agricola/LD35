using UnityEngine;
using System.Collections;

public class DeathBox : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision) {
        Damageable dmg = collision.gameObject.GetComponent<Damageable>();
        if (dmg) dmg.Kill();
        dmg = collision.gameObject.GetComponentInParent<Damageable>();
        if (dmg) dmg.Kill();
    }
}
