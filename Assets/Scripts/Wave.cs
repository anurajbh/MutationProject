using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float _minOffsetY;
    [SerializeField] private float _maxOffsetY;
    private GameObject _mainCamera;
    private GameObject _player;
    private PlayerController _playerScript;
    private float _distFromCamCenter;

    private void Start()
    {
        transform.position = new Vector2(transform.position.x, Random.Range(_minOffsetY, _maxOffsetY));
        _mainCamera = GameObject.Find("Main Camera").gameObject;
        _player = GameObject.Find("Player").gameObject;

        if (_player != null)
            _playerScript = _player.GetComponent<PlayerController>();
        else
            Debug.Log("PlayerController script is NULL");

    }

    private void Update()
    {
        _distFromCamCenter = transform.position.x - _mainCamera.transform.position.x;

        if (_distFromCamCenter > _playerScript._maxReturnDistance + 50f)
        {
            if (SpawnManager.Instance._previousWave.gameObject != this.gameObject)
                Destroy(this.gameObject);
        }
    }
}
