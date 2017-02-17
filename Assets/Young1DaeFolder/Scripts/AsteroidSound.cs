using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSound : MonoBehaviour
{

    public AudioSource m_ExplosionAudio;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (gameObject.tag == "Fire" && other.tag == "Enemy")
        {
            m_ExplosionAudio.Play();

        }
    }
}
