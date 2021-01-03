using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 1f;
    [SerializeField] private string _enemyType;

    public void HitEnemy()

    {
        health--;
        if (health <= 0f)
        {
            switch (_enemyType)
            {
                case "Melee_Enemy":
                    GameManager.Instance.AddScore(5);
                    break;
                case "Randed_Enemy":
                    GameManager.Instance.AddScore(10);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
