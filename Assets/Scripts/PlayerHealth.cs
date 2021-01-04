using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public UIManager uIManager;
    public float health = 3f;
    public float maxHealth = 3f;
    public float maxScale = 5f;
    public float startingScale = 1f;
    public GameObject defeatPanel;
    public void HitPlayer()
    {
        health--;
        uIManager.UpdateLivesText(health);
        if (health <= 0f)
        {
            InitiateLosingSequence();
        }
    }
    private void Awake()
    {
        uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        gameObject.transform.localScale = new Vector2(startingScale, startingScale);
        health = 3f;
    }

    private void InitiateLosingSequence()
    {
        defeatPanel.SetActive(true);
        Destroy(gameObject);
    }

    public void ScalePlayer(float scaleAmount)
    {
        if(gameObject.transform.localScale.x<=maxScale)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + scaleAmount, gameObject.transform.localScale.y + scaleAmount);
        }
    }
}
