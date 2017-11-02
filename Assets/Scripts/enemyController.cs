﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    public float startPosition;
    public float endPosition;
    public float timePerLap;
    public float dropTime;
    public float debugTime;

    private Vector2 P1;
    private Vector2 P2;
    private bool canAct;
    private Renderer rend;
    public float startTime = 0;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        P1 = new Vector2(startPosition, 4);
        P2 = new Vector2(endPosition, 4);
        canAct = false;
        Respawn();
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().gravityScale == 0 && rend.enabled == true)
        {
            float time = Time.timeSinceLevelLoad - startTime;
            debugTime = time;
            if ((time) > dropTime)
            {
                GetComponent<Rigidbody2D>().gravityScale = 2;
            }
        }
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }

        transform.position = Vector3.Lerp(P1, P2, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / timePerLap, 1f)));

        if (canAct)
        {
            rend.enabled = true;
        }else
        {
            rend.enabled = false;
        }
    }

    void OnTiggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void Respawn()
    {
        canAct = false;
        transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        StartCoroutine(sleep(timePerLap));
        
    }


    IEnumerator sleep(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAct = true;
        startTime = Time.timeSinceLevelLoad;
    }
}