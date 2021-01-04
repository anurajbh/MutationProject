using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy1 : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    public GameObject bullet;
    public Transform player;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public float senseDistance = 10f;

    public Animator anim;

    public bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = startTimeBtwShots;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CheckToShoot();

    }

    private void CheckToShoot()
    {
        if (Vector2.Distance(transform.position, player.position) < senseDistance)
        {
            if (player.position.x > transform.position.x && !facingRight) //if the target is to the right of enemy and the enemy is not facing right
                Flip();
            if (player.position.x < transform.position.x && facingRight)
                Flip();
            CheckDistance();
            Shoot();
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    private void Shoot()
    {
        if (timeBtwShots < -0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void CheckDistance()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            anim.SetBool("IsRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2 (player.position.x, transform.position.y), speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            anim.SetBool("IsRunning", false);
            transform.position = transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            anim.SetBool("IsRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), -speed * Time.deltaTime);
        }
    }
}
