using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMPro.TextMeshProUGUI itemNameText;
    [SerializeField] private Button itemButton;

    public void Setup(ItemsData itemData, int amount)
    {
        itemNameText.text = $"{itemData.itemName} x{amount}";
        itemButton.image.sprite = itemData.itemIcon;
        itemButton.onClick.AddListener(() => UseItem(itemData));
    }
    private void UseItem(ItemsData itemData)
    {
        PCAssemblyManager.Instance.TryPlaceItem(itemData);
    }
}
