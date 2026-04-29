using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GhostConfig", menuName = "Ghost/Create Ghost Config")]
public class GhostConfig : ScriptableObject
{
    [Header("Имя призрака")]
    public string ghostName;

    [Header("Признаки")]
    public List<GhostTrait> traits = new List<GhostTrait>();

    [Header("Реакции на экзорцизм")]
    public List<ExorcismReaction> reactions = new List<ExorcismReaction>();

    [Header("Правильный экзорцизм (убивает призрака)")]
    public ExorcismType correctExorcism;
    

    public GhostReaction GetReaction(ExorcismType type)
    {
        foreach (var r in reactions)
        {
            if (r.type == type)
                return r.reaction;
        }

        return GhostReaction.Angry;
    }
}