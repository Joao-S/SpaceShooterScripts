using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<AudioSource>().Play(0);
        Destroy(gameObject, 1.0f);
    }

    
}
