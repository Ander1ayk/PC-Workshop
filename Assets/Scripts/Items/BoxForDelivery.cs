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
        //GameObject go = Instantiate(currentItem.itemPrefab, transform.position, Quaternion.identity);

        //Rigidbody rb = go.GetComponent<Rigidbody>();
        //if(rb == null)
        //{
        //    rb = go.AddComponent<Rigidbody>();
        //}
        //Collider col = go.GetComponent<Collider>();
        //if (col == null)
        //{
        //    go.AddComponent<BoxCollider>();
        //}
        //col.enabled = false;
        //Vector3 pushDirection = (Vector3.up + Random.insideUnitSphere * 0.5f).normalized;
        //rb.AddForce(pushDirection * 5f, ForceMode.Impulse);

        //Destroy(go, 1f);
        Destroy(gameObject);
    }
}
