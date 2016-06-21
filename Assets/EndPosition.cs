using UnityEngine;
using System.Collections;

public class EndPosition : MonoBehaviour {

    GameObject player;
    WorldManager world;
    Tiled2Unity.TiledMap room;
    GameObject audioSource;

    static int roomNumber = 0;

    void Start() {
        roomNumber = 0;
        world = GetComponentInParent<WorldManager>();
        room = GetComponentInParent<Tiled2Unity.TiledMap>();
        audioSource = GameObject.Find("Audio Source");
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            player = collision.gameObject;
            if (world) {
                NextRoom();
                audioSource.GetComponent<AudioManager>().PlaySound(Sound.Pickup);
            }
        }
    }

    void NextRoom() {
        if (!room) return;
        roomNumber += 1;
        int index = world.worldRooms.IndexOf(room.gameObject);
        StartPosition newStartPosition;
        if (world.worldRooms.Count != roomNumber) {
            GameObject nextRoom = world.worldRooms[index + 1];
            newStartPosition = nextRoom.GetComponentInChildren<StartPosition>();
        } else {
            newStartPosition = world.BossRoom.GetComponentInChildren<StartPosition>();
            world.BossRoom.GetComponentInChildren<SpawnBoss>().CreateBoss();
        }
        player.transform.position = newStartPosition.transform.position;
    }
}
