using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _livesText;
    public TextMeshProUGUI defeatPanelText;
    public void UpdateScoreText(int score)
    {
        _scoreText.text = "Score: " + score.ToString();
        defeatPanelText.text = "Your Score: " + score.ToString();
    }

    public void UpdateLivesText(float lives)
    {
        int livesInt = (int)lives;
        _livesText.text = "Lives: " + livesInt.ToString();
    }
}
