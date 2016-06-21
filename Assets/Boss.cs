using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {
    [SerializeField] float fireRate = 0.5f;
    GameObject audioSource;
    bool cd = false;

    public GameObject Spawner { get; set; }

    int maxHealth = 500;
    float multiplier;
    bool tpCD = false;

    FireProjectile fP;
    Damageable dmg;
    SkeletonHead sH;
    Rigidbody2D rB;
    

    void Start() {
        fP = GetComponent<FireProjectile>();
        dmg = GetComponent<Damageable>();
        sH = GetComponent<SkeletonHead>();
        rB = GetComponent<Rigidbody2D>();
        audioSource = GameObject.Find("Audio Source");
        if (audioSource) audioSource.GetComponent<AudioManager>().PlayBossMusic();

        fP.Speed = 10;
        fP.CD = 0.0f;
        maxHealth = dmg.MaxHealth;
    }

    // Update is called once per frame
    void Update() {
        //float hp = dmg.Health;
        //float mHP = maxHealth;
        //multiplier = 1 / (hp/mHP);
        //float w = multiplier / 4;
        //if (w >= 0.5f) w = 0.45f;
        RandomShot();
        if (!tpCD) {
            Teleport();
            StartCoroutine(TeleportCD());
        }
    }

    void RandomShot() {
        Random.seed = (int)System.DateTime.Now.Ticks;
        float rngX = Random.Range(-1f, 2f);
        float rngY = Random.Range(-1f, 2f);
        Vector2 rngVector = new Vector2(rngX, rngY);
        fP.ShootProjectile(rngVector);
    }

    void Bounce() {
        float m = multiplier;
        m += 0.1f;
        if (m > 10) m = 10;
        rB.velocity = new Vector3(0, 0, 0);
        Vector2 dir = new Vector3(0, 0, 0);
        dir.y = Random.Range(-0.5f, 0.5f);
        dir.x = Random.Range(-0.5f, 0.5f);
        sH.Direction = (sH.Direction + dir) * -1 ;
        sH.Speed = sH.Speed + m;
        if (sH.Speed > 30) sH.Speed = 30;
        sH.Direction.Normalize();
        rB.velocity = sH.Direction * sH.Speed;
    }

    void Teleport() {
        if (Spawner == null) Spawner = GameObject.Find("BossSpawner");
        Vector2 teleportPoint = new Vector3(0, 0, 0);
        teleportPoint.x = Random.Range(-15, 15);
        transform.position = (Vector2)Spawner.transform.position + teleportPoint;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Boundry" || collision.gameObject.tag == "Player") Bounce();
    }

    IEnumerator TeleportCD() {
        tpCD = true;
        yield return new WaitForSeconds(5);
        tpCD = false;
    }
}
