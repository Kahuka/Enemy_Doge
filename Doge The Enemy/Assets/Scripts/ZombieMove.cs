using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class ZombieMove : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D myRigidbody;

    private bool moving;

    private bool chasing;
    public float lookRadius = 10f;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;

    private Vector3 moveDirection;

    public float waitToReload;
    private bool reloding;

    private GameObject thePlayer;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        //timeBetweenMoveCounter = timeBetweenMove;
        //timeToMoveCounter = timeToMove;

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
        chasing = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!chasing)
        {
            Debug.Log("Läks sisse");
            if (moving)
            {
                timeToMoveCounter -= Time.deltaTime;
                myRigidbody.velocity = moveDirection;
                if (timeToMoveCounter < 0f)
                {
                    moving = false;
                    //timeBetweenMoveCounter = timeBetweenMove;
                    timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
                }
            }
            else
            {
                timeBetweenMoveCounter -= Time.deltaTime;
                myRigidbody.velocity = Vector2.zero;

                if (timeBetweenMoveCounter < 0f)
                {
                    moving = true;
                    //timeToMoveCounter = timeToMove;
                    timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                    moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
                }
            }

            if (reloding)
            {
                waitToReload -= Time.deltaTime;
                if (waitToReload < 0f)
                {
                    SceneManager.LoadScene("Land");
                    thePlayer.SetActive(true);
                }
            }

        }


        if (Vector2.Distance(transform.position, target.position) < lookRadius) 
        {
            Debug.Log(chasing);
            chasing = true;
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {

            chasing = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player") 
        {
            //Destroy(other.gameObject);

            other.gameObject.SetActive(false);
            chasing = false;
            reloding = true;

            thePlayer = other.gameObject;

        }

    }

    //void OnTriggerEnter2D(Collider2D other)
    //{

    //    if (other.CompareTag("Player"))
    //    {
    //        other.gameObject.SetActive(false);

    //        reloding = true;

    //        thePlayer = other.gameObject;
    //    }
    //}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }


}
