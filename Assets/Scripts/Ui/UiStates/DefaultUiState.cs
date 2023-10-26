using UnityEngine;

public class DefaultUiState : IStateUi
{
    private readonly GameUiPresenter gameUiPresenter;
    private readonly InventoryUiPresenter inventoryUiPresenter;

    public DefaultUiState(GameUiPresenter gameUiPresenter, InventoryUiPresenter inventoryUiPresenter)
    {
        this.gameUiPresenter = gameUiPresenter;
        this.inventoryUiPresenter = inventoryUiPresenter;
        gameUiPresenter.Initialize();
    }

    public void Enter()
    {
        gameUiPresenter.Enable();
        inventoryUiPresenter.Enable();
    }

    public void Exit()
    {
        gameUiPresenter.Disable();
        inventoryUiPresenter.Disable();
    }
}
