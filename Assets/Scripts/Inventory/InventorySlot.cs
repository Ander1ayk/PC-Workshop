using System;
[Serializable]
public class InventorySlot
{
    public ItemsData itemData;
    public int amount;

    public InventorySlot(ItemsData itemData, int amount)
    {
        this.itemData = itemData;
        this.amount = amount;
    }
}
