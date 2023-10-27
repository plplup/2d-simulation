using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
}
