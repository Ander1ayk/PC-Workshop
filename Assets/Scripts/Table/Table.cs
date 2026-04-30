using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    public Transform itemSpawnPoint;
    public void Interact()
    {
        PCPlacementManager.Instance.OpenPCSelection(this, itemSpawnPoint);
    }
}
