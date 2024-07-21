using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameTester : MonoBehaviour
{
    [SerializeField] private PlayerDataSave dataSave;
   public void SaveGame()
    {
        SaveGameManager.CurrentSaveData.PlayerData = dataSave.MyData;
        SaveGameManager.SaveGame();
    }
}
