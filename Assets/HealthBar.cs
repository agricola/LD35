using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject player;

    Damageable dmg;

    void Start() {
        dmg = player.GetComponent<Damageable>();
    }

    void Update() {
        healthSlider.value = dmg.Health;
    }
}
