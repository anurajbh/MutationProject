using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 1f;
    public void HitEnemy()
    {
        health--;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
