using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForTime : MonoBehaviour
{
    int x;
    public WaitForTime(int x){
        this.x=x;
    }
    void start(){
        StartCoroutine(wait(x));
            }

    void Update(){}
    public IEnumerator wait(int x){
        yield return new WaitForSeconds(x);
    }
}