using System.Collections.Generic;
using UnityEngine;

public class InventoryUiPresenter : UiPresenterBase
{
    [Header("Reference Settings")]
    [SerializeField] private Transform inventorySlotTransformParent;
    [SerializeField] private InventorySlotUi inventorySlotUiPrefab;

    private List<InventorySlotUi> inventorySlotItems;

    public void Initialize(InventorySystem inventory)
    {
        CreateInventory(inventory);
    }

    private void CreateInventory(InventorySystem inventory)
    {
        if (inventory.InventorySlots.Length <= 0)
        {
            Debug.LogError("Inventory is empty");
            return;
        }

        inventorySlotItems = new List<InventorySlotUi>();

        for (int i = 0; i < inventory.InventorySlots.Length; i++)
        {
            var newSlotUi = Instantiate(inventorySlotUiPrefab, inventorySlotTransformParent);
            newSlotUi.itemImage.gameObject.SetActive(false);
            newSlotUi.stackSizeText.gameObject.SetActive(false);

            inventorySlotItems.Add(newSlotUi);
        }
    }

    public void UpdateInventory(InventorySystem inventory)
    {
        for (int i = 0; i < inventory.InventorySlots.Length; ++i)
        {
            int iCopy = i;
            inventorySlotItems[i].slotButon.onClick.RemoveAllListeners();

            if (inventory.InventorySlots[i].Item != null)
            {
                inventorySlotItems[i].itemImage.sprite = inventory.InventorySlots[i].Item.ItemIcon;
                inventorySlotItems[i].stackSizeText.text = inventory.InventorySlots[i].StackSize.ToString();
                inventorySlotItems[i].slotButon.onClick.AddListener(() => inventory.UseItem(inventory.InventorySlots[iCopy]));
            }

            inventorySlotItems[i].equippedIcon.gameObject.SetActive(inventory.InventorySlots[i].IsEquipped == true);
            inventorySlotItems[i].stackSizeText.gameObject.SetActive(inventory.InventorySlots[i].Item != null && inventory.InventorySlots[i].Item.MaxStackSize > 1);
            inventorySlotItems[i].itemImage.gameObject.SetActive(inventory.InventorySlots[i].Item != null);
        }
    }
}
