using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public GameObject door;

    PlayerController player;
    GameObject ball;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            ball = other.gameObject;
            if (door != null)
            {
                //Destroy(door);
                door.GetComponent<Door>().Remove();
                Invoke("NextServe", 2f);
            }

        }
    }

    void NextServe()
    {
        Destroy(ball);
        EnemyFire.canFire = true;
        //  player.DeleteBall();
    }
}
