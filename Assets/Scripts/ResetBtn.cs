using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetBtn : MonoBehaviour
{
    [SerializeField]
    private Text resetText;

    // Start is called before the first frame update
    void Start()
    {
        resetText.gameObject.SetActive(false);
        gameObject.GetComponent<Button>().onClick.AddListener(Restart);
    }

    void Restart()
    {
        resetText.gameObject.SetActive(true);
        DataManager.DeleteData();
    }
}
