using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;



[RequireComponent(typeof(ARTrackedImageManager))]
public class changeCharacter : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;
    public Button cambio;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    [SerializeField]
    public GameObject[] placeablePrefabs;
    private ARTrackedImageManager trackedImageManager;

    void Start()
    {
        cambio.onClick.AddListener(cartas);
    }

    private void Awake(){
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach(GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }

    private void cartas(){
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    void cambiarPersonaje(ARTrackedImage trackedimage){
        Debug.Log("fajsfiaj");
        
        string name = trackedimage.referenceImage.name;
        Vector3 position = trackedimage.transform.position;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        prefab.SetActive(true);
    


        foreach(GameObject go in spawnedPrefabs.Values)
        {
            if(go.name != name)
            {
                go.SetActive(false);
            }
        }
        
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        Debug.Log("holasa");
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            cambiarPersonaje(trackedImage);
        }
    }



    /*
    public Button cambio;
    private GameObject[] placeablePrefabs;
    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    
    public GameObject card;

    private void Awake(){
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach(GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            cambiarPersonaje(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.updated)
        {
            cambiarPersonaje(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }


    void cambiarPersonaje(){
        ARTrackedImage carta = FindObjectOfType<ARTrackedImage>();
        Vector3 position = carta.transform.position;

        GameObject prefab = card;
        prefab.transform.position = position;
        prefab.SetActive(true);
    
    }


    void Start()
    {
        //cambio.onClick.AddListener(cambiarPersonaje);
        cambiarPersonaje();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }*/
}
