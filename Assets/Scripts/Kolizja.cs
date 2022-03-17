using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kolizja : MonoBehaviour
{
    Scene scene;

    public GameObject bigexp;
    public GameObject text;
    public AudioClip boomsound;
    public AudioClip cios;

    AudioSource source;

    bool smierc = false;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        source = GetComponent<AudioSource>();
    }
    void dzwiek(AudioClip sound, float r1, float r2)
    {
        source.clip = sound;
        source.pitch = Random.Range(r1, r2);
        source.Play();
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Sphere")
        {
            dzwiek(cios,0.9f,1.1f);
            Sphere mysphereCript;
            GameObject kula = collision.gameObject;
            mysphereCript = kula.GetComponent<Sphere>();
            mysphereCript.health.Damage(1);
            mysphereCript.zmienKolor();
            mysphereCript.Skocz();
            if (mysphereCript.health.GetHealth() <= 0)
            {
                mysphereCript.Smierc();
            }
        }
    }
    void Update()
    {
        
    }


}
