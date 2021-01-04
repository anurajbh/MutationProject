using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    int sceneIndex;
    Scene scene;
    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
        sceneIndex = scene.buildIndex;
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
