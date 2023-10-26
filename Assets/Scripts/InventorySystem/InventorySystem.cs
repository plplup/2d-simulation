using System;
using UnityEngine;


[Serializable]
public class InventorySlot
{
    public ItemBase Item;
    public int StackSize;
}

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int inventorySize;


    public InventorySlot[] InventorySlots;


    public void Initialize()
    {
        InventorySlots = new InventorySlot[inventorySize];
    }

    public bool AddItem(ItemBase newItem, int amount = 1)
    {
        var amountToAdd = amount;
        
        for (int i = 0; i < InventorySlots.Length; ++i)
        {
            if (InventorySlots[i].Item != newItem) continue;

            if (InventorySlots[i].StackSize >= newItem.MaxStackSize) break;

            int fitAmount = Mathf.Min(newItem.MaxStackSize - InventorySlots[i].StackSize, amountToAdd);
            InventorySlots[i].StackSize += fitAmount;

            amountToAdd -= fitAmount;

            if (amountToAdd == 0)
                return true;
        }

        for (int i = 0; i < InventorySlots.Length; ++i)
        {
            if (InventorySlots[i].Item != null) continue;

            InventorySlots[i].Item = newItem;

            int fitAmount = Mathf.Min(newItem.MaxStackSize - InventorySlots[i].StackSize, amountToAdd);
            InventorySlots[i].StackSize = fitAmount;

            amountToAdd -= fitAmount;            

            if (amountToAdd == 0)
                return true;
        }

        return amountToAdd == 0;
    }

    public int Remove(int index, int count)
    {
        if (index < 0 || index >= InventorySlots.Length)
            return 0;

        int amount = Mathf.Min(count, InventorySlots[index].StackSize);

        InventorySlots[index].StackSize -= amount;

        if (InventorySlots[index].StackSize == 0)
        {
            InventorySlots[index].Item = null;
        }

        return amount;
    }

}
