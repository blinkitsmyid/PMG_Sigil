using UnityEngine;
using DG.Tweening;

public class Float : MonoBehaviour
{
    [Header("Настройки парения")]
    [SerializeField] private float floatHeight = 0.5f;
    [SerializeField] private float floatDuration = 2f;
    [SerializeField] private float randomDelay = 0f;

    [Header("Плавность")]
    [SerializeField] private Ease easeType = Ease.InOutSine;  // мягкое замедление на краях

    void Start()
    {
        float delay = Random.Range(0f, randomDelay);

        transform.DOLocalMoveY(transform.localPosition.y + floatHeight, floatDuration)
            .SetDelay(delay)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(easeType);  
    }
}
