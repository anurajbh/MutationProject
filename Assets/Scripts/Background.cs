using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    
    Vector2 _offset;
    Vector2 _previousPlayerPos;
    Transform _player;
    GameObject _newBackground;
    [SerializeField] float _backgroundWidth;
    [SerializeField] float _backgroundHeight;
    [HideInInspector] public GameObject _previousBackground;
    private static GameObject _currentBackground;
    GameManager gameManager;
    private void Awake()
    {
        _offset = new Vector2(_backgroundWidth/2, -_backgroundHeight/2);
    }


    void Start()
    {
        _player = GameObject.Find("Player").transform;
        _previousPlayerPos = _player.position;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        Vector2 _playerPos = _player.position;

        if (gameManager._currentBackground == this.gameObject)
        {
            if (_playerPos.x >= _previousPlayerPos.x + _offset.x)
            {
                Vector2 newPos = new Vector2(transform.position.x + _offset.x * 2, transform.position.y);

                _newBackground = Instantiate(this.gameObject, newPos, Quaternion.identity);

                gameManager._currentBackground = _newBackground;

                UpdateBackground(newPos);
            }
        }

        if (gameManager._currentBackground == this.gameObject)
        {
            if (_playerPos.y <= _previousPlayerPos.y + _offset.y)
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y * 2 + _offset.y);

                _newBackground = Instantiate(this.gameObject, newPos, Quaternion.identity);

                gameManager._currentBackground = _newBackground;

                UpdateBackground(newPos);
            }
        }

           
    }

    private void UpdateBackground(Vector2 newCameraPos)
    {
        

        Background backgroundScript = _newBackground.GetComponent<Background>();

        backgroundScript._previousBackground = this.gameObject;

        _previousPlayerPos = _player.position;

        if (_previousBackground != null)
        {
            Destroy(_previousBackground);
        }
    }

}
