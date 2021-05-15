using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class UIManager : MonoBehaviour
{
    ARTrackedImageManager m_TrackedImageManager;
    public GameObject heart;
    public List<Image> hearts;
    PlayerHearts playerHearts;
    // Start is called before the first frame update
    void Start()
    {
        
        // playerHearts = PlayerHearts.instance;
        for(int i = 0; i < playerHearts.maxHealth; ++i)
        {
            GameObject h = Instantiate(heart, this.transform);
            hearts.Add(h.GetComponent<Image>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
