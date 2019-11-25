using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public enum UIComponentType
{
    NotFound,
    Text,
    InputField,
    TextMeshProUGUI,
    TMP_InputField,
    Image,
    RawImage,
    Button,
    Slider,
    Scrollbar,
    Toggle
}

public enum Lines
{
    Top,
    Bottom,
    Left,
    Right
}
public enum Corners
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}

public class ResizableUIComponent : MonoBehaviour
{
#pragma warning disable 0649
    [Header("Corner Tabs")]
    [SerializeField] private Sprite _cornerSprite;
    [SerializeField] private Color _cornerColor = Color.blue;
    [SerializeField] private float _cornerSize = 15f;
    [Header("Outlines")]
    [SerializeField] private Color _outlineColor = Color.white;
    [SerializeField] private float _outlineWidth = 1f;
#pragma warning restore 0649

    private RectTransform _rectTransform;

    private GameObject[] _resizeGameObjects;

    public bool IsSelected { get; private set; }

    private void Awake()
    {
        if (_cornerSprite == null)
        {
            Debug.Log("Corner Sprite not Set");
        }
    }

    private void Start()
    {
        UIComponentType componentType = GetComponentType();
        if (componentType != UIComponentType.NotFound)
        {
            Init(componentType);
        }
        else
        {
            Debug.Log("Unknown UI Component Type");
        }

    }

    private UIComponentType GetComponentType()
    {
        if (transform.GetComponent<Text>() != null)
        {
            return UIComponentType.Text;
        }
        else if (transform.GetComponent<InputField>())
        {
            return UIComponentType.InputField;
        }
        else if (transform.GetComponent<TextMeshProUGUI>())
        {
            return UIComponentType.TextMeshProUGUI;
        }
        else if (transform.GetComponent<TMP_InputField>())
        {
            return UIComponentType.TMP_InputField;
        }
        else if (transform.GetComponent<Image>())
        {
            return UIComponentType.Image;
        }
        else if (transform.GetComponent<RawImage>())
        {
            return UIComponentType.RawImage;
        }
        else if (transform.GetComponent<Slider>())
        {
            return UIComponentType.Slider;
        }
        else if (transform.GetComponent<Scrollbar>())
        {
            return UIComponentType.Scrollbar;
        }
        else if (transform.GetComponent<Toggle>())
        {
            return UIComponentType.Toggle;
        }
        return UIComponentType.NotFound;
    }

    private void Init(UIComponentType componentType)
    {
        switch (componentType)
        {
            case UIComponentType.Text:
                _rectTransform = GetComponent<RectTransform>();
                SpawnOutlineAndCorners();
                ResizableUI.Instance.AddComponent(this, transform);
                break;
            case UIComponentType.InputField:
                _rectTransform = GetComponent<RectTransform>();
                SpawnOutlineAndCorners();
                ResizableUI.Instance.AddComponent(this, transform);
                break;
            case UIComponentType.TextMeshProUGUI:
                _rectTransform = GetComponent<RectTransform>();
                SpawnOutlineAndCorners();
                ResizableUI.Instance.AddComponent(this, transform);
                break;
            case UIComponentType.TMP_InputField:
                _rectTransform = GetComponent<RectTransform>() ;
                SpawnOutlineAndCorners();
                ResizableUI.Instance.AddComponent(this, transform);
                break;
            case UIComponentType.Image:
                _rectTransform = GetComponent<RectTransform>() ;
                SpawnOutlineAndCorners();
                ResizableUI.Instance.AddComponent(this, transform);
                break;
            case UIComponentType.RawImage:
                _rectTransform = GetComponent<RectTransform>() ;
                SpawnOutlineAndCorners();
                ResizableUI.Instance.AddComponent(this, transform);
                break;
            case UIComponentType.Slider:
                //TODO Add check for Background Image Transform
                _rectTransform = GetComponent<RectTransform>();
                SpawnOutlineAndCorners();

                Image[] childImgs = transform.GetComponentsInChildren<Image>();
                for (int x = 0; x < childImgs.Length; x++)
                {
                    ResizableUI.Instance.AddComponent(this, childImgs[x].transform);
                }
                break;
            case UIComponentType.Scrollbar:
                _rectTransform = GetComponent<RectTransform>();
                SpawnOutlineAndCorners();
                ResizableUI.Instance.AddComponent(this, transform);
                break;
            case UIComponentType.Toggle:
                _rectTransform = GetComponent<RectTransform>();
                SpawnOutlineAndCorners();
                //HACK fix this to create one entry
                ResizableUI.Instance.AddComponent(this, transform.GetChild(0));
                ResizableUI.Instance.AddComponent(this, transform.GetChild(1));
                break;
        }
    }

