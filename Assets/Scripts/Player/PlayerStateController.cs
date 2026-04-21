using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public static PlayerStateController Instance { get; private set; }
    public PlayerState CurrentState { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetState(PlayerState newState)
    {
        if(CurrentState == newState)
            return;
        CurrentState = newState;

        GameEvents.PlayerStateChanged(newState);
        Debug.Log("Player state changed to: " + newState);
    }
}
