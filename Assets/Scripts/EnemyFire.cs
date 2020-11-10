using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] GameObject[] bullet;

   public float fireRate;
    float nextFire;

    private Transform player;
    public float ballSpeed;
    public Slider health;
    int parryFrequency = 2;
    int fired = 0;
    public static bool canFire = false;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 2f;
        nextFire = Time.time;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire == false)
        {
            return;
        }
        CheckIfTimeToFire();
    }
    

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            fired += 1;
            int enemyBall;
            if (fired == parryFrequency)
            {
                enemyBall = 1;
                fired = 0;
            }
            else
            {
                enemyBall = 0;
            }
            GameObject newBall = Instantiate(bullet[enemyBall], transform.position, Quaternion.identity);
            newBall.transform.SetParent(null);
            Vector3 direction = (player.position - transform.position).normalized;
            newBall.GetComponent<Rigidbody2D>().velocity = direction * ballSpeed;
           nextFire = Time.time + fireRate;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag == "Ball" || (other.tag == "EnemyParry" && other.gameObject.GetComponent<EnemyBall>().hitbyPlayer == true))
        if (other.tag == "Ball")
        {
            Destroy(other.gameObject);
            health.value += 1;
            if (health.value > 1)
            {
                //Destroy(gameObject, 0.5f);
                transform.position = new Vector3(999, 999, 999);
                canFire = false;
                DestroyAllBalls();
                Invoke("Reset", 5f);
            }
        }
    }

    void Reset()
    {
        transform.position = startPosition;
        canFire = true;
        health.value = 0;
    }

    void DestroyAllBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var item in balls)
        {
            Destroy(item);
        }

        balls = GameObject.FindGameObjectsWithTag("EnemyBall");
        foreach (var item in balls)
        {
            Destroy(item);
        }

        balls = GameObject.FindGameObjectsWithTag("EnemyParry");
        foreach (var item in balls)
        {
            Destroy(item);
        }
    }

}
