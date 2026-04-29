using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject shopUI;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private List<ItemsData> shopItems;

    private void Start()
    {
        PopulateShop();
    }
    private void PopulateShop()
    {
        foreach (var item in shopItems)
        {
            GameObject shopItem = Instantiate(shopItemPrefab, content);
            shopItem.GetComponent<ShopSlotUI>().Setup(item);
        }
    }
}
