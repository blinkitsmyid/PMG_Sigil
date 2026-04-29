using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Button buttonMusic;
    public Button buttonSound;

    public Sprite spriteMusicOn;
    public Sprite spriteMusicOff;
    public Sprite spriteSoundOn;
    public Sprite spriteSoundOff;

    void Start()
    {
        UpdateUI();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        UpdateUI();
    }

    public void ToggleSound()
    {
        AudioManager.Instance.ToggleSFX();
        UpdateUI();
    }

    void UpdateUI()
    {
        buttonMusic.image.sprite = AudioManager.Instance.IsMusicOn() ? spriteMusicOn : spriteMusicOff;
        buttonSound.image.sprite = AudioManager.Instance.IsSFXOn() ? spriteSoundOn : spriteSoundOff;
    }
}