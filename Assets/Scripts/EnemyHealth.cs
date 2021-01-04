using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 1f;
    [SerializeField] private string _enemyType;
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void HitEnemy()

    {
        health--;
        if (health <= 0f)
        {
            switch (_enemyType)
            {
                case "Melee_Enemy":
                    gameManager.AddScore(5);
                    break;
                case "Ranged_Enemy":
                    gameManager.AddScore(10);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
