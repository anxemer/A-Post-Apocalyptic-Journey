using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameManager
{
    public static SaveData CurrentSaveData = new SaveData();
    public const string SaveDirectory = "/CurrentSaveData/";
    public const string FileName = "SaveGame.sav";

    public static bool SaveGame()
    {
        var dir = Application.persistentDataPath + SaveDirectory;
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
           
        }
        string json = JsonUtility.ToJson(CurrentSaveData, true);
        File.WriteAllText(dir + FileName, json);
        GUIUtility.systemCopyBuffer = json;
        return true;
    }
    public static void LoadGame()
    {
        string fullPath = Application.persistentDataPath+SaveDirectory + FileName;
        SaveData tempData = new SaveData();
        if(!File.Exists(fullPath)) { 
            Debug.LogError("Save File not exist");
        }
        string json = File.ReadAllText(fullPath);
        tempData = JsonUtility.FromJson<SaveData>(json);
        CurrentSaveData = tempData;

    }
}
