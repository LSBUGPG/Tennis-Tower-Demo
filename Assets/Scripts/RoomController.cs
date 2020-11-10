using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public static RoomController sticky;
    Transform cam, player;
    float offSet;

    Vector3 target;
    public float camSpeed;
    public List<Transform> rooms;

    [SerializeField]bool canTransition;

    void Start()
    {
        sticky = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main.transform;
        offSet = cam.transform.position.z;

        cam.transform.position = rooms[0].transform.position + new Vector3(0, 0, offSet);
        target = rooms[0].transform.position + new Vector3(0, 0, offSet);
    }


    void Update()
    {
        cam.position = Vector3.MoveTowards(cam.position, target + new Vector3(0, 0, offSet), camSpeed * Time.deltaTime);
    }

    public void CheckRoomDistance(Transform currentRoom)
    {
        float playerDist = 0;
        Transform closestRoom = null;

        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i] == currentRoom)
                continue;

            float roomDist = Vector3.Distance(rooms[i].position, player.position);

            if (playerDist == 0 || playerDist > roomDist)
            {

                playerDist = roomDist;
                closestRoom = rooms[i];
            }
        }

        if (closestRoom != null)
            SetRoomTarget(closestRoom.position);
    }

    public void CanTransition()
    {
        canTransition = true;
    }

    public void SetRoomTarget(Vector3 newTarget)
    {
        //print("AAAAH");
        //if (!canTransition)
        //    return;
        target = newTarget;
        canTransition = false;
    }
}
