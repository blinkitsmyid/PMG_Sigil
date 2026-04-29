using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CutsceneManager : MonoBehaviour
{
    [Header("��������� ��������")]
    [SerializeField] private Sprite[] cutsceneImages;
    [SerializeField] private Image displayImage;
    [SerializeField] private string nextSceneName;

    private int currentImageIndex = 0;

    void Start()
    {
        if (displayImage == null)
            displayImage = GetComponent<Image>();

        ShowCurrentImage();
    }

    void Update()
    {
        // ��������� ���� ����� ����� Input System
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            NextImage();
        }
    }

    private void NextImage()
    {
        if (currentImageIndex < cutsceneImages.Length - 1)
        {
            currentImageIndex++;
            ShowCurrentImage();
        }
        else
        {
            SceneManager.LoadScene("00_Menu");
        }
    }

    private void ShowCurrentImage()
    {
        if (cutsceneImages.Length > 0 && currentImageIndex < cutsceneImages.Length)
        {
            displayImage.sprite = cutsceneImages[currentImageIndex];
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set!");
        }
    }

    public void SkipCutscene()
    {
        LoadNextScene();
    }
}
