using UnityEngine;

public class PCShop : MonoBehaviour, IInteractable
{
    [Header("Info")]
    [SerializeField] private GameObject shopUI;
    private void Start()
    {
        shopUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PlayerStateController.Instance.SetState(PlayerState.assembling);
            CloseShop();
        }
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
