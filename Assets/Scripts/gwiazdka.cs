using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gwiazdka : MonoBehaviour
{

    public GameObject particles;
    public AudioClip gwiazdkasound;
    AudioSource source;

    void Start(){
        source = GetComponent<AudioSource>();
    }

     void dzwiek(AudioClip sound){
        source.clip = sound;
        source.pitch = 0.3f;
        source.volume = 0.11f;
        source.Play();
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.name != "Sphere")
        {
            return;
        }
        if(Krysztaly() ==1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            dzwiek(gwiazdkasound);
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
        }

        int Krysztaly()
        {
            gwiazdka[] gwiazdki = Component.FindObjectsOfType<gwiazdka>();
            return gwiazdki.Length;
        }
    }
}
