using System.Collections;
using UnityEngine;

public class GhostSoundTrait : MonoBehaviour
{
    [Header("Звук")]
    public AudioClip clip;

    [Header("Интервал (сек)")]
    public float interval = 60f;

    private AudioSource source;
    private Coroutine loop;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        if (source == null)
            source = gameObject.AddComponent<AudioSource>();

        source.playOnAwake = false;
        source.loop = false;
    }

    public void Activate()
    {
        loop = StartCoroutine(PlayLoop());
    }

    public void Deactivate()
    {
        if (loop != null)
            StopCoroutine(loop);
    }

    private IEnumerator PlayLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            if (clip != null)
                source.PlayOneShot(clip);
        }
    }
}