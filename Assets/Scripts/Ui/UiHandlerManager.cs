using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UiHandlerManager : MonoBehaviour
{
    public static UiHandlerManager Instance;

    [Header ("References Settings")]
    [SerializeField] private InventoryUiPresenter inventoryUiPresenter;
    [SerializeField] private StoreUiPresenter storeUiPresenter;
    [SerializeField] private GameUiPresenter gameUiPresenter;
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject exiteGamePanel;

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

        GameManager.Instance.ToggleAllInput(false);

        fadeImage.gameObject.SetActive(true);

        fadeImage.DOFade(0, 1.5f).From(.95f).OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false);
            GameManager.Instance.ToggleAllInput(true);
        }).SetDelay(.5f);
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

    public void ShowExitGame()
    {
        GameManager.Instance.Player.ToggleInput(false);
        exiteGamePanel.gameObject.SetActive(true);
    }

    public void CancelExit()
    {
        GameManager.Instance.Player.ToggleInput(true);
        exiteGamePanel.gameObject.SetActive(false);
    }
}
