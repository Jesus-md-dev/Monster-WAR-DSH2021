using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public int n = 0;
    public float lifeHealed;
    public float rotateSpeed = 0.5f;
    public string type;
    
    // Start is called before the first frame update
    void Start()
    {
        n = DataManager.LoadPotions(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(n > 0) transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    public void addPotion()
    {
        DataManager.SavePotion(gameObject, DataManager.LoadPotions(gameObject) + 1);
    }

    public void usePotion()
    {
        --n;
        Debug.Log(n);
        DataManager.SavePotion(gameObject, n);
    }
}
