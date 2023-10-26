using UnityEngine;

public class ItemBase : BaseScriptableObject
{
    public string DisplayName;
    public Sprite ItemIcon;
    public int MaxStackSize = 15;
    public bool Consumable;
    public int Price;

    public GameObject VisualPrefab;

}
