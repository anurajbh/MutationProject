using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _livesText;

    public void UpdateScoreText(int score)
    {
        _scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLivesText(float lives)
    {
        int livesInt = (int)lives;
        _scoreText.text = "Lives: " + livesInt.ToString();
    }
}
