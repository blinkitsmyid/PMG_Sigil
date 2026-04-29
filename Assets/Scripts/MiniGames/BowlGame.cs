using System.Collections;
using UnityEngine;

public class CupMinigame : MonoBehaviour
{
    [SerializeField] private CircleInput[] circles;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource audioSource;
    
    private int currentIndex = 0;
    private Coroutine failRoutine;

    private void Start()
    {
        // подписываемся на все круги
        for (int i = 0; i < circles.Length; i++)
        {
            int index = i;
            circles[i].OnCircleComplete += () => OnCircleDone(index);
        }

        ResetGame();
    }

    private void OnCircleDone(int circleIndex)
    {
       
        if (circleIndex != currentIndex)
            return;

      
      

        ActivateStep();
    }
    private void ActivateStep()
    {
        if (failRoutine != null)
            StopCoroutine(failRoutine);

        // 🔥 проигрываем поверх
        audioSource.PlayOneShot(clips[currentIndex]);

        float clipLength = clips[currentIndex].length;

        circles[currentIndex].SetActive(false);

        currentIndex++;

        if (currentIndex >= clips.Length)
        {
            StartCoroutine(WinCoroutine());
            return;
        }

        circles[currentIndex].SetActive(true);

        failRoutine = StartCoroutine(FailIfNotPressed(clipLength));
    }

    private IEnumerator FailIfNotPressed(float time)
    {
        yield return new WaitForSeconds(time);
        Fail();
    }

    private void Fail()
    {
        Debug.Log("FAIL");
        ResetGame();
    }

    private IEnumerator WinCoroutine()
    {
        Debug.Log("WIN!");
        
        yield return new WaitForSecondsRealtime(0.7f);
        AudioManager.Instance.PlayWinBowlsSound();
        yield return new WaitForSecondsRealtime(3f);
        GamesManager.Instance.CloseBowlsGame();
        GhostBehaviour.Instance.ApplyExorcism(ExorcismType.Bowls);
    }

    private void ResetGame()
    {
        currentIndex = 0;

        for (int i = 0; i < circles.Length; i++)
        {
            circles[i].SetActive(i == 0); // только первый активен
        }
    }
}