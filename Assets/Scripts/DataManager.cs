using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class DataManager 
{
    public static void DeleteData()
    {
        string path = Application.persistentDataPath;
        foreach(string file in Directory.GetFiles(path))
        {
            if (file.Contains(".data"))
                File.Delete(file);
        }
    }
    
    public static void SavePlayerPuntuation(float puntuation)
    {
        string path = Application.persistentDataPath + 
            "/PlayerPuntuation.data";
        File.WriteAllText(path, puntuation.ToString());
    }
    
    public static void SavePlayerHearts(int hearts)
    {
        string path = Application.persistentDataPath + 
            "/PlayerHearts.data";
        File.WriteAllText(path, hearts.ToString());
    }

    public static void SaveCharacter(CharacterManager character)
    {
        string path = Application.persistentDataPath + "/" + 
            character.getClassName() + ".data";
        File.WriteAllText(path, character.actualLife.ToString());
    }

    public static void SavePotion(GameObject potion, int n)
    {
        string path = Application.persistentDataPath + "/" + 
            potion.GetComponent<PotionManager>().type + ".data";
        File.WriteAllText(path, n
            .ToString());
    }

    public static float LoadPlayerPuntuation()
    {
        string path = Application.persistentDataPath +
            "/PlayerPuntuation.data";
        if (File.Exists(path))
        {
            string _playerPuntuation = File.ReadAllText(path);
            return float.Parse(_playerPuntuation);
        } else { Debug.Log(path + " doesn't exist"); }
        return 0;
    }

    public static void LoadPlayerHearts(PlayerManager player)
    {
        string path = Application.persistentDataPath +
            "/PlayerHearts.data";
        if (File.Exists(path))
        {
            string _playerHearts = File.ReadAllText(path);
            player.actualLife = int.Parse(_playerHearts);
        } else { Debug.Log(path + " doesn't exist"); }
    }

    public static void LoadCharacter(CharacterManager character)
    {
        string path = Application.persistentDataPath + "/" +
            character.getClassName() + ".data";
        if(File.Exists(path))
        {
            string _actualLife = File.ReadAllText(path);
            character.actualLife = float.Parse(_actualLife);
            if(character.actualLife > 0) character.bIsAlive = true;
            else character.bIsAlive = false;
        } else { Debug.Log(path + " doesn't exist"); }
    }

    public static int LoadPotions(GameObject potion)
    {
        string path = Application.persistentDataPath + "/" +
            potion.GetComponent<PotionManager>().type + ".data";
        if (File.Exists(path))
        {
            string _nPotions = File.ReadAllText(path);
            return int.Parse(_nPotions);
        }
        else { Debug.Log(path + " doesn't exist"); }
        return 0;
    }

}
