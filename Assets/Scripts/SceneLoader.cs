using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    public int buildIndex;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadTheScene);
    }

    private void LoadTheScene()
    {
        SceneManager.LoadScene(buildIndex);
    }

}
