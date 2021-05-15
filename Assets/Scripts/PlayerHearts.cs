using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHearts : MonoBehaviour
{
    public int maxHealth;
    // public PlayerHearts instance;
    public int health;
    
    // private void Awake() 
    // {
    //     if (instance == null)
    //         instance = this;
    // }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        if(health > 0)
            health -= 1;
    }
}
