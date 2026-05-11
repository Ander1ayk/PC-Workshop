using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public static PlayerStateController Instance { get; private set; }
    public PlayerState CurrentState { get; private set; }
    private int mistakesCount = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            CurrentState = PlayerState.moving;
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
    public int GetMistakesCount()
    {
        return mistakesCount;
    }
    private void OnEnable()
    {
        GameEvents.OnMadeMistake += HandleMistakeMade;
    }
    private void OnDisable()
    {
        GameEvents.OnMadeMistake -= HandleMistakeMade;
    }
    private void HandleMistakeMade()
    {
        mistakesCount++;
    }
    public PlayerState GetCurrentState()
    {
        return CurrentState;
    }
}
