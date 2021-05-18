using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private string className;
    [SerializeField]
    private Slider lifeBar;
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

    private void Start() {
        actualLife = maxLife;
        
        if (lifeBar != null)
        {
            DataManager.LoadCharacter(this);
            lifeBar.value = actualLife / maxLife;
            if(!bIsAlive || actualLife <= 0)
                gameObject.GetComponent<Animator>().SetTrigger("Death");
        }    
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
        if(lifeBar != null)
            lifeBar.value = actualLife/maxLife;
    }

    public void Healed(float heal)
    {
        if (actualLife < maxLife && actualLife > 0)
        {
            actualLife += heal;
            if (actualLife > maxLife) actualLife = maxLife;
            lifeBar.value = actualLife / maxLife;
        }
    }
}
