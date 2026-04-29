using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

namespace DragDropGame
{
    [RequireComponent(typeof(Image))]
    public class DropSlot : MonoBehaviour, IDropHandler
    {
        [Header("Требования")]
        public ItemType requiredType = ItemType.None;
        public bool isFilled = false;
        public DraggableItem currentItem = null;

        [Header("Визуал")]
        public Image silhouetteImage;

        [Header("Спрайты")]
        public Sprite defaultSprite;   
        public Sprite filledSprite;    

        private void Start()
        {
           
            if (silhouetteImage != null && defaultSprite != null)
            {
                silhouetteImage.sprite = defaultSprite;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            
            Debug.Log("ON DROP CALLED ON: " + name);
            if (isFilled) return;

            GameObject dropped = eventData.pointerDrag;
            if (dropped == null) return;

            DraggableItem draggable = dropped.GetComponent<DraggableItem>();
            if (draggable == null) return;
            if (draggable.isPlaced) return;

            Debug.Log($"Слот '{name}' ожидает: {requiredType}, пришёл: {draggable.itemType}");
            Debug.Log($"COMPARE: item={draggable.itemType} required={requiredType}");
            if (draggable.itemType == requiredType)
            {
               
                isFilled = true;
                currentItem = draggable;

                draggable.PlaceToSlot(transform);

                AudioManager.Instance.PlayRight_position_sound();
                if (silhouetteImage != null && filledSprite != null)
                {
                    silhouetteImage.sprite = filledSprite;
                }

                if (PatternManager.Instance != null)
                    PatternManager.Instance.OnItemPlaced();
                GameController.Instance.CheckWin();
                
                Debug.Log($" {draggable.itemType} установлен в слот {name}!");
            }
            else
            {
          
                draggable.ReturnToStart();
                AudioManager.Instance.PlayWrong_position_sound();
                StartCoroutine(WrongPlaceAnimation());
                
                Debug.Log($"Неверный предмет!");
            }
        }

        private IEnumerator WrongPlaceAnimation()
        {
            RectTransform rt = GetComponent<RectTransform>();
            Vector2 originalPos = rt.anchoredPosition;

            for (int i = 0; i < 4; i++)
            {
                rt.anchoredPosition = originalPos + new Vector2(5f, 0);
                yield return new WaitForSecondsRealtime(0.02f);

                rt.anchoredPosition = originalPos - new Vector2(5f, 0);
                yield return new WaitForSecondsRealtime(0.02f);
            }

            rt.anchoredPosition = originalPos;
        }

        public void ResetSlot()
        {
            isFilled = false;
            currentItem = null;

           
            if (silhouetteImage != null && defaultSprite != null)
            {
                silhouetteImage.sprite = defaultSprite;
            }
        }

        
        public void SetSilhouetteSprite(Sprite newSprite)
        {
            if (silhouetteImage == null) return;

            if (newSprite != null)
            {
                silhouetteImage.sprite = newSprite;
                Debug.Log($"Слот {name}: установлен спрайт {newSprite.name}");
            }
            else if (defaultSprite != null)
            {
                silhouetteImage.sprite = defaultSprite;
            }
        }
    }
}