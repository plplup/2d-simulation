public class StoreUiState : IStateUi
{
    private readonly InventoryUiPresenter inventoryUiPresenter;

    public StoreUiState(InventoryUiPresenter inventoryUiPresenter)
    {
        this.inventoryUiPresenter = inventoryUiPresenter;
    }

    public void Enter()
    {
        inventoryUiPresenter.Disable();
    }

    public void Exit()
    {
        inventoryUiPresenter.Enable();
    }
}
