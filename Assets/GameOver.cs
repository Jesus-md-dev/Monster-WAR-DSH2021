using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Text puntuation;
    // Start is called before the first frame update
    void Start()
    {
        puntuation.text = "Puntuation: " + 
            DataManager.LoadPlayerPuntuation().ToString();
    }
}
