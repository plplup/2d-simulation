using UnityEngine;

public class SellerWizard : InteractableObject
{
    [SerializeField] private Store store;
    public override void Interact()
    {
        UiHandlerManager.Instance.OpenStore(store);
    }
}