    private void SpawnOutlineAndCorners()
    {
        _resizeGameObjects = new GameObject[8] {
            AddLine(Lines.Top),
            AddLine(Lines.Bottom),
            AddLine(Lines.Left),
            AddLine(Lines.Right),
            AddCorner(Corners.TopLeft),
            AddCorner(Corners.TopRight),
            AddCorner(Corners.BottomLeft),
            AddCorner(Corners.BottomRight)
        };
    }
    
    private GameObject AddLine(Lines line)
    {
        GameObject go = new GameObject("Line " + line.ToString());
        Image img = go.AddComponent<Image>();
        img.color = _outlineColor;

        RectTransform imgRect = img.GetComponent<RectTransform>();
        imgRect.pivot = new Vector2(0.5f, 0.5f);
        imgRect.SetParent(transform);

        switch (line)
        {
            case Lines.Top:
                imgRect.SetAnchorAndStretchAcrossTop(height: _outlineWidth);
                break;
            case Lines.Bottom:
                imgRect.SetAnchorAndStretchAcrossBottom(height:_outlineWidth);
                break;
            case Lines.Left:
                imgRect.SetAnchorAndStretchAcrossLeft(width: _outlineWidth);
                break;
            case Lines.Right:
                imgRect.SetAnchorAndStrectchAcrossRight(width:_outlineWidth);
                break;
        }
        go.SetActive(false);
        return go;
    }

    private GameObject AddCorner(Corners corner)
    {
        GameObject go = new GameObject("Corner " + corner.ToString());

        Image img = go.AddComponent<Image>();
        img.color = _cornerColor;
        if (_cornerSprite != null)
        {
            img.sprite = _cornerSprite;
        }

        ResizableUICorner resizableUICorner = go.AddComponent<ResizableUICorner>();
        resizableUICorner.ResizableUIComponent = this;
        resizableUICorner.DraggableID = corner;

        RectTransform imgRect = img.GetComponent<RectTransform>();
        imgRect.pivot = new Vector2(0.5f, 0.5f);
        imgRect.SetParent(transform);

        switch (corner)
        {
            case Corners.TopLeft:
                imgRect.SetAnchorTopLeft(true);
                break;
            case Corners.TopRight:
                imgRect.SetAnchorTopRight(true);
                break;
            case Corners.BottomLeft:
                imgRect.SetAnchorBottomLeft(true);
                break;
            case Corners.BottomRight:
                imgRect.SetAnchorBottomRight(true);
                break;
        }

        imgRect.sizeDelta = new Vector2(_cornerSize, _cornerSize);
        go.SetActive(false);
        return go;
    }

    public void UpdateSize(ResizableUICorner corner, Vector2 change)
    {
        Vector2 offsetMin = _rectTransform.offsetMin;
        Vector2 offsetMax = _rectTransform.offsetMax;

        float newX;
        float newY;

        switch (corner.DraggableID)
        {
            case Corners.TopLeft:
                newX = Mathf.Min(offsetMin.x - change.x, offsetMax.x);
                newY = Mathf.Max(offsetMax.y - change.y, offsetMin.y);
                _rectTransform.offsetMin = Vector2.Lerp(_rectTransform.offsetMin, new Vector2(newX, offsetMin.y), Time.deltaTime * 50f);
                _rectTransform.offsetMax = Vector2.Lerp(_rectTransform.offsetMax, new Vector2(offsetMax.x, newY), Time.deltaTime * 50f);
                break;
            case Corners.TopRight:
                newX = Mathf.Max(offsetMax.x - change.x, offsetMin.x);
                newY = Mathf.Max(offsetMax.y - change.y, offsetMin.y);
                _rectTransform.offsetMax = Vector2.Lerp(_rectTransform.offsetMax, new Vector2(newX, newY), Time.deltaTime * 50f);
                break;
            case Corners.BottomLeft:
                newX = Mathf.Min(offsetMin.x - change.x, offsetMax.x);
                newY = Mathf.Min(offsetMin.y - change.y, offsetMax.y);
                _rectTransform.offsetMin = Vector2.Lerp(_rectTransform.offsetMin, new Vector2(newX, newY), Time.deltaTime * 50f);
                break;
            case Corners.BottomRight:
                newX = Mathf.Max(offsetMax.x - change.x, offsetMin.x);
                newY = Mathf.Min(offsetMin.y - change.y, offsetMax.y);
                _rectTransform.offsetMax = Vector2.Lerp(_rectTransform.offsetMax, new Vector2(newX, offsetMax.y), Time.deltaTime * 50f);
                _rectTransform.offsetMin = Vector2.Lerp(_rectTransform.offsetMin, new Vector2(offsetMin.x, newY), Time.deltaTime * 50f);
                break;

        }
    }

    public void Select(bool select, bool force = false)
    {
        if (IsSelected != select || force)
        {
            IsSelected = select;
            for (int x = 0; x < _resizeGameObjects.Length; x++)
            {
                _resizeGameObjects[x].SetActive(IsSelected);
            }
        }
    }
}
