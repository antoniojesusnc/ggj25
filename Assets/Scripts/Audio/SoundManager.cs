using System.Collections.Generic;
using UnityEngine;

public static class AudioType
{
    public enum SFX
    {
        Jump,
        Footsteps,
    }

    public enum Loop
    {
        PeacefulMusic,
        ActionMusic,
    }
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("Sound Manager");
                    instance = go.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private List<AudioLoop> loops;
    [SerializeField] private List<AudioSFX> sfxDatabase;
    
    private AudioSource loopSource;
    private List<AudioSource> sfxSources;
    private int maxSfxSources = 10;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    private void Initialize()
    {

        // Initialize audio sources
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.loop = true;

        sfxSources = new List<AudioSource>();
        for (int i = 0; i < maxSfxSources; i++)
        {
            sfxSources.Add(gameObject.AddComponent<AudioSource>());
        }
    }

    public void PlayLoop(AudioType.Loop loopType)
    {
        AudioLoop loop = loops.Find(l => l.TypeName == loopType);
    
        if (loop == null)
        {
            Debug.LogWarning($"Loop not found: {loopType}");
            return;
        }

        if (loop.clip == null)
        {
            Debug.LogWarning($"No clip found for loop: {loopType}");
            return;
        }

        loopSource.Stop();
        loopSource.clip = loop.clip;
        loopSource.volume = loop.volume;
        loopSource.pitch = loop.pitch;
        loopSource.Play();
    }


    public void StopLoop()
    {
        loopSource.Stop();
    }

    public void PlaySFX(AudioType.SFX sfxType)
    {
        AudioSFX sound = sfxDatabase.Find(s => s.TypeName == sfxType);
        
        if (sound == null)
        {
            Debug.LogWarning($"SFX not found: {sfxType}");
            return;
        }

        // Check if there are any clips
        if (sound.clips == null || sound.clips.Length == 0)
        {
            Debug.LogWarning($"No clips found for SFX: {sfxType}");
            return;
        }

        AudioSource source = sfxSources.Find(s => !s.isPlaying);
        if (source == null)
        {
            Debug.LogWarning("No available audio sources for SFX");
            return;
        }

        AudioClip clip = sound.clips[Random.Range(0, sound.clips.Length)];
        source.clip = clip;
        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.Play();
    }


    public void StopAllSounds()
    {
        loopSource.Stop();
        foreach (var source in sfxSources)
        {
            source.Stop();
        }
    }
}