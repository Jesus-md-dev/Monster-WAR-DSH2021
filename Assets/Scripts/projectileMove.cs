using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMove : MonoBehaviour
{
    public float speed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movDistance = Time.deltaTime * speed;
        transform.Translate(Vector3.forward * movDistance);
        Destroy(gameObject, 5.0f);
    }
}
