using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private GameObject menu;
    [SerializeField] private List<InventoryItemData> inventoryItems = new List<InventoryItemData>();

    private bool isActive = false;
    public  List<InventoryItem> initialItems = new List<InventoryItem>();
    // Start is called before the first frame update
    void Start()
    {
        PreapareUI();
        PrepareInventoryData();

    }
    
    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        foreach (InventoryItem item in initialItems)
        {
            if (item.IsEmpty)
            {
                continue;
            }
            inventoryData.AddItem(item);
        }
    }
    public void AddItem(InventoryItemData item)
    {
        // Check if the item already exists in the inventory
        InventoryItemData existingItem = inventoryItems.Find(i => i.itemName == item.itemName);
        if (existingItem != null)
        {
            existingItem.quantity += item.quantity;
        }
        else
        {
            inventoryItems.Add(item);
        }
    }
    public void RemoveItem(InventoryItemData item)
    {
        inventoryItems.Remove(item);
    }   
    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.Image, item.Value.quantity);
        }
    }

    private void PreapareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        inventoryUI.OnSwapItems += HandleSwapItems;
        inventoryUI.OnStartDragging += HandleDragging;
        inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }
    private void HandleItemActionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            inventoryUI.ResetSelection();
            return;

        }
        //ItemSO item = inventoryItem.item;
        //inventoryUI.UpdateDescription(itemIndex,item.Image,item.name,item.Description);
        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {

            inventoryUI.ShowItemAction(itemIndex);
            inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
        }

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
        }

    }

    private void DropItem(int itemIndex, int quantity)
    {
        inventoryData.RemoveItem(itemIndex, quantity);
        inventoryUI.ResetSelection();
        //audioSource.PlayOneShot(dropClip);
    }

    public void PerformAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject, inventoryItem.itemState);
            //audioSource.PlayOneShot(itemAction.actionSFX);
            if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                inventoryUI.ResetSelection();
        }
    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        inventoryUI.CreateDraggedItem(inventoryItem.item.Image, inventoryItem.quantity);
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
        inventoryData.SwapItems(itemIndex_1, itemIndex_2);
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = inventoryItem.item;
        string description = PrepareDescription(inventoryItem);
        inventoryUI.UpdateDescription(itemIndex, item.Image,
            item.name, item.Description);
    }

    private string PrepareDescription(InventoryItem inventoryItem)
    {
        //StringBuilder sb = new StringBuilder();
        //sb.Append(inventoryItem.item.Description);
        //sb.AppendLine();
        //for (int i = 0; i < inventoryItem.itemState.Count; i++)
        //{
        //    sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} " +
        //        $": {inventoryItem.itemState[i].value} / " +
        //        $"{inventoryItem.item.DefaultParametersList[i].value}");
        //    sb.AppendLine();
        //}
        //return sb.ToString();
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key,
                        item.Value.item.Image,
                        item.Value.quantity);
                }
            }
            else
            {
                inventoryUI.Hide();
            }
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isActive)
            {
                menu.SetActive(true);
                isActive = true;
            }
            else
            {
                menu.SetActive(false);

                isActive = false;
            }
        }
    }
}
