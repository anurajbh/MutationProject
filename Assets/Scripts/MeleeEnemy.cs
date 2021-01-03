using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public Transform target;
    public float distanceThreshold;
    public float speed = 2f;
    private bool moveRight = true;

    public Transform rightDetection;

    public Transform leftDetection;

    public float distance;

    public float attackCooldown = 2f;
    public float attackTimer = 0f;

    public bool hasAttacked = false;

    public bool isTouching = false;

    public int _score;
    public bool facingRight = false;

    public Animator animator;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    private void Update()
    {
        if(CheckIfPlayerIsWithinDistance())
        {
            if (target.position.x > transform.position.x && !facingRight) //if the target is to the right of enemy and the enemy is not facing right
                Flip();
            if (target.position.x < transform.position.x && facingRight)
                Flip();
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
        animator.SetBool("IsRunning", true);
        animator.SetBool("IsFighting", false);
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(rightDetection.position, Vector2.down, distance);
        RaycastHit2D groundInfo2 = Physics2D.Raycast(leftDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false || groundInfo2.collider == false)
        {
            if (moveRight == true)
            {
                moveRight = false;
                transform.eulerAngles = new Vector3(0, -180, 0);

                Flip();
            }
            else
            {
                moveRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);

                Flip();
            }
        }
    }

    private void ChasePlayer()
    {
        if(!isTouching)
        {
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsFighting", false);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsRunning", false);
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
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsFighting", true);
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

    private void OnDestroy()
    {
        GameManager.Instance.AddScore(5);
    }
}
