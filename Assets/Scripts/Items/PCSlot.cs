using UnityEngine;

public class PCSlot : MonoBehaviour, IInteractable
{
    public ItemsData itemData;
    private bool isInstalled = false;
    public void Interact()
    {
        if (isInstalled) return;
        PCAssemblyManager.Instance.StartPlacing(this);
    }
    public void TryInstall(ItemsData item)
    {
        if (item != itemData)
        {
            Debug.Log("Wrong item");
            GameEvents.MadeMistake();
            return;
        }

        if (!Inventory.Instance.HasItem(item, 1))
        {
            Debug.Log("No item in inventory");
            return;
        }

        Inventory.Instance.RemoveItem(item, 1);

        isInstalled = true;

        GetComponent<MeshRenderer>().enabled = true;

        Debug.Log("Installed " + item.itemName);
    }
    public bool IsInstalled() => isInstalled;
}
