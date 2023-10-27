using UnityEngine;

public static class Layers
{
    public static int Interactable;

    public static void Initialize()
    {
        Interactable = LayerMask.NameToLayer("Interactable");
    }
}
