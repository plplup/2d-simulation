using TMPro;
using UnityEngine;

public class GameUiPresenter : UiPresenterBase
{
    [Header("Reference Settings")]
    [SerializeField] private TMP_Text coinText;

    public void Initialize()
    {
        coinText.text = GameManager.Instance.Player.CoinController.TotalCoin.ToString();
        GameManager.Instance.Player.CoinController.CoinChange += OnCoinChange;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Player.CoinController.CoinChange -= OnCoinChange;
    }

    private void OnCoinChange()
    {
        coinText.text = GameManager.Instance.Player.CoinController.TotalCoin.ToString();
    }
}
