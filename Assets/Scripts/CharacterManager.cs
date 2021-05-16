using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private string className;
    [SerializeField]
    private float maxLife = 100;
    public float actualLife;
    public bool bIsAlive = true;

    public float getActualLife()
    {
        return actualLife;
    }
    
    public string getClassName()
    {
        return className;
    }

    public bool isAlive()
    {
        return bIsAlive;
    }

    private void Awake() {
        actualLife = maxLife;
    }

    public void Damaged(float damage)
    {
        Debug.Log("Damage: " + damage);
        actualLife -= damage;
        if(actualLife <= 0)
        {
            bIsAlive = false;
            gameObject.GetComponent<Animator>().SetTrigger("Death");
        }
    }

    public void Healed(float heal)
    {
        if(actualLife > 0) actualLife += heal;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
