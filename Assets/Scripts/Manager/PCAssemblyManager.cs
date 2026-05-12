using UnityEngine;

public class PCAssemblyManager : MonoBehaviour
{
    public static PCAssemblyManager Instance { get; private set; }

    private PCSlot currentSlot;
    [SerializeField] private AudioClip placeSound;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (PlayerStateController.Instance.CurrentState != PlayerState.ui)
            return;
        if (Input.GetMouseButtonDown(1))
        {
            currentSlot = null;
            UIInventory.Instance.Hide();

            if (PCRotate.CurrentPC != null)
            {
                PCRotate.CurrentPC.ExitAssembly();
            }
        }
    }
    public void StartPlacing(PCSlot slot)
    {
        currentSlot = slot;

        PlayerStateController.Instance.SetState(PlayerState.ui);

        UIInventory.Instance.Show();
    }

    public void TryPlaceItem(ItemsData item)
    {
        if (currentSlot == null) return;

        currentSlot.TryInstall(item);

        currentSlot = null;

        AudioManager.Instance.PlaySFX(placeSound, 1f);

        UIInventory.Instance.Hide();
        PlayerStateController.Instance.SetState(PlayerState.assembling);
        //Cursor.lockState = CursorLockMode.Locked;
    }
}
