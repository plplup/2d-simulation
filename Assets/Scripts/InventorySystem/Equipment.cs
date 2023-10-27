using UnityEngine;

public enum EquipmentType
{
    Hat,
    Shirt,
    Pants
}

[CreateAssetMenu(fileName = "New Equipment", menuName = "Items/Equipment")]
public class Equipment : ItemBase
{
    public EquipmentType Type;
    public bool IsEquipped;
}
