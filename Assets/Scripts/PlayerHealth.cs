using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 3f;
    public float maxHealth = 3f;
    public float maxScale = 5f;
    public void HitPlayer()
    {
        health--;
        UIManager.Instance.UpdateLivesText(health);
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
    public void ScalePlayer(float scaleAmount)
    {
        if(gameObject.transform.localScale.x<=maxScale)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + scaleAmount, gameObject.transform.localScale.y + scaleAmount);
        }
    }
}
