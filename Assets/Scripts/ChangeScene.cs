using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ChangeToScene);
    }

    void ChangeToScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
