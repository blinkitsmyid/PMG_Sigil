using System;
using UnityEngine;
namespace DragDropGame
{
    [Serializable]
    public class ItemSlotAssignment
    {
        public ItemType itemType;      // Какой предмет
        public int slotIndex;          // Индекс слота (0-4)
        public Sprite slotSprite;      // 🔥 НОВОЕ: какой спрайт силуэта показать в этом слоте
    }
}