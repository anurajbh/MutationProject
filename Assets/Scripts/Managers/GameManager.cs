using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector] public GameObject _currentBackground;

    private void Start()
    {
        _currentBackground = GameObject.Find("Background");
    }
}
