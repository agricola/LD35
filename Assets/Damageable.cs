using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Damageable : MonoBehaviour {
    [SerializeField] int maxHealth = 500;
    [SerializeField] int currentHealth = 500;
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] bool sound = true;

    Color oldColor;

    SpriteRenderer sR;
    AudioManager au;
    PlayerController pC;

    public int MaxHealth { get { return maxHealth; } }
    public int Health { get { return currentHealth; } set { currentHealth = value; } }

    void Awake() {
        maxHealth = currentHealth;
        sR = GetComponent<SpriteRenderer>();
        oldColor = sR.color;
    }

    void Start() {
        if (sound) {
            if (!audioSource) audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
            if (audioSource) au = audioSource.GetComponent<AudioManager>();
        }
        pC = GetComponent<PlayerController>();
    }

    void Update() {
        if (currentHealth <= 0) Kill();
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        if (pC && sound) au.PlaySound(Sound.Hit);
        currentHealth -= damage;
        StartCoroutine(FlashRed());

    }

    public void Kill() {
        if (sound) au.PlaySound(Sound.Hit);
        if (gameObject.tag == "Player") SceneManager.LoadScene("start");
        if (gameObject.GetComponent<Boss>()) SceneManager.LoadScene("End");
        Destroy(gameObject);
    }

    IEnumerator FlashRed() {
        sR.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sR.color = oldColor;
    }
}
