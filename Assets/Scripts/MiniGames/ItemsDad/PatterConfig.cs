using System.Collections.Generic;
using UnityEngine;

namespace DragDropGame
{
    [CreateAssetMenu(fileName = "NewPattern", menuName = "MiniGame/Pattern", order = 1)]
    public class PatternConfig : ScriptableObject
    {
        public int patternID;
        public string patternName;
        public List<ItemSlotAssignment> assignments = new List<ItemSlotAssignment>();
        
        // 🔥 НОВОЕ: общий список предметов, которые будут в этом паттерне
        public List<ItemType> availableItems = new List<ItemType>();
    }
}