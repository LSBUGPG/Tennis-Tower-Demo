using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float startingSize = 0.5f;
    public float maximumSize = 1f;
    public float minimumSize = 0.2f;

    public float hitSize = 0.8f;
    public float hitTolerance = 0.1f;
    public float hitForce = 100f;
    public float[] hitSpeeds;
    public float smashSpeed = 750f;

    public float growthSpeed = 0.05f;
    private int growthDirection = 1;
    private Rigidbody2D rb;

    private bool serving = false;
    private bool served = false;
    private string serveType;

    PlayerController player;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector2(startingSize, startingSize);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        float x = (growthSpeed * growthDirection) + transform.localScale.x;
        float y = (growthSpeed * growthDirection) + transform.localScale.y;

        if (x > maximumSize)
        {
            growthDirection = -1;
        }

        if (x < minimumSize)
        {
            if (served == false)
            {
                //player.DeleteBall();
                player.StopReferencingBall();
            }
            growthDirection = 1;
        }

        transform.localScale = new Vector2(x,y);

        if (serving && serveType == "Forehand")
        {
            serving = false;
            //if (transform.localScale.x <= (hitSize + hitTolerance) && transform.localScale.x >= (hitSize - hitTolerance))
            //{
            //served = true;
            float sv = player.SliderValue();
            float power = CalculateSpeed(sv);
            print(power);
            rb.AddForce(direction * Time.deltaTime * power, ForceMode2D.Impulse);
            player.StopReferencingBall();
            //}
        }
        else if(serving && serveType == "Smash")
        {
            serving = false;
            rb.AddForce(direction * Time.deltaTime * smashSpeed, ForceMode2D.Impulse);
            player.StopReferencingBall();
        }



    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((serveType == "Smash" && col.gameObject.transform.tag == "HiddenWall"))
        {
            Destroy(col.gameObject);
        }
    }

    float CalculateSpeed(float v)
    {
        if ((v >= 0 && v < 0.2f))
        {
            return hitSpeeds[3];
        }

        if ((v > 0.8f && v < 1f))
        {
            return hitSpeeds[3];
        }

        if ((v > 0.2f && v < 0.354f))
        {
            return hitSpeeds[2];
        }

        if ((v > 0.644f && v < 0.8f))
        {
            return hitSpeeds[2];
        }

        if ((v > 0.354f && v < 0.457f))
        {
            return hitSpeeds[1];
        }

        if ((v > 0.54f && v < 0.644f))
        {
            return hitSpeeds[1];
        }

        if ((v > 0.457f && v < 0.54f))
        {
            return hitSpeeds[0];
        }

        return 0;
    }

    public bool BallServed()
    {
        return served;
    }

    public void Serve(Vector3 v, string s)
    {
        serving = true;
        direction = v;
        serveType = s;
    }
}
