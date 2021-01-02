using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform player;
    Vector2 target;
    public float bulletSpeed = 10f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = new Vector2(player.position.x, player.position.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, bulletSpeed * Time.deltaTime);
        if(transform.position.x== target.x && transform.position.y== target.y)
        {
            DestroyProjectile();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
    }
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
