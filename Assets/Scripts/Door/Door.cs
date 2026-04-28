using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject doorPrefab;

    private void Start()
    {
        if (doorAnimator == null)
        {
            Debug.LogError("Door Animator is not assigned in the inspector.");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            doorAnimator.SetTrigger("IsOpen");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            doorAnimator.SetTrigger("IsClose");
        }
    }
}
