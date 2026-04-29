using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip levelMusic;

    [Header("UI")]
    public AudioClip pageSound; //+
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    [Header("MiniGame")]
    public AudioClip candle_on_sound; //+
    public AudioClip candle_off_sound; //+
    public AudioClip right_position_sound; //+
    public AudioClip wrong_position_sound; //+
    public AudioClip smoke_sound; //+
    public AudioClip winBowlsSound;


    [Header("SFX")]
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource stepSource;
    private bool isMusicOn = true;
    private bool isSoundOn = true;


    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // создаём AudioSource автоматически
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        stepSource =  gameObject.AddComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!isMusicOn) return;

        if (scene.buildIndex <= 1)
            PlayMusic(menuMusic);
        else
            PlayMusic(levelMusic);
    }

    // 🎵 Музыка
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    // 🔊 SFX
    public void PlaySFX(AudioClip clip)
    {
        if (!isSoundOn || clip == null) return;

        sfxSource.PlayOneShot(clip);
    }

    // быстрые методы
    public void PlayClick() => PlaySFX(clickSound);
    public void PlayHover() => PlaySFX(hoverSound);
    public void PlayWin() => PlaySFX(winSound);
    public void PlayLose() => PlaySFX(loseSound);
    public void PlayWinBowlsSound() => PlaySFX(winBowlsSound);


    public void PlayPageSound() => PlaySFX(pageSound); //+
    public void PlayCandleOn_Sound() => PlaySFX (candle_on_sound); //+
    public void PlayCandleOff_Sound() => PlaySFX(candle_off_sound); //+
    public void PlayRight_position_sound() => PlaySFX(right_position_sound); //+
    public void PlayWrong_position_sound() => PlaySFX(wrong_position_sound); //+
    public void PlaySmoke() => PlaySFX(smoke_sound); //+




    // ⚙️ настройки
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;
    }

    public void ToggleSFX()
    {
        isSoundOn = !isSoundOn;
        sfxSource.mute = !isSoundOn;
    }
    public void PlayWalk()
    {
        if (!isSoundOn || walkSound == null) return;

        if (stepSource.clip == walkSound && stepSource.isPlaying) return;

        stepSource.clip = walkSound;
        stepSource.pitch = Random.Range(0.9f, 1.1f); // 🔥 ВОТ ЭТО ГЛАВНОЕ
        stepSource.Play();
    }

    public void PlayRun()
    {
        if (!isSoundOn || runSound == null) return;

        if (stepSource.clip == runSound && stepSource.isPlaying) return;

        stepSource.clip = runSound;
        stepSource.pitch = Random.Range(1.1f, 1.3f); // чуть быстрее и выше
        stepSource.Play();
    }

    public void StopSteps()
    {
        stepSource.Stop();
    }
    public bool IsMusicOn() => isMusicOn;
    public bool IsSFXOn() => isSoundOn;
}