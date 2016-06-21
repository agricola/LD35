using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {


    [SerializeField] Slider manaSlider;
    [SerializeField] GameObject player;

    Mana mana;

    void Start() {
        mana = player.GetComponent<Mana>();
    }

    void Update() {
        manaSlider.value = mana.CurrentMana;
    }
}
