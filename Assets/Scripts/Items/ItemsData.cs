using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Item Data", menuName = "ScriptableObjects/Item Data")]
public class ItemsData : ScriptableObject
{
    [Header("Basic Info")]
    public string itemId;
    public string itemName;
    [TextArea]
    public string description;
    [Header("Needed Info")]
    public Image itemImage;
    public GameObject itemPrefab;
}
