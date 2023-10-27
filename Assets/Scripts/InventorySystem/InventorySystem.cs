using System;
using UnityEngine;


[Serializable]
public class InventorySlot
{
    public ItemBase Item;
    public int StackSize;
    //I didn't have time to create equipment slots
    public bool IsEquipped;
}

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int inventorySize;

    public InventorySlot[] InventorySlots;

    public void Initialize()
    {
        InventorySlots = new InventorySlot[inventorySize];

        for (int i = 0; i < inventorySize; i++)
        {
            InventorySlots[i] = new InventorySlot();
        }
    }

    public bool AddItem(ItemBase newItem, int amount = 1)
    {
        if (newItem.MaxStackSize <= 1)
        {
            for (int i = 0; i < InventorySlots.Length; ++i)
            {
                if (InventorySlots[i].Item != null) continue;

                InventorySlots[i].Item = newItem;

                UiHandlerManager.Instance.InventoryChanged(this);

                return true;
            }
        }

        var amountToAdd = amount;
        
        for (int i = 0; i < InventorySlots.Length; ++i)
        {
            if (InventorySlots[i].Item != newItem) continue;

            if (InventorySlots[i].StackSize >= newItem.MaxStackSize) break;

            int fitAmount = Mathf.Min(newItem.MaxStackSize - InventorySlots[i].StackSize, amountToAdd);
            InventorySlots[i].StackSize += fitAmount;

            UiHandlerManager.Instance.InventoryChanged(this);

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

            UiHandlerManager.Instance.InventoryChanged(this);

            if (amountToAdd == 0)
                return true;
        }

        return amountToAdd == 0;
    }

    public void RemoveItem(ItemBase item)
    {
        for (int i = 0; i < InventorySlots.Length; ++i)
        {
            if (InventorySlots[i].Item == item)
            {
                InventorySlots[i].Item = null;
                UiHandlerManager.Instance.InventoryChanged(this);
                break;
            }                
        }
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
        UiHandlerManager.Instance.InventoryChanged(this);
        return amount;
    }

    public void UnequipItemByType(EquipmentType equipType)
    {
        for (int i = 0; i < InventorySlots.Length; ++i)
        {
            if (InventorySlots[i].Item == null) continue;

            if (InventorySlots[i].IsEquipped == false) continue;

            if (InventorySlots[i].Item is Equipment == false) continue;

            if ((InventorySlots[i].Item as Equipment).Type != equipType) continue;

            InventorySlots[i].IsEquipped = false;
        }
    }

    public void UseItem(InventorySlot inventorySlot)
    {
        if (inventorySlot.Item == null)
            return;

        if (inventorySlot.IsEquipped == true)
        {
            inventorySlot.IsEquipped = false;
            GameManager.Instance.Player.UnequipItem(inventorySlot.Item as Equipment);
            UiHandlerManager.Instance.InventoryChanged(this);
            return;
        }

        if (inventorySlot.IsEquipped == false && inventorySlot.Item is Equipment == true)
        {
            UnequipItemByType((inventorySlot.Item as Equipment).Type);

            inventorySlot.IsEquipped = true;

            GameManager.Instance.Player.EquipItem(inventorySlot.Item as Equipment);

            UiHandlerManager.Instance.InventoryChanged(this);
            return;
        }
    }
}
