public class StoreUiState : IStateUi
{
    private readonly StoreUiPresenter storeUiPresenter;
    private readonly InventoryUiPresenter inventoryUiPresenter;

    public StoreUiState(StoreUiPresenter storeUiPresenter, InventoryUiPresenter inventoryUiPresenter)
    {
        this.storeUiPresenter = storeUiPresenter;
        this.inventoryUiPresenter = inventoryUiPresenter;
    }

    public void Enter()
    {        
        inventoryUiPresenter.Disable();
        storeUiPresenter.Enable();

        GameManager.Instance.Player.ToggleInput(false);
    }

    public void Exit()
    {
        inventoryUiPresenter.Enable();
        storeUiPresenter.Disable();

        GameManager.Instance.Player.ToggleInput(true);
    }
}
