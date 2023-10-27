using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header ("Reference Settings")]
    [SerializeField] private CursorController cursorController;

    public PlayerController Player { get; private set; }

    private void Awake()
    {
        Instance = this;

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
}
