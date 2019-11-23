using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ResizableUIDraggable : MonoBehaviour, IDragHandler
{

#pragma warning disable 0649
    [SerializeField] private ResizableUIObject _resizableElement;
    [SerializeField] private ResizableUIDraggableIds _draggableId;
#pragma warning restore 0649

    public ResizableUIDraggableIds DraggableID { get { return _draggableId; } }


    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _resizableElement.UpdateSize(this, (Vector2)transform.position - eventData.position);
    }

}
