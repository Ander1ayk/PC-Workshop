using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemUIPrefab;
    [SerializeField] private GameObject inventoryUI;
    public static UIInventory Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameEvents.OnInventoryChanged += UpdateUI;
    }
    private void UpdateUI()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        foreach (var slot in Inventory.Instance.slots)
        {
            GameObject itemUI = Instantiate(itemUIPrefab, content);
            itemUI.GetComponent<ItemUI>().Setup(slot.itemData, slot.amount);
        }
    }
    private void OnEnable()
    {
        GameEvents.OnInventoryChanged += UpdateUI;
    }
    private void OnDisable()
    {
        GameEvents.OnInventoryChanged -= UpdateUI;
    }
    public void Show()
    {
        inventoryUI.SetActive(true);
        UpdateUI();
    }

    public void Hide()
    {
        inventoryUI.SetActive(false);
    }
}
