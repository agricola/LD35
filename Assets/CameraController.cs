using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField] Transform player;

    float z;

    void Awake() {
        z = transform.position.z;
    }

    void Update() {
        if (!player) return;
        Vector3 pos = player.position;
        transform.position = new Vector3(pos.x, pos.y, z);
    }

}
