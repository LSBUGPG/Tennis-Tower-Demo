using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    private bool hasSpawned = false;
    public Ball ball;
    Ball aball;
    bool canServe = true;
    GameObject currentBall;
    public int index;
    public int smashLimit = 10;
    private bool smashFire;
    public Text smashText;

    public Vector2 spawnPosition;

    public float rotationSpeed = 5;

    public SliderAnimate sliderAnimate;
    public GameObject sliderParent;
    public Slider slider;
    public Canvas canvas;

    public float ServeFrequency = 2.0f;
    public GameObject curveball;
    float nextServeTime = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextServeTime = 0;
        smashFire = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Curveball cb = Instantiate(curveball, new Vector2(transform.position.x + spawnPosition.x, transform.position.y + spawnPosition.y), Quaternion.identity).GetComponent<Curveball>();
            //cb.Fired();
        }

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        canvas.transform.position = transform.position + new Vector3(-0.1f, 0, 0);
        if (aball != null && aball.BallServed() == false)
        {
            aball.transform.position = new Vector2(transform.position.x + spawnPosition.x, transform.position.y + spawnPosition.y);

        }

        float h = 0;

        if (Input.GetKey(KeyCode.K))
        {
            h = -1;
        }

        if (Input.GetKey(KeyCode.J))
        {
            h = 1;
        }

        if (h > 0 || h < 0)
        {
            transform.Rotate(0, 0, h * rotationSpeed);
        }

        bool pressedSpace = Input.GetKeyDown(KeyCode.Space);
        bool releasedSpace = Input.GetKeyUp(KeyCode.Space);
        if ((pressedSpace || releasedSpace) && Time.time > nextServeTime)
        {
            if (ServeBall("Forehand"))
            {
                aball.Serve(transform.up, "Forehand");
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time > nextServeTime)
        {
            if (ServeBall("Smash") && smashFire == false)
            {
                aball.Serve(transform.up, "Smash");
                smashLimit--;
            }

            if (smashLimit == 0)
            {
                smashFire = true;
            }

        }

        if (smashText != null)
        {
            smashText.text = "Smash: " + smashLimit.ToString();
        }
    }



    bool ServeBall(string serveType)
    {
        if (hasSpawned == false && canServe)
        {
            DeleteBall();
            if (serveType == "Forehand")
            {
                sliderAnimate.Animateslider();
                sliderParent.SetActive(true);
            }



            hasSpawned = true;
            return false;

        }

        else
        {

            //DeleteBall();
            aball = Instantiate(ball, new Vector2(transform.position.x + spawnPosition.x, transform.position.y + spawnPosition.y), Quaternion.identity);
            hasSpawned = true;
            Physics2D.IgnoreCollision(aball.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            canServe = false;
            nextServeTime = Time.time + ServeFrequency;
            return true;
        }

    }

    public float SliderValue()
    {
        return sliderAnimate.GetValue();
    }

    public void StopReferencingBall()
    {
        aball = null;
        canServe = true;
        hasSpawned = false;
        sliderAnimate.StopSlider();
        sliderParent.SetActive(false);
    }

    public void DeleteBall()
    {
        currentBall = GameObject.FindGameObjectWithTag("Ball");
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        //print("canServe "+ canServe);
        //print("hasSpawned " + hasSpawned);
    }

    void FixedUpdate()
    {
        //rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        rb.velocity = moveVelocity * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}
