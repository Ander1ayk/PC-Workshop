using UnityEngine;

public class PCPlacementManager : MonoBehaviour
{
    public static PCPlacementManager Instance { get; private set; }

    private Table currentTable;
    private Transform spawnP;
    private void Awake()
    {
        Instance = this;
    }
    public void OpenPCSelection(Table table, Transform spawnPoint)
    {
        currentTable = table;
        spawnP = spawnPoint;
        PlayerStateController.Instance.SetState(PlayerState.ui);

        UIInventory.Instance.ShowPCBoxes(this); //  отдельный режим UI
    }

    public void PlacePC(GameObject pcPrefab)
    {
        var obj = Instantiate(pcPrefab, spawnP.position, Quaternion.identity);
        PlayerStateController.Instance.SetState(PlayerState.moving);

        UIInventory.Instance.Hide();
    }
}
