using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Items/Generic Item")]
public class ItemBase : BaseScriptableObject
{
    public string DisplayName;
    public Sprite ItemIcon;
    public int MaxStackSize = 15;
    public bool Consumable;
    public int Price;

    public GameObject VisualPrefab;
    public Sprite VisualItemSprite;

    public string AnimatorControllerPath;
}
