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
        if(content == null || itemUIPrefab == null || inventoryUI == null)
        {
            return;
        }
        foreach (Transform child in content)
        {
            if(child != null)
            Destroy(child.gameObject);
        }
        foreach (var slot in Inventory.Instance.slots)
        {
            GameObject itemUI = Instantiate(itemUIPrefab, content);
            itemUI.GetComponent<ItemUI>().Setup(slot.itemData, slot.amount);
        }
    }
    public void ShowPCBoxes(PCPlacementManager manager)
    {
        inventoryUI.SetActive(true);

        foreach (Transform child in content)
            Destroy(child.gameObject);

        foreach (var slot in Inventory.Instance.slots)
        {
            if (slot.itemData.isPCBox)
            {
                var itemUI = Instantiate(itemUIPrefab, content);
                itemUI.GetComponent<ItemUI>().SetupPCBox(slot.itemData, manager);
            }
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
