using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] TextMeshProUGUI _scoreText;
    public void UpdateScoreText(int score)
    {
        _scoreText.text = "Score: " + score.ToString();
    }
}
