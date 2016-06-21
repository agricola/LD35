using UnityEngine;
using System.Collections;

public class SkeletonHead : MonoBehaviour {

	[SerializeField] Vector2 direction;
    [SerializeField] float speed = 5;

    Rigidbody2D rB;

    public Vector2 Direction { get { return direction; } set { direction = value; } }
    public float Speed { get { return speed; } set { speed = value; } }

	void Start () {
        rB = GetComponent<Rigidbody2D>();
        rB.velocity = direction * speed;
        rB.AddTorque(5);
	}

}
