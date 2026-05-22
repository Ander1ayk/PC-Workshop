using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private Transform storeDelivery;
    [SerializeField] private GameObject[] boxForDelivery;
    private List<ItemsData> boughtItems;
    [SerializeField] private AudioClip buySound;
    [SerializeField] private AudioClip deliverSound;
    public void BuyItem(ItemsData item)
    {
        if (boughtItems == null)
            boughtItems = new List<ItemsData>();
        boughtItems.Add(item);
        AudioManager.Instance.PlaySFX(buySound, 1f);
    }
    public void DeliverItems()
    {
        if (boughtItems == null || boughtItems.Count == 0)
            return;
        foreach (var item in boughtItems)
        {
            GameObject box = Instantiate(boxForDelivery[Random.Range(0, boxForDelivery.Length)], storeDelivery.position, Quaternion.identity);
            box.GetComponent<BoxForDelivery>().Setup(item);
        }
        boughtItems.Clear();
        AudioManager.Instance.PlaySFX(deliverSound, 1f);
    }
}
