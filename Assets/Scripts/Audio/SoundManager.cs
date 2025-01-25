using System.Collections.Generic;
using UnityEngine;
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
    
    private Dictionary<string, AudioLoop> loopMap;
    private Dictionary<string, AudioSFX> sfxMap;
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
        // Initialize maps
        loopMap = new Dictionary<string, AudioLoop>();
        sfxMap = new Dictionary<string, AudioSFX>();

        foreach (var loop in loops)
        {
            loopMap[loop.name] = loop;
        }

        foreach (var sfx in sfxDatabase)
        {
            sfxMap[sfx.name] = sfx;
        }

        // Initialize audio sources
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.loop = true;

        sfxSources = new List<AudioSource>();
        for (int i = 0; i < maxSfxSources; i++)
        {
            sfxSources.Add(gameObject.AddComponent<AudioSource>());
        }
    }

    public void PlayLoop(string loopName)
    {
        if (!loopMap.TryGetValue(loopName, out AudioLoop loop))
        {
            Debug.LogWarning($"Loop not found: {loopName}");
            return;
        }

        if (loop.clip == null)
        {
            Debug.LogWarning($"No clip found for loop: {loopName}");
            return;
        }

        loopSource.clip = loop.clip;
        loopSource.volume = loop.volume;
        loopSource.pitch = loop.pitch;
        loopSource.Play();
    }


    public void StopLoop()
    {
        loopSource.Stop();
    }

    public void PlaySFX(string soundName)
    {
        if (!sfxMap.TryGetValue(soundName, out AudioSFX sound))
        {
            Debug.LogWarning($"SFX not found: {soundName}");
            return;
        }

        // Check if there are any clips
        if (sound.clips == null || sound.clips.Length == 0)
        {
            Debug.LogWarning($"No clips found for SFX: {soundName}");
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