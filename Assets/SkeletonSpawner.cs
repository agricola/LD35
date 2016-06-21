using UnityEngine;
using System.Collections;

public class SkeletonSpawner : MonoBehaviour {

	[SerializeField] GameObject skeletons;
    [SerializeField] GameObject currentSkeleton;
    [SerializeField] int cd = 30;
    bool wait = false;
    bool spawning = false;

    void Start() {
        currentSkeleton = SpawnSkeleton();
    }

    void Update() {
        if (currentSkeleton == null) {
            if (!spawning) StartCoroutine(SpawnCooldown());
            if (!wait) {
                currentSkeleton = SpawnSkeleton();
                spawning = false;
            }
        }
    }

    GameObject SpawnSkeleton() {
        GameObject skeleton = Instantiate(skeletons, transform.position, Quaternion.identity) as GameObject;
        return skeleton;
    }

    IEnumerator SpawnCooldown() {
        spawning = true;
        wait = true;
        yield return new WaitForSeconds(cd);
        wait = false;
    }
}
