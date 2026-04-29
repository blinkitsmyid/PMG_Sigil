using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DragDropGame
{
    [RequireComponent(typeof(Image))]
    public class DraggableItem : MonoBehaviour, 
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Настройки предмета")]
        public ItemType itemType;
        public bool isPlaced = false;

        private Vector2 startPosition;
        private Transform startParent;

        private Canvas canvas;
        private CanvasGroup canvasGroup;
        private Image image;
        private RectTransform rectTransform;

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();

            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        private void Start()
        {
            startPosition = rectTransform.anchoredPosition;
            startParent = transform.parent;
        }

        public void SetStartPosition(Vector2 newPosition)
        {
            startPosition = newPosition;
            startParent = transform.parent;
            rectTransform.anchoredPosition = newPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isPlaced) return;

            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;

            transform.SetParent(canvas.transform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isPlaced) return;

            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isPlaced) return;

            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            // если не попали в слот
            if (transform.parent == canvas.transform)
            {
                ReturnToStart();
            }
        }

        public void PlaceToSlot(Transform slotTransform)
        {
            isPlaced = true;

            transform.SetParent(slotTransform);
            rectTransform.anchoredPosition = Vector2.zero;

            canvasGroup.blocksRaycasts = false;
            image.raycastTarget = false;
        }

        public void ReturnToStart()
        {
            transform.SetParent(startParent);
            rectTransform.anchoredPosition = startPosition;
        }

        public void ResetItem()
        {
            isPlaced = false;

            canvasGroup.blocksRaycasts = true;
            image.raycastTarget = true;

            ReturnToStart();
        }
    }
}