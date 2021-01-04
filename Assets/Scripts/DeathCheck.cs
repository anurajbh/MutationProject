using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
    ReloadScene reload;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Invoke("Restart", 0.5f);
        }

    }

    private void Awake()
    {
        reload = GetComponent<ReloadScene>();
    }
    private void Restart()
    {
        reload.LoadScene();
    }

}
