using UnityEngine;
using System.Collections;

public class HealthPotion : MonoBehaviour {

    [SerializeField] int healthRestoration = 50;

    AudioManager aM;

    void Start() {
        aM = GameObject.Find("Audio Source").GetComponent<AudioManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        UsePot(collision.gameObject);
    }

    void UsePot(GameObject g) {
        Damageable dmg = g.GetComponent<Damageable>();
        if (!dmg || g.tag != "Player") return;
        if (aM) aM.PlaySound(Sound.Pickup);
        dmg.Health += healthRestoration;
        Destroy(gameObject);
    }
}
