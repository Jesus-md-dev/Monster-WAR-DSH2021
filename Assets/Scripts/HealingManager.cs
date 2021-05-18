using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject warrior;
    [SerializeField]
    private GameObject archer;
    [SerializeField]
    private GameObject mage;
    [SerializeField]
    private ToggleGroup charactersToggle;
    [SerializeField]
    private GameObject lightPot;
    [SerializeField]
    private GameObject mediumPot;
    [SerializeField]
    private GameObject greatPot;
    [SerializeField]
    private ToggleGroup potionsToggle;
    [SerializeField]
    private Text[] potionsText;
    [SerializeField]
    private Button healBtn;
    // Start is called before the first frame update
    Toggle[] charactersToggleArray;
    Toggle[] potionsToggleArray;
    void Start()
    {
        charactersToggleArray = charactersToggle
            .GetComponentsInChildren<Toggle>();
        potionsToggleArray = potionsToggle
            .GetComponentsInChildren<Toggle>();
        healBtn.onClick.AddListener(Heal);
    }

    // Update is called once per frame
    void Update()
    {
        if (lightPot.GetComponent<PotionManager>().n == 0)
            foreach (var toggle in potionsToggleArray)
                if (toggle.name == "LightPotionToggle")
                    toggle.interactable = false;
        if (mediumPot.GetComponent<PotionManager>().n == 0)
            foreach (var toggle in potionsToggleArray)
                if (toggle.name == "MediumPotionToggle")
                    toggle.interactable = false;
        if (greatPot.GetComponent<PotionManager>().n == 0)
            foreach (var toggle in potionsToggleArray)
                if (toggle.name == "GreatPotionToggle")
                    toggle.interactable = false;

        if(!warrior.GetComponent<CharacterManager>().bIsAlive)
            foreach (var toggle in charactersToggleArray)
                if (toggle.name == "WarriorToggle") 
                    toggle.interactable = false;
        if(!archer.GetComponent<CharacterManager>().bIsAlive)
            foreach (var toggle in charactersToggleArray)
                if (toggle.name == "ArcherToggle") 
                    toggle.interactable = false;
        if(!mage.GetComponent<CharacterManager>().bIsAlive)
            foreach (var toggle in charactersToggleArray)
                if (toggle.name == "MageToggle") 
                    toggle.interactable = false;
                    
        potionsText[0].text = "- " + lightPot.GetComponent<PotionManager>().n.ToString();
        potionsText[1].text = "- " + mediumPot.GetComponent<PotionManager>().n.ToString();
        potionsText[2].text = "- " + greatPot.GetComponent<PotionManager>().n.ToString();
    }

    Toggle GetActiveToggle(ToggleGroup toggleGroup)
    {
        Toggle[] toggleGroupArray = toggleGroup
            .GetComponentsInChildren<Toggle>();

        foreach (var toggle in toggleGroupArray)
            if (toggle.isOn && toggle.interactable) return toggle;
        
        return null;
    }

    void Heal()
    {
        GameObject character, potion;
        Toggle characterActiveToggle = GetActiveToggle(charactersToggle);
        Toggle potionActiveToggle = GetActiveToggle(potionsToggle);

        if (characterActiveToggle != null && potionActiveToggle != null)
        {
            switch (characterActiveToggle.name)
            {
                case "WarriorToggle":
                    character = warrior;
                    break;
                case "ArcherToggle":
                    character = archer;
                    break;
                case "MageToggle":
                    character = mage;
                    break;
                default:
                    character = null;
                    break;
            }

            switch (potionActiveToggle.name)
            {
                case "LightPotionToggle":
                    potion = lightPot;
                    break;
                case "MediumPotionToggle":
                    potion = mediumPot;
                    break;
                case "GreatPotionToggle":
                    potion = greatPot;
                    break;
                default:
                    potion = null;
                    break;
            }

            if (character != null && potion != null)
            {
                character.GetComponent<CharacterManager>()
                    .Healed(potion.GetComponent<PotionManager>().lifeHealed);

                potion.GetComponent<PotionManager>().usePotion();

                DataManager.SaveCharacter(character.GetComponent<CharacterManager>());
            }
        }
    }
}
