using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    private SpawnManager _spawner;
    private Animator _animator;
    private bool _isdead = false;
    [SerializeField]
    private GameObject _explosion;
    private GameObject _playa;




    // Start is called before the first frame update
    void Start()
    {
        _spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _playa = GameObject.Find("Player");
        if (_playa != null)
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            _speed = 4.0f / Mathf.Pow(0.88f,(float)_player.level-1);
            _animator = gameObject.GetComponent<Animator>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.0f && _isdead==false)
        {
            transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 8.0f, 0);
        }
    }
    private void DestroyEnemy()
    {
        //_animator.SetTrigger("OnEnemyDeath");

        _isdead = true;
        Instantiate(_explosion, transform.position, Quaternion.identity);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 0.25f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            DestroyEnemy();
        }
        else if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.Score(10);
            }
            DestroyEnemy();
        }
        else if(other.CompareTag("Enemy"))
        {
            transform.position = new Vector3(Random.Range(-10.0f, 10.0f)* Random.Range(-0.2f, 0.2f), 8.0f, 0);
        }
        else if (other.CompareTag("Shield"))
        {
            if (_player != null)
            {
                _player.Score(5);
            }
            DestroyEnemy();
        }
    }
    public void LevelUp()
    {
        _speed = _speed / 0.66f;
    }
}
