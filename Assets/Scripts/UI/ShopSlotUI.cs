using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Button shopSlotPrefab;

    public void Setup(ItemsData itemData)
    {
        itemNameText.text = $"{itemData.itemName}";
        shopSlotPrefab.image.sprite = itemData.itemIcon;

        shopSlotPrefab.onClick.RemoveAllListeners();
        shopSlotPrefab.onClick.AddListener(() => ShopManager.Instance.BuyItem(itemData));
    }
}
