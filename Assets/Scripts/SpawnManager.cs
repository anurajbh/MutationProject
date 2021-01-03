using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private int _difficultyLevel;
    [SerializeField] private int _wavesPerDifficulty;
    [SerializeField] GameObject[] _waves;
    [SerializeField] private float _distanceBetweenSpawns;
    [SerializeField] private float _spawnInterval;

    public GameObject _previousWave;

    private GameObject _player;
    private float _playerPosX;

    private PlayerController _playerScript;

    public bool _spawnStarted;

    void Start()
    {
        _player = GameObject.Find("Player");
        _playerScript = _player.GetComponent<PlayerController>();
        _difficultyLevel = 1;
        SpawnWave(_difficultyLevel, _playerPosX + 10f);
        _spawnStarted = false;
    }

    void Update()
    {
        if (_spawnStarted == true)
        {
            if (_playerScript._distanceTraveledX >= _spawnInterval)
            {
                SpawnWave(_difficultyLevel, _previousWave.gameObject.transform.position.x + _distanceBetweenSpawns);
                _playerScript._distanceTraveledX = 0f;
            }
        }
        
    }

    private void SpawnWave(int difficultyLevel, float offset)
    {
        _playerPosX = _player.transform.position.x;
        int _waveNumber = Random.Range(0, (difficultyLevel * _wavesPerDifficulty) - 1);
        Vector2 wavePos = new Vector2(_waves[_waveNumber].transform.position.x + offset, _waves[_waveNumber].transform.position.y);
        GameObject newWave = Instantiate(_waves[_waveNumber], wavePos, Quaternion.identity);

        _previousWave = newWave;
    }
}
