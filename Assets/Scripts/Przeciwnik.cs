using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Przeciwnik : MonoBehaviour
{

    public float PredkoscObrotu;
    public float PredkoscRuchu;
    public float PoleWidzenia;
    public float OdlegloscOdGracza;
    public float CoIleStrzelac;
    public float Wyrzut;
    public Transform Lufa;
    public Rigidbody Pocisk;
    public Transform Gracz;
    float dystans;
    float Timer;
    float Timer2;

    Rigidbody rigidbody;


    void Start()
    {


        rigidbody = transform.GetComponent<Rigidbody>();

    }

    bool czyMogeStrzelac;
    bool czyZnaleziony = false;

    bool czyNowaPozycja = true;

    bool czySzukam = false;


    Vector3 ostatniaPozycja;



    LinkedList<Vector3> pozycjeKuli = new LinkedList<Vector3>();



    void Update()
    {
        RaycastHit hit;
		Vector3 dir = (Gracz.position - transform.position).normalized;
        Ray promien = new Ray(transform.position, dir);


        Timer += Time.deltaTime;
        Timer2 += Time.deltaTime;
        dystans = Vector3.Distance(Gracz.position, transform.position);


        if (dystans <= PoleWidzenia && Physics.Raycast(promien, out hit, dystans) && hit.collider.name == "Sphere")
        {


            if (Physics.Raycast(promien, out hit, dystans))
            {
                if (hit.collider.name != "Sphere")
                {
                    czyMogeStrzelac = false;
                }
                else if (!czySzukam)
                {
                    if (czyNowaPozycja)
                    {
                        pozycjeKuli.Clear();
                        czyNowaPozycja = false;
                    }
                    pozycjeKuli.AddLast(Gracz.position);
                    if (pozycjeKuli.Count > 30)
                        pozycjeKuli.RemoveFirst();


                    czyZnaleziony = true;
                    czyMogeStrzelac = true;
                    ostatniaPozycja = pozycjeKuli.First.Value;
                }
            }
            if (czyZnaleziony && hit.collider.name != "Sphere")
            {
                czySzukam = true;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - ostatniaPozycja), PredkoscObrotu * Time.deltaTime);
                transform.position += -transform.forward * PredkoscRuchu * Time.deltaTime;
            }
            if (hit.collider.name == "Sphere" && czySzukam)
            {
                czySzukam = false;
            }


            if (hit.collider.name == "Sphere" && !czySzukam)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - Gracz.position), PredkoscObrotu * Time.deltaTime);

            if (dystans >= OdlegloscOdGracza && hit.collider.name == "Sphere" && !czySzukam)
            {
                transform.position += -transform.forward * PredkoscRuchu * Time.deltaTime;
            }

            if (Timer >= CoIleStrzelac && czyMogeStrzelac)
            {
                Rigidbody Pclone = Instantiate(Pocisk, Lufa.position, Lufa.rotation) as Rigidbody;
                Pclone.velocity = -transform.forward * Wyrzut * Time.deltaTime;
                Timer = 0f;
            }
        }
        else
        {

            bool czyJestPrzeszkoda = Physics.Raycast(transform.position, this.transform.forward * -2f, 2f);

            if (czyJestPrzeszkoda)
            {
                Quaternion kat = transform.rotation;
                float los = Random.Range(0, 180);
                kat *= Quaternion.Euler(Vector3.right * los);

                transform.rotation = Quaternion.Slerp(transform.rotation, kat, 1f * Time.deltaTime);
            }
            else
            {
                Quaternion kat = transform.rotation;
                kat *= Quaternion.Euler(Vector3.up * 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, kat, 1f * Time.deltaTime);

                transform.position += -transform.forward * PredkoscRuchu / 2 * Time.deltaTime;
            }

            if (transform.rotation.eulerAngles.y != 180)
            {
                if (transform.rotation.eulerAngles.y > 180)
                {
                    Quaternion kat = transform.rotation;
                    kat *= Quaternion.Euler(Vector3.up * -20);
                    transform.rotation = Quaternion.Slerp(transform.rotation, kat, 5f * Time.deltaTime);
                }
                else
                {
                    Quaternion kat = transform.rotation;
                    kat *= Quaternion.Euler(Vector3.up * 20);
                    transform.rotation = Quaternion.Slerp(transform.rotation, kat, 5f * Time.deltaTime);
                }
            }

            if (transform.rotation.eulerAngles.z != 0)
            {
                if (transform.rotation.eulerAngles.z > 180)
                {
                    Quaternion kat = transform.rotation;
                    kat *= Quaternion.Euler(Vector3.back * -20);
                    transform.rotation = Quaternion.Slerp(transform.rotation, kat, 5f * Time.deltaTime);
                }
                else
                {
                    Quaternion kat = transform.rotation;
                    kat *= Quaternion.Euler(Vector3.back * 20);
                    transform.rotation = Quaternion.Slerp(transform.rotation, kat, 5f * Time.deltaTime);
                }
            }

            //kat *= Quaternion.Euler(Vector3.back * 45);

            //right obrot osi x
            //back osi z


        }


        if (transform.position.x > 3)
        {
            float delta = rigidbody.position.x - 2f;
            Vector3 predkosc = rigidbody.velocity;
            predkosc.x = -delta;
            rigidbody.velocity = predkosc;
        }
    }
}
