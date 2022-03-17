using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Poziom : MonoBehaviour
{
    public TextMesh poziom;
    
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        string levelName = scene.name;
        poziom.text = levelName;
    }

}
