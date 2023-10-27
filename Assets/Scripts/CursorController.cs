using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum CursorType
{
    Normal,
    Interact,
    System
}

public class CursorController : MonoBehaviour
{
    [Header("Reference Settings")]
    [SerializeField] private Texture2D NormalCursor;
    [SerializeField] private Texture2D InteractCursor;
    [SerializeField] private InputActionAsset inputActionAsset;

    private InputAction useItemAction;
    private Vector3 currentWorldMousePos;
    private InteractableObject currentInteractiveTarget = null;
    private Collider2D[] collidersCache = new Collider2D[8];
    private CursorType currentCursorType;

    private bool isOverUI = false;
    private bool canControl = true;
    private bool haveFocus = true;

    private void Awake()
    {
        useItemAction = inputActionAsset.FindAction("InteractAction/Use");
        useItemAction.Enable();

        useItemAction.performed += context => TryUseObject();
    }

    // Update is called once per frame
    private void Update()
    {
        isOverUI = EventSystem.current.IsPointerOverGameObject();
        currentInteractiveTarget = null;

        if (!canControl || isOverUI)
        {
            if (isOverUI) ChangeCursor(CursorType.Interact);
            return;
        }

        currentWorldMousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        var overlapCount = Physics2D.OverlapPointNonAlloc(currentWorldMousePos, collidersCache, 1 << Layers.Interactable);
        for (int i = 0; i < overlapCount; ++i)
        {
            var interactableObject = collidersCache[i].GetComponent<InteractableObject>();
            if (interactableObject != null)
            {
                currentInteractiveTarget = interactableObject;
                ChangeCursor(CursorType.Interact);
                return;
            }
        }

        ChangeCursor(CursorType.Normal);       
    }

    private void TryUseObject()
    {
        if (isOverUI == true)
            return;

        if (currentInteractiveTarget != null)
        {
            currentInteractiveTarget.Interact();
            return;
        }

    }

    private void ChangeCursor(CursorType cursorType)
    {
        if (haveFocus)
        {
            switch (cursorType)
            {
                case CursorType.Interact:
                    Cursor.SetCursor(InteractCursor, Vector2.one * 7, CursorMode.Auto);
                    break;
                case CursorType.Normal:
                    //hotspot position changed because brackeys cursor image is too small
                    Cursor.SetCursor(NormalCursor, Vector2.one * 7, CursorMode.Auto);
                    break;
                case CursorType.System:
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    break;
            }
        }

        currentCursorType = cursorType;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        haveFocus = hasFocus;
        if (!hasFocus)
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        else
            ChangeCursor(currentCursorType);
    }
}
