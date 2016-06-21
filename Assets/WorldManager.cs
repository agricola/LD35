using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {

	[SerializeField] List<GameObject> rooms = new List<GameObject>();
    [SerializeField] int roomNumber = 5;
    [SerializeField] GameObject player;
    [SerializeField] GameObject finalRoom;
    [SerializeField] GameObject firstRoom;

    GameObject bossRoom;
    GameObject startRoom;

    public GameObject BossRoom { get { return bossRoom;} }
    public List<GameObject> worldRooms = new List<GameObject>();

    void Start() {
        GenerateRooms();
    }

    void GenerateRooms() {
        Vector3 pos = transform.position;
        startRoom = Instantiate(firstRoom, pos, Quaternion.identity) as GameObject;
        startRoom.transform.parent = transform;
        StartPosition start = startRoom.GetComponentInChildren<StartPosition>();
        player.transform.position = start.transform.position;

        for (int i = 0; i < roomNumber; i++) {
            pos += new Vector3(120, 0, 0);
            GameObject rngRoom = rooms[Random.Range(0, rooms.Count)];
            GameObject room = Instantiate(rngRoom, pos , Quaternion.identity) as GameObject;
            worldRooms.Add(room);
            rooms.Remove(rngRoom);
            room.transform.parent = transform;
        }

        pos.x += 120;
        bossRoom = Instantiate(finalRoom, pos, Quaternion.identity) as GameObject;
        bossRoom.transform.parent = transform;
    }
}
