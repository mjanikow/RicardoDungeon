using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sphere : MonoBehaviour
{
    public GameObject text; public GameObject tinyexp; public GameObject bigexp; public GameObject flary;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Image[] hearts;
    public AudioClip expSound1;
    public AudioClip expSound2;

    public AudioClip hitSound;


    AudioSource source;
    int warstwa = 0; int powierzchnia = 0; int dash = 0; int down = 0; int stop = 0;
    ParticleSystem particle;


    float lastClickTime;
    Vector3 kula;
    Scene scene;
    new Rigidbody rigidbody;
    public Collider coll;

    public Health health;


    void Start()
    {
        coll = GetComponent<Collider>();
        rigidbody = transform.GetComponent<Rigidbody>();   //pobieranie komponentu fizyki z kuli "Rigidbody" 
        scene = SceneManager.GetActiveScene();
        source = GetComponent<AudioSource>();
        health = new Health(5);


    }
    int smierci = 0;

    float thisTime = 0;
    bool czekanie = false;

    void Update()
    {
        kula = Vector3.zero;
        if (!smierc)
        {
            Sterowanie();
            ZmienWarstwe();
        }
        Upadek();
        czyUmarl(smierc);
        serca();
        Timer += Time.deltaTime;
        czekaj(czekanie);
    }


    bool czyTimer = true;
    void czekaj(bool czekanie)
    {
        if (czekanie)
        {
            if (czyTimer)
            {
                thisTime = Timer;
                czyTimer = false;
            }
            if (Timer - thisTime > 0.3f)
            {
                thisTime = 0;
                var cubeRenderer = transform.GetComponent<Renderer>();
                cubeRenderer.material.SetColor("_Color", Color.white);
                czekanie = false;
                czyTimer = true;
            }
        }
    }
    public void zmienKolor()
    {
        var cubeRenderer = transform.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.red);
        czekanie = true;
    }


    void serca()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < health.GetHealth())
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < 5)
                hearts[i].enabled = true;

            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void dzwiek(AudioClip sound)
    {
        source.clip = sound;
        source.pitch = Random.Range(0.8f, 1.5f);
        source.Play();
    }

    public void bum(GameObject boom)
    {
        GameObject jebut = Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(jebut, 2);
    }



    void Upadek()
    {
        if (rigidbody.position.y < -10f)
        {
            SceneManager.LoadScene(scene.path);
        }
    }
    bool czywarstwa = true;
    bool czyactive = false;
    void ZmienWarstwe()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            bool czyJest = Physics.Raycast(transform.position, Vector3.left * 2f, 2f);
            bool czyJest2 = Physics.Raycast(transform.position, Vector3.down * 2f, 4f);
            if (!czyJest)
            {
                if (!czywarstwa)
                {
                    dzwiek(expSound1);
                    warstwa = 0;
                    czywarstwa = !czywarstwa;
                    bum(tinyexp);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            bool czyJest = Physics.Raycast(transform.position, Vector3.right * 2f, 2f);
            bool czyJest2 = Physics.Raycast(transform.position, Vector3.down * 2f, 4f);
            if (!czyJest)
            {
                if (czywarstwa)
                {
                    dzwiek(expSound1);
                    warstwa = 1;
                    czywarstwa = !czywarstwa;
                    bum(tinyexp);
                }
            }
        }
        float delta = rigidbody.position.x - (warstwa * 2f);
        Vector3 predkosc = rigidbody.velocity;
        predkosc.x = -delta * 3f;
        rigidbody.velocity = predkosc;

    }

    int skok = 0;

    float Timer = 0;

    bool czySciana;
    bool czyzerowac = false;

    bool czydotknelo = false;
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "laser" && !smierc)
        {
            kula = Vector3.up;
            rigidbody.AddForce(kula * 100f);
            if (Random.Range(0f, 1f) > 0.5f)
            {
                rigidbody.AddForce(Vector3.forward * 50f);
            }
            else
            {
                rigidbody.AddForce(Vector3.back * 50f);
            }
        }

    }
    bool smierc = false;
    GameObject endText;

    float czas = 0;

    void czyUmarl(bool smierc)
    {
        if (smierc)
        {
            Vector3 pos = transform.position;
            if (smierci == 0)
            {
                endText = Instantiate(text, pos, Quaternion.Euler(new Vector3(0, -90, 0)));

            }
            czas += Time.deltaTime;
            pos.z += 6;
            endText.transform.position = pos;
            smierci++;
            if (czas > 1)
            {
                Time.timeScale = 0;
                if (Input.anyKeyDown)
                {
                    SceneManager.LoadScene(scene.path);
                }
            }
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void Smierc()
    {
        //rigidbody.AddExplosionForce(10f,transform.position,100f,1f,ForceMode.Impulse);
        dzwiek(expSound2);
        bum(bigexp);


        smierc = true;
    }
    public void Skocz()
    {
        rigidbody.velocity = Vector3.zero;
        powierzchnia = 0;
        dash = 0;
        down = 0;
        skok = 0;
        dzwiek(expSound1);
        kula = Vector3.up;
        rigidbody.AddForce(kula * 500f);
        if (Random.Range(0f, 1f) > 0.5f)
        {
            rigidbody.AddForce(Vector3.forward * 100f);
        }
        else
        {
            rigidbody.AddForce(Vector3.back * 100f);
        }
        bum(tinyexp);
        skok++;
    }
    void Sterowanie()
    {


        bool czySciana()
        {
            RaycastHit hit;
            Ray promien1 = new Ray(transform.position, Vector3.back);
            Ray promien2 = new Ray(transform.position, Vector3.forward);

            if (Physics.Raycast(promien1, out hit, 2f))
            {
                if (hit.collider.tag == "sciana")
                    return true;
                else return false;
            }
            if (Physics.Raycast(promien2, out hit, 2f))
            {
                if (hit.collider.tag == "sciana")
                    return true;
                else return false;
            }
            else return false;
        }

        bool czyJest()
        {
            if (skok == 1)
                return false;
            else
                return Physics.Raycast(transform.position, Vector3.down * 0.2f, 0.65F);
        }


        bool active()
        { //czy X jest aktywny
            if (Input.GetKey(KeyCode.X) && czyPowierzchnia())
                return true;
            else return false;
        }
        bool czyJestPodloga = Physics.Raycast(transform.position, Vector3.down * 0.2f, 0.65F);
        bool czyJestSufit = Physics.Raycast(transform.position, Vector3.up * 0.2f, 0.65F);
        bool czyJestBok = Physics.Raycast(transform.position, Vector3.back * 0.2f, 0.65F) || Physics.Raycast(transform.position, Vector3.forward * 0.2f, 0.65F);
        bool czyPowierzchnia()
        {
            return Physics.Raycast(transform.position, Vector3.back * 0.2f, 0.65F) || Physics.Raycast(transform.position, Vector3.forward * 0.2f, 0.65F) || Physics.Raycast(transform.position, Vector3.down * 0.2f, 0.65F) || Physics.Raycast(transform.position, Vector3.left * 0.2f, 0.65F) || Physics.Raycast(transform.position, Vector3.right * 0.2f, 0.65F) || Physics.Raycast(transform.position, Vector3.up * 0.2f, 0.65F);
        }

        //toczenie kuli
        if (Input.GetKey(KeyCode.LeftArrow) && (((czyJest() || czyJestSufit) && (!active() || czyJest())) || czyJestPodloga))
        {
            kula = Vector3.left;
            rigidbody.AddTorque(kula * 25f);
            // rigidbody.AddForce(Vector3.back * 7f);
        }
        if (Input.GetKey(KeyCode.RightArrow) && (((czyJest() || czyJestSufit) && (!active() || czyJest())) || czyJestPodloga))
        {
            kula = Vector3.right;
            rigidbody.AddTorque(kula * 25f);
            // rigidbody.AddForce(Vector3.forward * 7f);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && (czyJest() || czyJestSufit) && active() && czyJestSufit)
        {
            kula = Vector3.right;
            rigidbody.AddTorque(kula * 25f);
        }
        if (Input.GetKey(KeyCode.RightArrow) && (czyJest() || czyJestSufit) && active() && czyJestSufit)
        {
            kula = Vector3.left;
            rigidbody.AddTorque(kula * 25f);
        }


        if (Input.GetKeyDown(KeyCode.Space) && skok <= 1)
        {
            dzwiek(expSound1);
            kula = Vector3.up;
            rigidbody.AddForce(kula * 300f);
            bum(tinyexp);
            skok++;
        }

        if (czyzerowac && czyJestBok)
        {
            zeruj();
            czyzerowac = false; ;
        }

        if (czyJest())//czy dotyka powierzchni na dole
        {
            powierzchnia = 0;
            dash = 0;
            down = 0;
            skok = 0;
        }
        void zeruj()
        {//zeruje wartosci definiujace mozliwosc ponownego uzycia mocy
            powierzchnia = 0;
            dash = 0;
            down = 0;
            skok = 0;
        }




        if (Input.GetKey(KeyCode.X) && czyPowierzchnia() && !czySciana())//hamowanie/przylepianie
        {
            zeruj();
            Vector3 predkosc = rigidbody.velocity;
            rigidbody.AddForce(-5f * predkosc);
            if (!czyJest()) predkosc.y = 1f;
            if (!czyJest() && czyJestBok) predkosc.y = 0.2f;
            rigidbody.velocity = predkosc;
        }
        if (Input.GetKey(KeyCode.LeftControl) && !czyJest() && !active())//moc w dół
        {
            //dzwiek(expSound1);
            kula = Vector3.down;
            rigidbody.AddForce(kula * 15f);
            down++;
            bum(tinyexp);
        }
        //dash w lewo/prawo        
        if (Input.GetKeyDown(KeyCode.D) && !czyJest() && dash == 0 && !active())
        {
            dzwiek(expSound1);
            kula = Vector3.back * -1f;
            rigidbody.AddForce(kula * 250);
            dash++;
            bum(tinyexp);
        }
        if (Input.GetKeyDown(KeyCode.A) && !czyJest() && dash == 0 && !active())


        {
            dzwiek(expSound1);
            kula = Vector3.back;
            rigidbody.AddForce(kula * 250);
            dash++;
            bum(tinyexp);
        }

        if (Input.GetKey(KeyCode.RightArrow) && !czyJest() && !active() && (!czyPowierzchnia() || czyJestBok))
        {
            kula = Vector3.forward;
            rigidbody.AddForce(kula * 5f);
            GameObject jeb = Instantiate(flary, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
            Destroy(jeb, 0.1f);

        }
        if (Input.GetKey(KeyCode.LeftArrow) && !czyJest() && !active() && (!czyPowierzchnia() || czyJestBok))
        {
            kula = Vector3.back;
            rigidbody.AddForce(kula * 5f);
            GameObject jeb = Instantiate(flary, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            Destroy(jeb, 0.1f);

        }

    }


}

