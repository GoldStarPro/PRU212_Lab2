using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }


    [SerializeField] private AudioSource bgMusicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip menuBGMusic;
    [SerializeField] private AudioClip gameBGMusic;
    [SerializeField] private AudioClip SkiingSFX;
    [SerializeField] private AudioClip SnowflakeSFX;
    [SerializeField] private AudioClip HitSFX;
    [SerializeField] private AudioClip VictorySFX;

    // Thêm các biến âm lượng cho từng file âm thanh (giá trị từ 0 đến 1)
    [SerializeField, Range(0f, 1f)] private float menuBGMusicVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float gameBGMusicVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float SkiingSFXVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float SnowflakeSFXVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float HitSFXVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float VictorySFXVolume = 1f;

    void Awake()
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

        if (bgMusicSource == null)
        {
            bgMusicSource = gameObject.AddComponent<AudioSource>();
            bgMusicSource.loop = true;
            Debug.Log("Added bgMusicSource automatically.");
        }
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("Added sfxSource automatically.");
        }

        if (menuBGMusic == null) Debug.LogWarning("Menu BG Music is not assigned!");
        if (gameBGMusic == null) Debug.LogWarning("Game BG Music is not assigned!");
        if (SkiingSFX == null) Debug.LogWarning("Skiing SFX is not assigned!");
        if (SnowflakeSFX == null) Debug.LogWarning("Snowflake SFX is not assigned!");
        if (HitSFX == null) Debug.LogWarning("Hit SFX is not assigned!");
        if (VictorySFX == null) Debug.LogWarning("Victory SFX is not assigned!");
    }

    public void PlayMenuBGMusic()
    {
        if (bgMusicSource != null && menuBGMusic != null)
        {
            if (bgMusicSource.clip != menuBGMusic)
            {
                bgMusicSource.clip = menuBGMusic;
                bgMusicSource.volume = menuBGMusicVolume; // Áp dụng âm lượng riêng
                bgMusicSource.Play();
                Debug.Log("Playing Menu BG Music with volume: " + menuBGMusicVolume);
            }
        }
        else
        {
            Debug.LogError("Cannot play Menu BG Music: Source or clip is null.");
        }
    }
    public void StopBGMusic()
    {
        if (bgMusicSource != null)
        {
            bgMusicSource.Stop();
            Debug.Log("BG Music stopped.");
        }
    }

    public void PlayGameBGMusic()
    {
        if (bgMusicSource != null && gameBGMusic != null)
        {
            if (bgMusicSource.clip != gameBGMusic)
            {
                bgMusicSource.clip = gameBGMusic;
                bgMusicSource.volume = gameBGMusicVolume;
                bgMusicSource.Play();
                Debug.Log("Playing Game BG Music with volume: " + gameBGMusicVolume);
            }
        }
        else
        {
            Debug.LogError("Cannot play Game BG Music: Source or clip is null.");
        }
    }

    public void PlaySkiingSFX()
    {
        if (sfxSource != null && SkiingSFX != null)
        {
            sfxSource.PlayOneShot(SkiingSFX, SkiingSFXVolume);
            Debug.Log("Playing Power Up SFX with volume: " + SkiingSFXVolume);
        }
        else
        {
            Debug.LogError("Cannot play Power Up SFX: Source or clip is null.");
        }
    }

    public void PlaySnowflakeSFX()
    {
        if (sfxSource != null && SnowflakeSFX != null)
        {
            sfxSource.PlayOneShot(SnowflakeSFX, SnowflakeSFXVolume);
            Debug.Log("Playing Power Up SFX with volume: " + SnowflakeSFXVolume);
        }
        else
        {
            Debug.LogError("Cannot play Power Up SFX: Source or clip is null.");
        }
    }

    public void PlayHitSFX()
    {
        if (sfxSource != null && HitSFX != null)
        {
            sfxSource.PlayOneShot(HitSFX, HitSFXVolume);
            Debug.Log("Playing Power Up SFX with volume: " + HitSFXVolume);
        }
        else
        {
            Debug.LogError("Cannot play Power Up SFX: Source or clip is null.");
        }
    }

    public void PlayVictorySFX()
    {
        if (sfxSource != null && VictorySFX != null)
        {
            sfxSource.PlayOneShot(VictorySFX, VictorySFXVolume);
            Debug.Log("Playing Power Up SFX with volume: " + VictorySFXVolume);
        }
        else
        {
            Debug.LogError("Cannot play Power Up SFX: Source or clip is null.");
        }
    }
}
