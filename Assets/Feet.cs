using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Feet : MonoBehaviour {

    [SerializeField] List<CircleCollider2D> feetCollider = new List<CircleCollider2D>();
    [SerializeField] CircleCollider2D collider0;
    [SerializeField] CircleCollider2D collider1;

    PlayerController pC;

    void Start() {
        pC = GetComponentInParent<PlayerController>();
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy") pC.Grounded = true;
    }

    public void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy") pC.Grounded = true;
    }

    public void OnCollisionExit2D(Collision2D collision) {
        pC.Grounded = false;
    }

    public void ChangeColliders(float offsetx0, float offsety0, float offsetx1, float offsety1) {
        collider0.offset = new Vector2(offsetx0, offsety0);
        collider0.offset = new Vector2(offsetx1, offsety1);
    }
}
