using UnityEngine;

[CreateAssetMenu(fileName = "PatternConfig", menuName = "Game/Pattern Config")]
public class PatternConfig : ScriptableObject
{
    [Range(1, 10)]
    public int patternID;

    [Tooltip("Строка из 11 цифр от 1 до 5")]
    public string patternSequence;

    public string patternName;
}