using UnityEngine;

public class PCShop : MonoBehaviour, IInteractable
{
    [Header("Info")]
    [SerializeField] private GameObject shopUI;
    private void Start()
    {
        shopUI.SetActive(false);
    }
    public void Interact()
    {
        PlayerStateController.Instance.SetState(PlayerState.ui);
        shopUI.SetActive(true);
    }
    public void CloseShop()
    {
        PlayerStateController.Instance.SetState(PlayerState.moving);
        shopUI.SetActive(false);
    }
}
