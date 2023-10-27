using UnityEngine;

public class UiPresenterBase : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    public virtual void Enable()
    {
        canvas.enabled = true;
    }

    public void Disable()
    {
        canvas.enabled = false;
    }
}
