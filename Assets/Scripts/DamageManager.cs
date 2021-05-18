using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [SerializeField]
    private float criticalDamage = 75;
    [SerializeField]
    private float standardDamage = 50;
    [SerializeField]
    private float minimumDamage = 25;
    public float BattleDamage(string humanClass, string monsterClass)
    {
        if(humanClass == monsterClass)
            return standardDamage;
        else if(humanClass == "Warrior")
        {
            if (monsterClass == "Mage") return minimumDamage;
            else return criticalDamage;
        }
        else if(humanClass == "Archer")
        {
            if (monsterClass == "Warrior") return minimumDamage;
            else return criticalDamage;
        }
        else if(humanClass == "Mage")
        {
            if (monsterClass == "Archer") return minimumDamage;
            else return criticalDamage;
        }
        else return 0;
    }
}
