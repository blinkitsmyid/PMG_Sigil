using UnityEngine;

public class ColliderOffsetSorter : MonoBehaviour
{
    [Header("Коллайдеры")]
    public Collider2D itemCollider;      // Перетащить вручную или найти автоматически
    public Collider2D playerCollider;    // Перетащить вручную

    [Header("Смещения")]
    public float itemOffsetY = 0f;       // Смещение точки сравнения у предмета
    public float playerOffsetY = 0f;     // Смещение точки сравнения у персонажа

    [Header("Пороги")]
    public int orderWhenAbove = 2;
    public int orderWhenBelow = 1;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (itemCollider == null)
            itemCollider = GetComponent<Collider2D>();

        if (playerCollider == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerCollider = player.GetComponent<Collider2D>();
        }
    }

    void LateUpdate()
    {
        if (playerCollider == null || itemCollider == null) return;

        // Берем нижнюю границу + добавляем смещение
        float itemY = itemCollider.bounds.min.y + itemOffsetY;
        float playerY = playerCollider.bounds.min.y + playerOffsetY;

        spriteRenderer.sortingOrder = playerY > itemY ? orderWhenAbove : orderWhenBelow;
    }
}