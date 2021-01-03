using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutation : MonoBehaviour
{
    PlayerHealth player;
    public float scaleAmount = 1f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.ScalePlayer(scaleAmount);
        Destroy(gameObject);
    }
}
