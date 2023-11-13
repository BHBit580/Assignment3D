using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private PlacedObjectTypeSO currentPlacedObjectType;
    [SerializeField] private RectTransform backgroundRectTransform;
    [SerializeField] private GenericEventChannelSO objectTobeDraggedEventChannel;

    private RectTransform draggingObjectRectTransform;
    private Vector3 velocity = Vector3.zero;
    private Vector3 originalPosition; 
    private float dampingSpeed = 0f;

    private void Start()
    {
        draggingObjectRectTransform = GetComponent<RectTransform>();
        originalPosition = draggingObjectRectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObjectRectTransform,
                eventData.position, eventData.pressEventCamera, out var globalMousePosition))
        {
            Vector3 targetPosition = Vector3.SmoothDamp(
                draggingObjectRectTransform.position,
                globalMousePosition,
                ref velocity,
                dampingSpeed
            );

            draggingObjectRectTransform.position = targetPosition;

            // Check if the mouse is not hovering over the background
            if (!IsMouseOverBackground())
            {
                // Handle the situation when the mouse is not over the background
                Debug.Log("Mouse is not hovering over the background.");
                objectTobeDraggedEventChannel.RaiseEvent(currentPlacedObjectType);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset the position to the original position when dragging ends.
        draggingObjectRectTransform.position = originalPosition;
    }

    private bool IsMouseOverBackground()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        // Raycast to check if the mouse is over any UI element
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<RectTransform>() == backgroundRectTransform)
            {
                // Mouse is over the background
                return true;
            }
        }

        // Mouse is not over the background 
        return false;
    }

    private void OnDisable()
    {
        draggingObjectRectTransform.position = originalPosition;
    }
}