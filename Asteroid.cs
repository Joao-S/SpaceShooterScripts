using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _speed=4.0f;
    [SerializeField]
    private GameObject _explosion;
    private GameObject _playa;
    private Player _player;
    void Start()
    {
        _playa = GameObject.Find("Player");
        if (_playa != null)
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,-1* _speed, 0) * Time.deltaTime);
        transform.Rotate(Vector3.forward * 20.0f*Time.deltaTime);
        if (transform.position.y < -4.0f || transform.position.x > 12.0f)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _speed = 0.0f;
            _player.Score(20);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject,0.25f);
              
        }
        else if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _speed = 0.0f;
            _player.Damage();
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.25f);
        }
        else if (other.CompareTag("Shield"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _speed = 0.0f;
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.25f);

        }
    }

}
