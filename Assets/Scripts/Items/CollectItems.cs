using UnityEngine;

public class CollectItems : MonoBehaviour, IInteractable
{
    public ItemsData itemData;

    public void Interact()
    {
        // Add the item to the player's inventory and destroy the item in the scene.
        GameEvents.ItemAdded(itemData, 1);
        GameEvents.InventoryChanged();
        Debug.Log("Collected " + itemData.itemName);
        Destroy(gameObject);
    }
}
