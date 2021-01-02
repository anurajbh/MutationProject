using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 3f;
    public void HitPlayer()
    {
        health--;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
