using UnityEngine;

public class BoxForDelivery : MonoBehaviour, IInteractable
{
    private ItemsData currentItem;
    public void Setup(ItemsData item)
    {
        // Setup the box with the item data (e.g., change appearance based on item type)
        if (item == null) return;
        currentItem = item;
    }
    public void Interact()
    {
        GameEvents.ItemAdded(currentItem, 1);
        GameEvents.ItemCollectedFromBox(currentItem);
       
        Destroy(gameObject);
    }
}
