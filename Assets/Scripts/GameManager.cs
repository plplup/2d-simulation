using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header ("Reference Settings")]
    [SerializeField] private CursorController cursorController;
    [SerializeField] private InputActionAsset inputActionsAsset;

    private InputAction escapeAction;

    public PlayerController Player { get; private set; }

    private void Awake()
    {
        Instance = this;

        escapeAction = inputActionsAsset.FindAction("InteractAction/Escape");
        escapeAction.Enable();

        escapeAction.performed += context => UiHandlerManager.Instance.ShowExitGame();

        Layers.Initialize();
    }

    private void Start()
    {
        UiHandlerManager.Instance.Initialize();
    }

    public void SetPlayerController(PlayerController player)
    {
        Player = player;
    }

    public void ToggleAllInput(bool isEnabled)
    {
        if (isEnabled == true)
        {
            Player.ToggleInput(true);
            cursorController.ToggleInput(true);
        }
        else
        {
            Player.ToggleInput(false);
            cursorController.ToggleInput(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
