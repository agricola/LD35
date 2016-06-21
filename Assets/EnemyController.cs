using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    [SerializeField] float speed = 0.1f;
    [SerializeField] bool playerSighted = false;
    [SerializeField] bool isMoving = true;
    [SerializeField] float visionDistance = 10;

    bool giveUpPlayer = false;

    GameObject player;
    Vector2 targetDirection;
    FireProjectile fP;
    Rigidbody2D rB;
    Animator anim;

    void Start() {
        player = GameObject.Find("Character");
        targetDirection = new Vector2(-1, 0);

        fP = GetComponent<FireProjectile>();
        rB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (!player) {
            rB.Sleep();
            return;
        }
        if (isMoving) {
            if (playerSighted && !giveUpPlayer) {
                Move(new Vector2(0, 0));
                anim.SetBool("walking", false);
            } else {
                Move(targetDirection);
                anim.SetBool("walking", true);
            }
        }
        if (playerSighted) fP.ShootProjectile(FindDistanceToPlayer().normalized);

        if (CheckDistanceToPlayer(visionDistance)) playerSighted = true;
        else playerSighted = false;

        if (playerSighted) {
            if (FindDistanceToPlayer().normalized.x > 0) transform.localScale = new Vector2(-1, 1);
            else transform.localScale = new Vector2(1, 1);
        } else {
            if (targetDirection.x > 0) transform.localScale = new Vector2(-1, 1);
            else transform.localScale = new Vector2(1, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Boundry") {
            if (playerSighted) giveUpPlayer = true;
            else giveUpPlayer = false;
            targetDirection *= -1;
        }
    }

    void Move(Vector2 dir) {
        dir.Normalize();
        float movement = dir.x * speed;
        transform.Translate(movement, 0, 0);
    }

    Vector2 FindDistanceToPlayer() {
        Vector2 dis = player.transform.position - gameObject.transform.position;
        return dis;
    }

    bool CheckDistanceToPlayer(float d) {
        float distance;
        Vector2 dis = FindDistanceToPlayer();
        distance = dis.magnitude;

        if (distance < d) return true;
        else {
            isMoving = true;
            return false;
        }

    }
}
