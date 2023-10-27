using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public abstract void Interact();

    private void Start()
    {
        gameObject.layer = Layers.Interactable;
    }
}
