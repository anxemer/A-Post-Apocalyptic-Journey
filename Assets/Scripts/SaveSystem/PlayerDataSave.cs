using Assets.Scripts.SaveSystem;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSave : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private InventorySO inventoryItem;
    public PlayerData MyData = new PlayerData();

    private void Start()
    {
        // LoadData should be called here if needed
    }

    private void Update()
    {
        // Save data continuously can be problematic; consider saving at specific events
        MyData.playerPosition = transform.position;
        MyData.playerHealth = damageable.Health;
        SaveInventory();
    }

    public void SaveData()
    {
        SaveGameManager.CurrentSaveData.PlayerData = MyData;
        SaveGameManager.SaveGame();
    }

    public void LoadData()
    {
        StartCoroutine(LoadDataCoroutine());
    }

    private IEnumerator LoadDataCoroutine()
    {
        SaveGameManager.LoadGame();
        MyData = SaveGameManager.CurrentSaveData.PlayerData;

        yield return null; // Wait for one frame to ensure scene is fully loaded

        if (MyData != null)
        {
            transform.position = MyData.playerPosition;
            damageable.Health = MyData.playerHealth;
            LoadInventory();
        }
        else
        {
            Debug.LogWarning("Failed to load player data.");
        }
    }

    private void SaveInventory()
    {
        if (inventoryItem != null)
        {
            MyData.inventoryData.items = new List<InventoryItem>(inventoryItem.GetCurrentInventoryState().Values);
        }
    }

    private void LoadInventory()
    {
        if (inventoryItem != null && MyData.inventoryData.items != null)
        {
            inventoryItem.Initialize();
            foreach (var item in MyData.inventoryData.items)
            {
                inventoryItem.AddItem(item);
            }
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public InventoryData inventoryData;
    public Vector3 playerPosition;
    public int playerHealth;
    public InventorySO inventoryItem;

    public PlayerData()
    {
        inventoryData = new InventoryData();
    }
}
