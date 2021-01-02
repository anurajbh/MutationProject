using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public Transform target;
    public float distanceThreshold;
    public float speed = 2f;
    public Transform startingPos;
    private bool moveRight = true;

    public Transform groundDetection;

    public float distance;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }
    private void Update()
    {
        if(CheckIfPlayerIsWithinDistance())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

    }

    private void Patrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (moveRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveRight = true;
            }
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
    }

    private bool CheckIfPlayerIsWithinDistance()
    {
        if(Vector2.Distance(transform.position, target.position)<=distanceThreshold)
        {
            return true;
        }
        return false;
    }
}
