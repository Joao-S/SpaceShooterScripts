using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    public float fireRate = 0.5f;
    public float nextFire = 0.0f;
    public int lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private int _score;
    private UIManager _uiManager;
    private GameManager _gameManager;
    private bool _isShieldActive = false;
    public int level = 1;
    private int _lvlscore = 100;
    [SerializeField]
    private AudioSource _shootlaser;
    [SerializeField]
    private AudioSource _getpowerup;





    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is Null");
        }
    }


    void Update()
    {
        CalculateMovement();
        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > nextFire)
        {
            FireLaser();
        }
    }


    void CalculateMovement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 2.75f), 0);

        if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
        else if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        nextFire = Time.time + fireRate;

        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        _shootlaser.GetComponent<AudioSource>().Play(0);
    }

   public void Damage()
    {
        if (_isShieldActive == false)
        {
            lives--;
            _uiManager.UpdateLives(lives);
            if (lives == 2)
            {
                GameObject.Find("RightFire").GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(lives == 1)
            {
                GameObject.Find("LeftFire").GetComponent<SpriteRenderer>().enabled = true;
            }
            else if (lives == 0)
            {
                _spawnManager.OnPlayerDeath();
                _uiManager.GameOver();
                _gameManager.GameOver();
                gameObject.SetActive(false);
            }
        }
    }

    public void GetPowerUp(int _powerID)
    {
        switch(_powerID)
        {
            case 0:
                _isTripleShotActive = true;
                StartCoroutine(TripleShotPowerDown(5.0f));
                break;
            case 1:
                _speed=20.0f;
                fireRate = fireRate / 5;
                StartCoroutine(SpeedPowerDown(5.0f));
                break;
            case 2:
                var _shield = Instantiate(_shieldPrefab, transform.position, Quaternion.identity);
                _shield.transform.parent = transform;
                _isShieldActive = true;
                StartCoroutine(ShieldPowerDown(5.0f));
                break;
        }
        _getpowerup.GetComponent<AudioSource>().Play(0);
    }

    private IEnumerator TripleShotPowerDown(float _rate)
    {
        yield return new WaitForSeconds(_rate);
        _isTripleShotActive = false;
    }
    private IEnumerator SpeedPowerDown(float _rate)
    {
        yield return new WaitForSeconds(_rate);
        _speed=10.0f;
        fireRate = fireRate * 5;
    }
    private IEnumerator ShieldPowerDown(float _rate)
    {
        yield return new WaitForSeconds(_rate);
        _isShieldActive = false;
        Destroy(GameObject.Find("Shield(Clone)"));
    }

    public void Score(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
        if(_score >= _lvlscore)
        {
            _lvlscore = _score + 100;
            LevelUp();
        }
    }
    public void LevelUp()
    {
        level++;
        _spawnManager.LevelUp();
    }
}   
