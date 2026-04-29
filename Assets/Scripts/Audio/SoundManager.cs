using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource sfxSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip keySound;
    public AudioClip dooropenSound;
    public AudioClip doorcloseSound;
    public AudioClip lampswitchSound;
    public AudioClip whistleSound;
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip spiderwalkSound;
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
            return;
        }

        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }
    }

    public void PlayHover()
    {
        if (hoverSound != null && sfxSource != null)
            sfxSource.PlayOneShot(hoverSound);
    }

    public void PlayClick()
    {
        if (clickSound != null && sfxSource != null)
            sfxSource.PlayOneShot(clickSound);
    }

    public void PlayWinSound()
    {
        if (winSound != null && sfxSource != null)
            sfxSource.PlayOneShot(winSound);
    }

    public void PlayLoseSound()
    {
        if (loseSound != null && sfxSource != null)
            sfxSource.PlayOneShot(loseSound);
    }
    public void PlayKeySound()
    {
        if (keySound != null && sfxSource != null)
            sfxSource.PlayOneShot(keySound);
    }

    public void PlayWalkSound()
    {
        
    }
    public void PlayRunSound()
    {
        
    }
    public void PlayOpenedDoor()
    {
        if (dooropenSound != null && sfxSource != null)
            sfxSource.PlayOneShot(dooropenSound);
    }
    public void PlayClosedDoor()
    {
        if (doorcloseSound != null && sfxSource != null)
            sfxSource.PlayOneShot(doorcloseSound);
    }
    public void PlayLampSwitch()
    {
        if (lampswitchSound != null && sfxSource != null)
            sfxSource.PlayOneShot(lampswitchSound);
    }
    public void PlayWhistleSound()
    {
        if (whistleSound != null && sfxSource != null)
            sfxSource.PlayOneShot(whistleSound);
    }
    public void Enemy()
    {
        
    }
    
}