using System.Collections;
using UnityEngine;

public class GhostAudio : MonoBehaviour
{
    [SerializeField] private AudioSource ghostSource;
    [SerializeField] private float delay = 120f; // время между проигрыванием (в секундах)
    [SerializeField] private bool playOnStart = false;
    public AudioClip ghostCrying;
    public AudioClip ghostLauphing;
    private Coroutine _playRoutine;

    private void Start()
    {
        //GhostPlaySound();
        
    }

    public void GhostLauphingPlaySound()
    {
        if (_playRoutine == null)
        {
            _playRoutine = StartCoroutine(PlayRoutine(ghostLauphing));
        }
    }
    public void GhostCryingPlaySound()
    {
        if (_playRoutine == null)
        {
            _playRoutine = StartCoroutine(PlayRoutine(ghostCrying));
        }
    }
    public void StopPlaying()
    {
        if (_playRoutine != null)
        {
            StopCoroutine(_playRoutine);
            _playRoutine = null;
        }
    }

    private IEnumerator PlayRoutine(AudioClip audioClip)
    {
        while (true)
        {
            if (ghostSource != null)
            {
                ghostSource.PlayOneShot(audioClip);
            }

            yield return new WaitForSeconds(delay);
        }
    }
}