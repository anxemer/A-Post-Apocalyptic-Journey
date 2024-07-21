using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "Inventory/Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int quantity;
}
