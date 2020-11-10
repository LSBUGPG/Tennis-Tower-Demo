using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    Transform enemy;
    public float parrySpeed;
    Rigidbody2D rb;
    public Collider2D collider;
    bool incontactwithPlayer = false;
    Vector2 playerDistance;

    void Start()
    {
        enemy = FindObjectOfType<EnemyFire>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerDistance);
        bool parryDistance = (distance > 1.05f && distance < 1.2f);
        if (parryDistance && incontactwithPlayer)
        {
            CheckForParry();
        }
        else if (distance < 1.05f && incontactwithPlayer)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().ScoreUpdate();
            Destroy(gameObject);
        }

    }

    void CheckForParry()
    {
        if (Input.GetKey(KeyCode.G))
        {
            rb.velocity = Vector2.zero;
            Vector3 direction = (enemy.position - transform.position).normalized;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = direction * parrySpeed;
            gameObject.tag = "Ball";
            collider.enabled = false;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            incontactwithPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag == "EnemyBall")
        {
            other.gameObject.GetComponent<PlayerHealth>().ScoreUpdate();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player" && gameObject.tag == "EnemyParry")
        {
            incontactwithPlayer = true;
            playerDistance = other.gameObject.transform.position;
        }

        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
