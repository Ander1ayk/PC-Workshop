using UnityEngine;

public class PCSlot : MonoBehaviour, IInteractable
{
    public ItemsData itemData;
    private bool isInstalled = false;
    public void Interact()
    {
        if (isInstalled) return;
        // Make open inventory UI and choose the item to use on the PC slot, then check if the player has the item in the inventory and if so, remove it and trigger the desired effect.
        //!!!!!!!!!!!!!!!!!!!!!!!!!!! This is just a placeholder for the actual inventory UI interaction, you will need to implement the inventory UI and item selection logic to make this work properly. !!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (Inventory.Instance.HasItem(itemData, 1))
        {
            Inventory.Instance.RemoveItem(itemData, 1);

            isInstalled = true;

            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
            
            GameEvents.InventoryChanged();
            Debug.Log("Used " + itemData.itemName);
            // Here you can add any additional logic for what happens when the item is used on the PC slot, such as unlocking a door, activating a machine, etc.
        }
        else
        {
            Debug.Log("You don't have " + itemData.itemName);
            GameEvents.MadeMistake();
        }
    }
    public bool IsInstalled() => isInstalled;
}
