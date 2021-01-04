using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public GameObject _currentBackground;
    public int _score;
    public UIManager uIManager;

    private void Awake()
    {
        _score = 0;
        _currentBackground = GameObject.Find("Background");
        uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
    }

    public void AddScore(int score)
    {
        _score += score;
        uIManager.UpdateScoreText(_score);
    }
}
