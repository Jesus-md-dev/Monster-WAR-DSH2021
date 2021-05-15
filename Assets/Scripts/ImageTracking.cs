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
    private Button changeButton;
    [SerializeField]
    private Text text;
    private Dictionary<int, GameObject> spawnedPrefabs = new Dictionary<int, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    private int character = 0;



    [SerializeField]
    GameObject testCharacter;

    void Start()
    {
        changeButton.onClick.AddListener(changeCharacter);
        text.text = character.ToString();
    }
    
    void changeCharacter()
    {
        character = (character + 1) % placeablePrefabs.Length;
        text.text = character.ToString() + ":" + spawnedPrefabs[character].name;
    }

    public GameObject getPlayerCharacter()
    {
        // return spawnedPrefabs[character];
        return testCharacter;
    }

    private void Awake() {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        int i = 0;
        foreach (GameObject prefab in placeablePrefabs)
        {
                GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                spawnedPrefabs.Add(i, newPrefab);
                ++i;
        }
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
