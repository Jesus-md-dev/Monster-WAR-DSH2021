using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController _instance;

    private void Awake()
    {
        if (SoundController._instance == null)
        {
            //Nunca se ha lanzado
            SoundController._instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Ya se ha lanzado
            Destroy(gameObject);
        }
    }
}
