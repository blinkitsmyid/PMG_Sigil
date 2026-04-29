using UnityEngine;

public class GhostTraitObject : MonoBehaviour
{
    public GhostTrait trait;

    private GhostSoundTrait soundTrait;

    private void Awake()
    {
        soundTrait = GetComponent<GhostSoundTrait>();
    }

    private void OnEnable()
    {
        soundTrait?.Activate();
    }

    private void OnDisable()
    {
        soundTrait?.Deactivate();
    }
}