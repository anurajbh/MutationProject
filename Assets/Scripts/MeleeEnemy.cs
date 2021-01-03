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

    public float attackCooldown = 2f;
    public float attackTimer = 0f;

    public bool hasAttacked = false;

    public bool isTouching = false;

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
        if(hasAttacked)
        {
            attackTimer += Time.deltaTime;
        }
        if(attackTimer>=attackCooldown)
        {
            hasAttacked = false;
            attackTimer = 0f;
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
        if(!isTouching)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!hasAttacked && collision.gameObject.tag=="Player")
        {
            isTouching = true;
            AttackPlayer();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!hasAttacked && collision.gameObject.tag == "Player")
        {
            isTouching = true;
            AttackPlayer();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouching = false;
    }

    private void AttackPlayer()
    {
        target.GetComponent<PlayerHealth>().HitPlayer();
        hasAttacked = true;
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
