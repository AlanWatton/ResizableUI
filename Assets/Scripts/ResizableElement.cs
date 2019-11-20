using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ResizableElementCorners
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}

public class ResizableElement : MonoBehaviour, IPointerClickHandler
{
#pragma warning disable 0649
    [SerializeField] private GameObject[] _lines;
    [SerializeField] private ResizableElementDraggable[] _corners;
#pragma warning disable 0649

    private RectTransform _rectTransform;
    public bool IsSelected { get; private set; }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void UnSelect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            for (int l = 0; l < _lines.Length; l++)
            {
                _lines[l].SetActive(false);
            }
            for (int c = 0; c < _lines.Length; c++)
            {
                _corners[c].gameObject.SetActive(false);
            }
        }
    }

    public void Select()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            for (int l = 0; l < _lines.Length; l++)
            {
                _lines[l].SetActive(true);
            }
            for (int c = 0; c < _lines.Length; c++)
            {
                _corners[c].gameObject.SetActive(true);
            }
        }
    }

    public void UpdateSize(ResizableElementCorners corner, Vector2 change)
    {
        Vector2 offsetMin = _rectTransform.offsetMin;
        Vector2 offsetMax = _rectTransform.offsetMax;

        switch (corner)
        {
            case ResizableElementCorners.TopLeft:
                _rectTransform.offsetMax = new Vector2(offsetMax.x, offsetMax.y - change.y);
                _rectTransform.offsetMin = new Vector2(offsetMin.x - change.x, offsetMin.y);
                break;
            case ResizableElementCorners.TopRight:
                _rectTransform.offsetMax = new Vector2(offsetMax.x - change.x, offsetMax.y - change.y);
                break;
            case ResizableElementCorners.BottomLeft:
                _rectTransform.offsetMin = new Vector2(offsetMin.x - change.x, offsetMin.y - change.y);

                break;
            case ResizableElementCorners.BottomRight:
                _rectTransform.offsetMax = new Vector2(offsetMax.x - change.x, offsetMax.y);
                _rectTransform.offsetMin = new Vector2(offsetMin.x, offsetMin.y - change.y);
                break;
        }

        for (int c = 0; c < _lines.Length; c++)
        {
            _corners[c].UpdatePosition();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }
}
