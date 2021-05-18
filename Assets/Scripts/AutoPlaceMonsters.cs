using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class AutoPlaceMonsters : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placedPrefab;
    [SerializeField]
    private ARPlaneManager arPlaneManager;
    [SerializeField]
    private int spawnLimit = 5;
    [SerializeField]
    private float worldScale = 0.2f;
    [SerializeField]
    private GameObject[] potions;
    public int nMonsters = 0;
    private GameObject placedObject;

    private void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }

    public void MonsterDie(GameObject monster)
    {
        int iPotion = Random.Range(0, potions.Length + 1);
        --nMonsters;
        if(iPotion < potions.Length)
        {
            GameObject potion = Instantiate(potions[iPotion], 
            monster.transform.position, 
            Quaternion.Euler(new Vector3(-810, 0, 0)));
            float monsterLocalScale = monster.transform.localScale.x;
            potion.transform.localScale = new Vector3(
                monsterLocalScale * 8, monsterLocalScale * 8,
                monsterLocalScale * 8);
            potion.GetComponent<PotionManager>().addPotion();
            Destroy(potion, 4.5f);
        }
    }

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if(args.added != null && nMonsters < spawnLimit)
        {
            ARPlane arPlane = args.added[0];
            placedObject = Instantiate(placedPrefab[Random.Range(0, 
                placedPrefab.Length)], arPlane.transform.position, 
                Quaternion.identity);
            placedObject.transform.localScale 
                = new Vector3(placedObject.transform.localScale.x * 
                    worldScale, placedObject.transform.localScale.y * 
                    worldScale, placedObject.transform.localScale.z * 
                    worldScale);
            ++nMonsters;
        }
    }
}
