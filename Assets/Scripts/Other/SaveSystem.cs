using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static SaveData dataFile;

    public static void SaveGame()
    {
        if (dataFile == null)
            dataFile = new SaveData();

        SaveDataToFile();
        PlayerPrefs.SetString("savedData", JsonUtility.ToJson(dataFile));

        Debug.Log("Game Saved");
    }

    public static void LoadGame()
    {
        LoadPreferences();

        if (dataFile == null)
            dataFile = new SaveData();

        if (PlayerPrefs.HasKey("savedData"))
        {
            dataFile = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("savedData"));
            LoadDataFromFile();
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No data to Load");
        }

    }

    public static void SavePreferences()
    {
        if (GameManager.soundsOn)
            PlayerPrefs.SetInt("soundOn", 1);
        else
            PlayerPrefs.SetInt("soundOn", 0);

        if (GameManager.musicOn)
            PlayerPrefs.SetInt("musicOn", 1);
        else
            PlayerPrefs.SetInt("musicOn", 0);

        Debug.Log("Preferences Saved");
    }

    public static void LoadPreferences()
    {
        if(PlayerPrefs.HasKey("soundOn"))
        {
            if (PlayerPrefs.GetInt("soundOn") == 1)
                GameManager.soundsOn = true;
            else
                GameManager.soundsOn = false;
        }

        if(PlayerPrefs.HasKey("musicOn"))
        {
            if (PlayerPrefs.GetInt("musicOn") == 1)
                GameManager.musicOn = true;
            else
                GameManager.musicOn = false;
        }
            
    }

    public static void EraseData()
    {
        if (dataFile == null)
            dataFile = new SaveData();

        EraseDataFromFile();
    }

    public static void SaveDataToFile()
    {
        dataFile.endingPlayed = GameManager.endingPlayed;
        dataFile.currentGameStage = GameManager.currentGameStage;

        //dataFile.upgradesPurchased.Clear();
        //dataFile.upgradesPurchased.AddRange(GameManager.upgradesPurchased);
        dataFile.upgradesPurchasedId.Clear();
        dataFile.upgradesPurchasedId.AddRange(GameManager.upgradesPurchasedId);

        dataFile.vaccineStored = LabManager.vaccineStored;
        dataFile.scienceStored = LabManager.scienceStored;
        dataFile.antigenStored = LabManager.antigenStored;
        dataFile.dataStored = LabManager.dataStored;
        dataFile.vaccineInRocket = LabManager.vaccineInRocket;

        for (int i = 0; i < LabManager.componentStored.Length; i++)
        {
            dataFile.componentStored[i] = LabManager.componentStored[i];
            dataFile.componentcurrentTarget[i] = LabManager.componentcurrentTarget[i];

        }

    }

    public static void LoadDataFromFile()
    {
        GameManager.endingPlayed = dataFile.endingPlayed;
        GameManager.currentGameStage = dataFile.currentGameStage;
        //GameManager.upgradesPurchased.Clear();
        //GameManager.upgradesPurchased.AddRange(dataFile.upgradesPurchased);
        GameManager.upgradesPurchasedId.Clear();
        GameManager.upgradesPurchasedId.AddRange(dataFile.upgradesPurchasedId);

        LabManager.vaccineStored = dataFile.vaccineStored;
        LabManager.scienceStored = dataFile.scienceStored;
        LabManager.antigenStored = dataFile.antigenStored;
        LabManager.dataStored = dataFile.dataStored;
        LabManager.vaccineInRocket = dataFile.vaccineInRocket;

        for (int i = 0; i < LabManager.componentStored.Length; i++)
        {
            LabManager.componentStored[i] = dataFile.componentStored[i];
            LabManager.componentcurrentTarget[i] = dataFile.componentcurrentTarget[i];

        }

    }

    public static void EraseDataFromFile()
    {
        dataFile.endingPlayed = false;
        dataFile.currentGameStage = 0;
        //dataFile.upgradesPurchased.Clear();
        dataFile.upgradesPurchasedId.Clear();

        dataFile.vaccineStored = 0;
        dataFile.scienceStored = 0;
        dataFile.antigenStored = 0;
        dataFile.dataStored = 0;
        dataFile.vaccineInRocket = 0;

        for (int i = 0; i < LabManager.componentStored.Length; i++)
        {
            dataFile.componentStored[i] = 0;
            dataFile.componentcurrentTarget[i] = 0;

        }

        LoadDataFromFile();

        PlayerPrefs.DeleteKey("savedData");
    }
}
