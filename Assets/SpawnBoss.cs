using UnityEngine;
using System.Collections;

public class SpawnBoss : MonoBehaviour {
    [SerializeField] public GameObject boss;

    public void CreateBoss() {
        GameObject skeletonBoss = Instantiate(boss, transform.position, Quaternion.identity) as GameObject;
        skeletonBoss.transform.parent = transform.parent;
        skeletonBoss.GetComponent<Boss>().Spawner = this.gameObject;
    }
}
