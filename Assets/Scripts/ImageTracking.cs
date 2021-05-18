using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeablePrefabs;
    [SerializeField]
    private Button warriorButton;
    [SerializeField]
    private Button archerButton;
    [SerializeField]
    private Button mageButton;
    [SerializeField]
    private float worldScale = 0.2f;
    private int warrior, archer, mage;
    private Dictionary<int, GameObject> spawnedPrefabs 
        = new Dictionary<int, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    public int character = 0;

    // [SerializeField]
    // GameObject testCharacter;
    
    void changeToWarrior() { character = warrior; }
    void changeToArcher() { character = archer; }
    void changeToMage() { character = mage; }

    public GameObject getPlayerCharacter()
    {
        return spawnedPrefabs[character];
        // return testCharacter;
    }

    public Dictionary<int, GameObject> getCharacters()
    {
        return spawnedPrefabs;
    }

    private void Awake() {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        int i = 0;
        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, 
                Quaternion.identity);

            newPrefab.transform.localScale
            = new Vector3(newPrefab.transform.localScale.x * 
                worldScale, newPrefab.transform.localScale.y * 
                worldScale, newPrefab.transform.localScale.z * 
                worldScale);

            switch (newPrefab.GetComponent<CharacterManager>().getClassName())
            {
                case "Warrior":
                    Debug.Log("Warrior " + i);
                    warrior = i;
                    break;   
                case "Archer":
                    Debug.Log("Archer " + i);
                    archer = i;
                    break;   
                case "Mage":
                    Debug.Log("Mage " + i);
                    mage = i;
                    break;   
                default:
                    Debug.Log("Undefined Class");
                    break;
            }

            spawnedPrefabs.Add(i, newPrefab);
            ++i;
        }

        warriorButton.onClick.AddListener(changeToWarrior);
        archerButton.onClick.AddListener(changeToArcher);
        mageButton.onClick.AddListener(changeToMage);
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        Vector3 position = trackedImage.transform.position;

        GameObject prefab = spawnedPrefabs[character];
        prefab.transform.position = position;
        prefab.SetActive(true);

        for(int i = 0; i < placeablePrefabs.Length; ++i)
            if(i != character)
                spawnedPrefabs[i].SetActive(false);
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[character].SetActive(false);
        }   
    }

    private void OnEnable() {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable() {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }
}
