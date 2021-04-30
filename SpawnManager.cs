using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float _spawnrate = 2.0f;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _speedPrefab;
    [SerializeField]
    private GameObject _shieldPUPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _asteroidPrefab;
    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(EnemySpawner(_spawnrate));
        StartCoroutine(AsteroidSpawner(_spawnrate*4.0f));
        StartCoroutine(PowerUpSpawner());
    }

    void Update()
    {

    }
    private IEnumerator AsteroidSpawner(float _rate)
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(_rate);
            GameObject _newEnemy = Instantiate(_asteroidPrefab, new Vector3(Random.Range(-13.0f, 8.0f), 8.0f, 0), Quaternion.identity);

        }
    }

    private IEnumerator EnemySpawner(float _rate)
    {
        while (_stopSpawning==false)
        {
            yield return new WaitForSeconds(_rate);
            GameObject _newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-11.0f, 11.0f), 8.0f, 0), Quaternion.identity);
            _newEnemy.transform.parent = _enemyContainer.transform;
     
        }
    }

    private IEnumerator PowerUpSpawner()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(3.0f,7.0f));
            int _powerID = Random.Range(0, 3);
            //int _powerID = 2;
            GameObject _newPowerUp;
            switch (_powerID)
            {
                case 0:
                    _newPowerUp = Instantiate(_tripleShotPrefab, new Vector3(Random.Range(-9.0f, 9.0f), 8.0f, 0), Quaternion.identity);
                    break;
                case 1:
                    _newPowerUp = Instantiate(_speedPrefab, new Vector3(Random.Range(-9.0f, 9.0f), 8.0f, 0), Quaternion.identity);
                    break;
                case 2:
                    _newPowerUp = Instantiate(_shieldPUPrefab, new Vector3(Random.Range(-9.0f, 9.0f), 8.0f, 0), Quaternion.identity);
                    break;
            }

        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    public void LevelUp()
    {
        _spawnrate = _spawnrate * 0.66f;
    }

}
