using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private AudioClip doorSound;

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
            AudioManager.Instance.PlaySFX(doorSound, 1f, doorPrefab.transform.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            doorAnimator.SetTrigger("IsClose");
            AudioManager.Instance.PlaySFX(doorSound, 1f, doorPrefab.transform.position);
        }
    }
}
