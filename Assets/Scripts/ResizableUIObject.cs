using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;

public enum ResizableUIDraggableIds
{
    CornerTopLeft,
    CornerTopRight,
    CornerBottomLeft,
    CornerBottomRight
}

public enum DraggableDirections
{
    Both,
    Vertical,
    Horizontal,
    None
}

public enum ElementType
{
    Text,
    TextMeshProGUI,
    Image
}

public class ResizableUIObject : MonoBehaviour
{
#pragma warning disable 0649
    [Header("Resiza Settings")]
    [SerializeField] private DraggableDirections _draggableDirection;

    [SerializeField] private bool _minSize;
    [SerializeField] private float _minSizeX;
    [SerializeField] private float _minSizeY;

    [SerializeField] private bool _lockCorners;
    [SerializeField] private bool _lockCornerTopLeft;
    [SerializeField] private bool _lockCornerTopRight;
    [SerializeField] private bool _lockCornerBottomLeft;
    [SerializeField] private bool _lockCornerBottomRight;
#pragma warning disable 0649

    private RectTransform _rectTransform;
    public bool IsSelected { get; private set; }
    public DraggableDirections DraggableDirection { get { return _draggableDirection; } }

    private void Awake()
    {
        _rectTransform = transform.parent.GetComponent<RectTransform>();
        if (_rectTransform == null)
        {
            Debug.Log(string.Format("ResizableUIObject {0} unable to locate parent RectTransform", gameObject.name));
        }
    }

    private void Start()
    {
        if (ResizableUI.Instance == null)
        {
            Debug.Log("Add the ResizableUI GameObject to the Scene");
        }
        else
        {
            ResizableUI.Instance.AddObject(this);
        }
    }

    public void Select(bool select)
    {
        if (IsSelected != select)
        {
            IsSelected = select;

            for (int x = 0; x < transform.childCount; x++)
            {
                transform.GetChild(x).gameObject.SetActive(IsSelected);
            }
        }
    }

    public void UpdateSize(ResizableUIDraggable draggable, Vector2 change)
    {
        switch (DraggableDirection)
        {
            case DraggableDirections.Horizontal:
                change.y = 0f;
                break;
            case DraggableDirections.Vertical:
                change.x = 0f;
                break;
            case DraggableDirections.None:
                change = Vector2.zero;
                break;
        }

        if (change == Vector2.zero)
        {
            return;
        }        
        
        Vector2 offsetMin = _rectTransform.offsetMin;
        Vector2 offsetMax = _rectTransform.offsetMax;

        float newX;
        float newY;

        switch (draggable.DraggableID)
        {
            case ResizableUIDraggableIds.CornerTopLeft:
                if (!_lockCornerTopLeft)
                {
                    newX = Mathf.Min(offsetMin.x - change.x, offsetMax.x - _minSizeX);
                    newY = Mathf.Max(offsetMax.y - change.y, offsetMin.y + _minSizeY);
                    _rectTransform.offsetMin = Vector2.Lerp(_rectTransform.offsetMin, new Vector2(newX, offsetMin.y), Time.deltaTime * 50f);
                    _rectTransform.offsetMax = Vector2.Lerp(_rectTransform.offsetMax, new Vector2(offsetMax.x, newY), Time.deltaTime * 50f);
                }
                break;
            case ResizableUIDraggableIds.CornerTopRight:
                if (!_lockCornerTopRight)
                {
                    newX = Mathf.Max(offsetMax.x - change.x, offsetMin.x + _minSizeX);
                    newY = Mathf.Max(offsetMax.y - change.y, offsetMin.y + _minSizeY);
                    _rectTransform.offsetMax = Vector2.Lerp(_rectTransform.offsetMax, new Vector2(newX, newY), Time.deltaTime * 50f);
                }
                break;
            case ResizableUIDraggableIds.CornerBottomLeft:
                if (!_lockCornerBottomLeft)
                {
                    newX = Mathf.Min(offsetMin.x - change.x, offsetMax.x - _minSizeX);
                    newY = Mathf.Min(offsetMin.y - change.y, offsetMax.y - _minSizeY);
                    _rectTransform.offsetMin = Vector2.Lerp(_rectTransform.offsetMin, new Vector2(newX, newY), Time.deltaTime * 50f);
                }
                break;
            case ResizableUIDraggableIds.CornerBottomRight:
                if (!_lockCornerBottomRight)
                {
                    newX = Mathf.Max(offsetMax.x - change.x, offsetMin.x + _minSizeX);
                    newY = Mathf.Min(offsetMin.y - change.y, offsetMax.y - _minSizeY);
                    _rectTransform.offsetMax = Vector2.Lerp(_rectTransform.offsetMax, new Vector2(newX, offsetMax.y), Time.deltaTime * 50f);
                    _rectTransform.offsetMin = Vector2.Lerp(_rectTransform.offsetMin, new Vector2(offsetMin.x, newY), Time.deltaTime * 50f);
                }
                break;

        }
    }
}
