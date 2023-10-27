using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoreItem
{
    public ItemBase Item;
    public int Amount = 1;
}

[CreateAssetMenu(fileName = "New Store", menuName = "Items/Store")]
public class Store : BaseScriptableObject
{
    public List<StoreItem> storeItems;
}
