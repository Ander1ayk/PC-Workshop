using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;

    private void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats is not assigned in the inspector.");
        }
    }

}
