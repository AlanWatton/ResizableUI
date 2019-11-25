using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResizableUICorner : MonoBehaviour, IDragHandler
{
    public ResizableUIComponent ResizableUIComponent { get; set; }
    public Corners DraggableID { get; set; }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        ResizableUIComponent.UpdateSize(this, (Vector2)transform.position - eventData.position);
    }

}
