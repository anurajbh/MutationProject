using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector] public GameObject _currentBackground;
    public int _score;


    private void Start()
    {
        _score = 0;
        _currentBackground = GameObject.Find("Background");
    }

    public void AddScore(int score)
    {
        _score += score;
        UIManager.Instance.UpdateScoreText(_score);
    }
}
