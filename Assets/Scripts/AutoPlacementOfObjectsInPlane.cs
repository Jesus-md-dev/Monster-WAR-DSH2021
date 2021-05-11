using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class AutoPlacementOfObjectsInPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomePanel;
    [SerializeField]
    private GameObject[] placedPrefab;
    [SerializeField]
    private Button dismissButton;
    [SerializeField]
    private ARPlaneManager arPlaneManager;
    [SerializeField]
    private Text nObjectsText;
    private int nObjects = 0;
    private GameObject placedObject;

    private void Awake() 
    {
        dismissButton.onClick.AddListener(Dismiss);
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if(args.added != null && nObjects < 5)
        {
            ARPlane arPlane = args.added[0];
            placedObject = Instantiate(placedPrefab[Random.Range(0, 
                placedPrefab.Length)], arPlane.transform.position, 
                Quaternion.identity);
            placedObject.transform.localScale 
                = new Vector3(0.2f, 0.2f, 0.2f);
            ++nObjects;
            nObjectsText.text = nObjects.ToString();
        }
    }

    private void Dismiss() => welcomePanel.SetActive(false);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(welcomePanel.activeSelf)
            return;
    }
}
