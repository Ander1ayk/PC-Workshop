using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    [Header("Info")]
    [SerializeField] private Transform storeDelivery;
    [SerializeField] private GameObject[] boxForDelivery;
    private List<ItemsData> boughtItems;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void BuyItem(ItemsData item)
    {
        if (boughtItems == null)
            boughtItems = new List<ItemsData>();
        boughtItems.Add(item);
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
    }
}
