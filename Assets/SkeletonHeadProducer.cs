using UnityEngine;
using System.Collections;

public class SkeletonHeadProducer : MonoBehaviour {

    [SerializeField] GameObject skeletonHead;
    [SerializeField] Vector2 direction;
    [SerializeField] float cd;
    bool cool = false;

    void Update() {
        if (!cool) {
            CreateHead();
            StartCoroutine(HeadCD());
        }
    }

	void CreateHead() {
        GameObject head = Instantiate(skeletonHead, transform.position, Quaternion.identity) as GameObject;
        SkeletonHead skeleHead = head.GetComponent<SkeletonHead>();
        skeleHead.Direction = direction;
    }

    IEnumerator HeadCD() {
        cool = true;
        yield return new WaitForSeconds(cd);
        cool = false;
    }
}
