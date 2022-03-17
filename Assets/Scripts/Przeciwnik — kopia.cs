using UnityEngine;
using System.Collections;

public class Przeciwnik2 : MonoBehaviour {

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
	
	void Start () {
	}

	void Update () {
		Timer += Time.deltaTime;
		dystans = Vector3.Distance (Gracz.position, transform.position) ;

		Debug.Log(dystans);
		if (dystans <= PoleWidzenia) 
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - Gracz.position), PredkoscObrotu * Time.deltaTime);

			if(Timer >= CoIleStrzelac)
			{
				Rigidbody Pclone = Instantiate(Pocisk, Lufa.position, Lufa.rotation) as Rigidbody;
				Pclone.velocity = -transform.forward * Wyrzut * Time.deltaTime;
				Timer = 2f;
			}
		}
	}
}
