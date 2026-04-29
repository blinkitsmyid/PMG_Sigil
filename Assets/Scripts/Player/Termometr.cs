using UnityEngine;

public class Thermometer : MonoBehaviour
{
    // 🔹 Состояния температуры
    public enum TemperatureState
    {
        Cold,
        Normal,
        Hot
    }

    // 🔹 Набор спрайтов (один стиль)
    [System.Serializable]
    public class ThermometerSpriteSet
    {
        public Sprite cold;
        public Sprite normal;
        public Sprite hot;
    }

    // 🔹 Группа имён → одно состояние
    [System.Serializable]
    public class NameGroup
    {
        public string[] names;
        public TemperatureState state;
    }

    [Header("Компоненты")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Наборы спрайтов")]
    [SerializeField] private ThermometerSpriteSet[] spriteSets;

    [Header("Выбор набора")]
    [SerializeField] private int currentSetIndex;

    [Header("Группы имён")]
    [SerializeField] private NameGroup[] nameGroups;

    [Header("Текущее состояние (для дебага)")]
    [SerializeField] private TemperatureState currentState;

    private void Start()
    {
        UpdateSprite();
    }

    // 🔹 Установить состояние напрямую
    public void TermometrSetState(TemperatureState state)
    {
        currentState = state;
        UpdateSprite();
    }

    // 🔹 Установить по имени (ghostBoy и т.д.)
    public void SetStateByName(string name)
    {
        foreach (var group in nameGroups)
        {
            foreach (var n in group.names)
            {
                if (string.Equals(n, name, System.StringComparison.OrdinalIgnoreCase))
                {
                    TermometrSetState(group.state);
                    return;
                }
            }
        }

        Debug.LogWarning("Не найдено состояние для: " + name);
    }

    // 🔹 Смена набора (например другой стиль термометра)
    public void SetSetIndex(int index)
    {
        currentSetIndex = index;
        UpdateSprite();
    }

    // 🔹 Обновление спрайта
    private void UpdateSprite()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer не назначен!");
            return;
        }

        if (spriteSets == null || spriteSets.Length == 0)
        {
            Debug.LogError("Нет наборов спрайтов!");
            return;
        }

        if (currentSetIndex < 0 || currentSetIndex >= spriteSets.Length)
        {
            Debug.LogError("Неверный индекс набора!");
            return;
        }

        var set = spriteSets[currentSetIndex];

        switch (currentState)
        {
            case TemperatureState.Cold:
                spriteRenderer.sprite = set.cold;
                break;

            case TemperatureState.Normal:
                spriteRenderer.sprite = set.normal;
                break;

            case TemperatureState.Hot:
                spriteRenderer.sprite = set.hot;
                break;
        }
    }
}