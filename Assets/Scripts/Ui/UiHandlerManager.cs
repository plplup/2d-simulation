using UnityEngine;

public class UiHandlerManager : MonoBehaviour
{
    public static UiHandlerManager Instance;

    [Header ("References Settings")]
    [SerializeField] private InventoryUiPresenter inventoryUiPresenter;
    [SerializeField] private StoreUiPresenter storeUiPresenter;
    [SerializeField] private GameUiPresenter gameUiPresenter;

    private StateManager uiStateManager;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        inventoryUiPresenter.Initialize(GameManager.Instance.Player.InventorySystem);

        uiStateManager = new StateManager();

        uiStateManager.AddState(new DefaultUiState(gameUiPresenter, inventoryUiPresenter));
        uiStateManager.AddState(new StoreUiState(storeUiPresenter, inventoryUiPresenter));

        uiStateManager.GoToState<DefaultUiState>();
    }

    public void OpenStore(Store store = null)
    {
        if (store == null)
        {
            Debug.LogError("Store settings is missing");
            return;
        }

        storeUiPresenter.SetStoreInfo(store);

        uiStateManager.GoToState<StoreUiState>();
    }

    public void ReturnDefaultUi()
    {
        uiStateManager.GoToState<DefaultUiState>();
    }

    public void InventoryChanged(InventorySystem inventory)
    {
        inventoryUiPresenter.UpdateInventory(inventory);
    }
}
