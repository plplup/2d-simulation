using System;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private int totalCoin = 0;
    public int TotalCoin
    {
        get { return totalCoin; }
        set
        {
            totalCoin = value;
            CoinChange?.Invoke();
        }
    }

    public Action CoinChange;

    public void AddCoin(int amount)
    {
        TotalCoin += amount;
    }

    public bool CanSpendCoin(int amount)
    {
        return TotalCoin >= amount;
    }

    public void SpendCoin(int amount)
    {
        TotalCoin = Mathf.Max(TotalCoin - amount, 0);
    }

}
