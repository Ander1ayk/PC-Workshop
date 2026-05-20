using UnityEngine;
[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    [Header("Player Stats")]
    public float speed;
    public float interactionRange;
    [Header("Mouse sensitivity")]
    [Range(0.1f, 4f)]
    public float mouseSensitivity = 2f;
}
