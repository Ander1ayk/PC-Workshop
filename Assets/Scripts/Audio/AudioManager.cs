using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f, Vector3? position = null)
    {
        if (clip == null) return;

        GameObject go =  new GameObject("SFX_" + clip.name);

        if(position.HasValue)
        {
            go.transform.position = position.Value;
        }

        go.hideFlags = HideFlags.HideInHierarchy;

        AudioSource audioSource = go.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
        audioSource.volume = volume;

        audioSource.spatialBlend = position.HasValue ? 1f : 0f;

        audioSource.Play();
        Destroy(go, clip.length);
    }
    public void StopSFX(AudioClip clip)
    {
        if (clip == null) return;

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source.clip == clip)
            {
                source.Stop();
            }
        }
    }
}
