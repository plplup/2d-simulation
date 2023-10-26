using UnityEngine;

public class UiHandlerManager : MonoBehaviour
{
    public static UiHandlerManager Instance;

    [Header ("References Settings")]
    [SerializeField] private InventoryUiPresenter inventoryUiPresenter;
    [SerializeField] private StoreUiPresenter storeUiPresenter;

    private StateManager uiStateManager;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        uiStateManager = new StateManager();

        uiStateManager.AddState(new StoreUiState(inventoryUiPresenter));
    }

    public void OpenStore()
    {
        uiStateManager.GoToState<StoreUiState>();
    }
}
