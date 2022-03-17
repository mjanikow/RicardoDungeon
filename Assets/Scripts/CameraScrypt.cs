using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrypt : MonoBehaviour
{
    public Transform sphere;  
    void Update()
    {
            try{
            Vector3 vector;
            Rigidbody rigidbody = sphere.GetComponent<Rigidbody>(); //pobranie komponentu fizyki z kuli
            if(Input.GetKey(KeyCode.Tab)){
                vector = new Vector3(30f,1.5f, 0f);
                if(transform.position.x>31)
                {
                    Vector3 finalPosition = transform.position;
                    finalPosition.x=32;
                    transform.position=finalPosition;
                }
            }
            else
            vector = new Vector3(6f,1.5f, 0f);
             
             float velocity = rigidbody.velocity.sqrMagnitude;  //pobranie predkosci 
             vector = vector * Max(1f + velocity/50); //przesuniecie pozycji powieksza sie wraz z predkoscia velocity
             Vector3 newPosition = sphere.position + vector; //ostateczna pozycja kamery, przesuniecie sphere o vector
             transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime*4f);
          //przypisanie pozycji kamery na pozycje kuli + przesuniecie o vector
        //z kazda klatka aktualna pozycja kamery bedzie przyblizac sie do newPosition
            transform.LookAt(sphere); //kamera ma patrzec na kule 
            }
            catch(MissingReferenceException){};      
    }
    

    float Max(float f){
        if(f>2f)
        return 2f;
        else 
        return f;
    }
}
