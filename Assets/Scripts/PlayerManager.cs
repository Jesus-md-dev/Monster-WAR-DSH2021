using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float playerPuntuation;
    [SerializeField]
    private DamageManager damageManager;
    [SerializeField]
    private float battleTime = 5;
    private ImageTracking imageTracking;
    private AutoPlaceMonsters autoPlaceMonsters;
    private Text playerPuntuationText;
    private GameObject[] hearts;
    private GameObject HeartContainer;
    private  GameObject human, monster;
    private GameObject arSessionOrigin;
    private Button battleButton;
    public bool inBattle = false;
    private float seconds = 0;
    public int actualLife = 0;
    private int maxLife = 3;

    // Start is called before the first frame update
    void Start()
    {
        arSessionOrigin = GameObject.FindWithTag("ARSOrigin");
        battleButton = GameObject.FindWithTag("BattleBtn")
            .GetComponent<Button>();
        HeartContainer = GameObject.FindWithTag("HeartContainer");
        imageTracking = arSessionOrigin.GetComponent<ImageTracking>();
        autoPlaceMonsters = arSessionOrigin
            .GetComponent<AutoPlaceMonsters>();
        playerPuntuationText = GameObject.FindWithTag("PlayerPuntuation")
            .GetComponent<Text>();
            
        playerPuntuation = DataManager.LoadPlayerPuntuation();
        actualLife = maxLife;
        DataManager.LoadPlayerHearts(this);
        if (actualLife <= 0)
            SceneManager.LoadScene("GameOver");

        hearts = new GameObject[HeartContainer.transform.childCount];

        for(int i = 0; i < HeartContainer.transform.childCount; ++i)
            hearts[i] = HeartContainer.transform.GetChild(i).gameObject;

        for (int i = actualLife; i < hearts.Length; ++i)
            hearts[i].SetActive(false);

        playerPuntuationText.text = "Puntuation: " + 
            playerPuntuation.ToString();
        
        battleButton.onClick.AddListener(StartBattle);
        Debug.Log("Hello World");
    }

    public void PlayerDamaged()
    {
        --actualLife;
        hearts[actualLife].SetActive(false);
        DataManager.SavePlayerHearts(actualLife);
        if (actualLife <= 0)   
            SceneManager.LoadScene("GameOver");
    }

    // Update is called once per frame
    void Update()
    {
        if(inBattle)
        {
            if (monster.GetComponent<Animator>()
            .GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                inBattle = false;
                Destroy(monster, 3.8f);
                autoPlaceMonsters.MonsterDie(monster);
            }
            else
            {
                human.transform.LookAt(monster.transform);
                monster.transform.LookAt(human.transform);
            }
        }
    }

    void StartBattle()
    {
        battleButton.interactable = false;
        Debug.Log("Battle");
        StartCoroutine(Battle());
    }

    IEnumerator Battle()
    {
        float timer = 0;
        float damage = 0;
        inBattle = true;
        while(timer < battleTime)
        {
            timer += Time.deltaTime;
            seconds = timer;
            human.GetComponent<Animator>().SetTrigger("Attack");
            monster.GetComponent<Animator>().SetTrigger("Attack");
            yield return null;
        }
        
        damage = damageManager.BattleDamage(
                human.GetComponent<CharacterManager>().getClassName(),
                monster.GetComponent<CharacterManager>().getClassName());
        human.GetComponent<CharacterManager>().Damaged(damage);
        if(!human.GetComponent<CharacterManager>().bIsAlive)
            PlayerDamaged();
        DataManager.SaveCharacter(human.GetComponent<CharacterManager>());
        Debug.Log("Battle Ended");
        monster.GetComponent<Animator>().SetTrigger("Death");
        playerPuntuation += 100 - damage;
        DataManager.SavePlayerPuntuation(playerPuntuation);
        playerPuntuationText.text = "Puntuation: " + 
            playerPuntuation.ToString();
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("TriggerEnter");
        if(other.tag == "Monster" && !inBattle)
        {
            Debug.Log("TriggerEnterMonster");
            human = imageTracking.getPlayerCharacter();
            if(human.GetComponent<CharacterManager>().isAlive())
            {
                monster = other.gameObject;
                battleButton.interactable = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("TriggerEnter");
        battleButton.interactable = false;
    }
}
