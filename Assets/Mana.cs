using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour {

	[SerializeField] int maxMana = 100;
    [SerializeField] float currentMana = 100;

    public int MaxMana { get { return maxMana; } }
    public float CurrentMana { get { return currentMana; } set { currentMana = value; } }

    void Update() {
        SpendMana(-0.5f);
        if (currentMana > maxMana) currentMana = maxMana;
    }

    public bool SpendMana(float cost) {
        if (cost > currentMana) return false;
        currentMana -= cost;
        return true;
    }
}
