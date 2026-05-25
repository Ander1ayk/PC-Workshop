using System.Collections;
using UnityEngine;

public class AutoSaveManager : MonoBehaviour
{
    private Coroutine saveRoutine;
    private float saveDelay = 5f;
    private void OnEnable()
    {
        GameEvents.OnInventoryChanged += RequestSave;
    }
    private void OnDisable()
    {
        GameEvents.OnInventoryChanged -= RequestSave;
    }
    private void RequestSave()
    {
        if (saveRoutine != null)
        {
            StopCoroutine(saveRoutine);
        }
        saveRoutine = StartCoroutine(AutoSave());
    }
    private IEnumerator AutoSave()
    {
        yield return new WaitForSeconds(saveDelay);

        SaveSystem.Instance.SaveGame();
        saveRoutine = null;
        Debug.Log("Game auto-saved.");
        GameEvents.AutoSaveRequested();
    }
}
