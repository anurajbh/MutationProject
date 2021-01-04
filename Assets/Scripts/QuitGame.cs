using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuitGame : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(QuitTheGame);
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}