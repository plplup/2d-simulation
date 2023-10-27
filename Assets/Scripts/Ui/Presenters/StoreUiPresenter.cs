using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUiPresenter : UiPresenterBase
{
    [Header("Reference Settings")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Transform mainPanel;
    [SerializeField] private Transform itemBuyTransformParent;
    [SerializeField] private ItemBuyUi itemBuyUiPrefab;
    [SerializeField] private ItemSellUi itemSellUiPrefab;
    [SerializeField] private TMP_Text totalCoinText;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button buyTabButton;
    [SerializeField] private Button sellTabButton;

    [Header("Store Panel Tween Variables")]
    [SerializeField] private float scaleDuration = .5f;
    [SerializeField] private float fadeDuration = .4f;
    [SerializeField] private Ease scaleEase;

    private Store currentStore = null;

    private delegate void RefreshItems();
    private RefreshItems refreshItemsCallback;

    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
        buyTabButton.onClick.AddListener(OnBuyTabButtonClick);
        sellTabButton.onClick.AddListener(OnSellTabButtonClick);
    }

    private void OnCloseButtonClick()
    {
        refreshItemsCallback = null;
        UiHandlerManager.Instance.ReturnDefaultUi();
    }

    private void OnBuyTabButtonClick()
    {
        PopulateStore();
        buyTabButton.interactable = false;
        sellTabButton.interactable = true;
    }

    private void OnSellTabButtonClick()
    {
        PopulateStore(true);
        buyTabButton.interactable = true;
        sellTabButton.interactable = false;
    }

    public void SetStoreInfo(Store store)
    {
        currentStore = store;
    }

    public override void Enable()
    {
        base.Enable();

        DOTween.Sequence()
            .Append(backgroundImage.DOFade(0.7f, fadeDuration).From(0))
            .Insert(fadeDuration / 2, mainPanel.DOScale(Vector3.one, scaleDuration).From(0.2f).SetEase(scaleEase));


        buyTabButton.interactable = false;
        sellTabButton.interactable = true;

        totalCoinText.text = GameManager.Instance.Player.CoinController.TotalCoin.ToString();
        PopulateStore();
    }

    private void PopulateStore(bool isSellStore = false)
    {
        refreshItemsCallback = null;

        foreach (Transform childTransform in itemBuyTransformParent)
        {
            Destroy(childTransform.gameObject);
        }

        if (isSellStore == false)
        {
            foreach (var storeItem in currentStore.storeItems)
            {
                var itemBuyUi = Instantiate(itemBuyUiPrefab, itemBuyTransformParent);
                itemBuyUi.IconImage.sprite = storeItem.Item.ItemIcon;
                itemBuyUi.NameText.text = storeItem.Item.DisplayName;
                itemBuyUi.PriceText.text = storeItem.Item.Price.ToString();

                if (GameManager.Instance.Player.CoinController.TotalCoin < storeItem.Item.Price)
                {
                    itemBuyUi.BuyButton.interactable = false;
                    itemBuyUi.PriceText.color = Color.red;
                }

                itemBuyUi.BuyButton.onClick.AddListener(() => OnBuyItemClick(storeItem));

                refreshItemsCallback += () => UpdateItemToSell(itemBuyUi, storeItem.Item);
            }
        }
        else
        {
            foreach (var inventorySlot in GameManager.Instance.Player.InventorySystem.InventorySlots)
            {
                if (inventorySlot.Item == null) continue;

                var itemSellUi = Instantiate(itemSellUiPrefab, itemBuyTransformParent);
                itemSellUi.IconImage.sprite = inventorySlot.Item.ItemIcon;
                itemSellUi.NameText.text = inventorySlot.Item.DisplayName;
                itemSellUi.ValueText.text = inventorySlot.Item.Price.ToString();

                if (inventorySlot.IsEquipped == true)
                {
                    itemSellUi.IsEquippedPanel.gameObject.SetActive(true);
                }
                else
                    itemSellUi.SellButton.onClick.AddListener(() => OnSellItemClick(inventorySlot, itemSellUi));
            }
        }

    }

    private void UpdateItemToSell(ItemBuyUi itemBuyUi, ItemBase itemToSell)
    {
        if (GameManager.Instance.Player.CoinController.TotalCoin >= itemToSell.Price)
        {
            itemBuyUi.BuyButton.interactable = true;
            itemBuyUi.PriceText.color = Color.white;
        }
        else
        {
            itemBuyUi.BuyButton.interactable = false;
            itemBuyUi.PriceText.color = Color.red;
        }        
    }

    private void OnBuyItemClick(StoreItem storeItem)
    {
        GameManager.Instance.Player.CoinController.SpendCoin(storeItem.Item.Price);
        GameManager.Instance.Player.InventorySystem.AddItem(storeItem.Item, storeItem.Amount);

        totalCoinText.text = GameManager.Instance.Player.CoinController.TotalCoin.ToString();
        refreshItemsCallback();
    }

    private void OnSellItemClick(InventorySlot inventorySlot, ItemSellUi itemSell)
    {
        itemSell.gameObject.SetActive(false);

        GameManager.Instance.Player.CoinController.AddCoin(inventorySlot.Item.Price);
        GameManager.Instance.Player.InventorySystem.RemoveItem(inventorySlot.Item);

        totalCoinText.text = GameManager.Instance.Player.CoinController.TotalCoin.ToString();
    }
}
