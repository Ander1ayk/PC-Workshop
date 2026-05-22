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
        StartCoroutine(DeliverItemsRoutine());
    }
    private System.Collections.IEnumerator DeliverItemsRoutine()
    {
        AudioManager.Instance.PlaySFX(deliverSound, 1f);
        foreach (var item in boughtItems)
        {
            Vector3 randomOffset = new Vector3(
            Random.Range(-0.3f, 0.3f),
            0f,
            Random.Range(-0.3f, 0.3f)
        );
            Vector3 spawnPosition = storeDelivery.position + randomOffset;

            GameObject box = Instantiate(boxForDelivery[Random.Range(0, boxForDelivery.Length)], spawnPosition, Quaternion.identity);
            box.GetComponent<BoxForDelivery>().Setup(item);
            yield return new WaitForSeconds(1f);
        }
        boughtItems.Clear();
    }

}
