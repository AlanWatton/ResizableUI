using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ResizableElementDraggable : MonoBehaviour, IDragHandler
{

#pragma warning disable 0649
    [SerializeField] private ResizableElement _resizableElement;
    [SerializeField] private ResizableElementCorners _corner;
#pragma warning restore 0649

    private Vector2 _lastKnownPosition;

    private void Awake()
    {
        _lastKnownPosition = transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _resizableElement.UpdateSize(_corner, _lastKnownPosition - eventData.position);
        _lastKnownPosition = eventData.position;
    }
}
