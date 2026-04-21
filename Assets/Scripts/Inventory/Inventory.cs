using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<InventorySlot> slots = new List<InventorySlot>();

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
    private void AddItems(ItemsData items, int amount)
    {
        foreach(InventorySlot slot in slots)
        {
            if (slot.itemData == items)
            {
                slot.amount += amount;
                GameEvents.InventoryChanged();
                return;
            }
        }
        slots.Add(new InventorySlot(items, amount));
        GameEvents.InventoryChanged();
    }
    public void RemoveItem(ItemsData item, int amount)
    {
        foreach (var slot in slots)
        {
            if (slot.itemData == item)
            {
                slot.amount -= amount;

                if (slot.amount <= 0)
                    slots.Remove(slot);

                GameEvents.InventoryChanged();
                return;
            }
        }
    }
    public bool HasItem(ItemsData items, int amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.itemData == items && slot.amount >= amount)
            {
                return true;
            }
        }
        return false;
    }
    private void OnEnable()
    {
        GameEvents.OnItemAdded += AddItems;
    }
    private void OnDisable()
    {
        GameEvents.OnItemAdded -= AddItems;
    }
}
