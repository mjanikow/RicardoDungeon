using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public AudioClip wystrzal;
    public AudioClip cios;
    public AudioClip sciana;

    Rigidbody rigidbody;




    AudioSource source;

    public GameObject tinyexp;
    float Timer;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        rigidbody = transform.GetComponent<Rigidbody>();

    }
    void dzwiek(AudioClip sound, float r1, float r2)
    {
        source.clip = sound;
        source.pitch = Random.Range(r1, r2);
        source.Play();
    }

    public void bum(GameObject boom)
    {
        GameObject jebut = Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(jebut, 2);
    }

    bool licz = false;
    bool mozna = true;
    bool smierc = false;
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == "Sphere")
        {
            Sphere mysphereCript;
            GameObject kula = col.gameObject;
            mysphereCript = kula.GetComponent<Sphere>();
            mysphereCript.health.Damage(1);
            if (mysphereCript.health.GetHealth() >0)
            {   
                mysphereCript.zmienKolor();
                dzwiek(cios,0.9f,1.1f);
                bum(tinyexp);
            }

            licz = true;
            if (mysphereCript.health.GetHealth() <= 0)
            {
                mysphereCript.Skocz();
                mysphereCript.Smierc();
            }

        }
        if (col.gameObject.tag == "gun")
        {
            dzwiek(wystrzal,0.7f,1.2f);
            bum(tinyexp);

        }
        else
        {
            licz = true;
        }

    }

    void Update()
    {
        if (licz)
            Timer += Time.deltaTime;
        if (Timer > 0.25f)
            Destroy(gameObject);


    }



}
