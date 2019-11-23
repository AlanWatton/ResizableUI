using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResizableUI : MonoBehaviour
{
    private static object _padlock = new object();
    private static ResizableUI _instance = null;
    public static ResizableUI Instance
    {
        get
        {
            if (_instance == null)
            {

                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("Resizable UI");
                        _instance = go.AddComponent<ResizableUI>();
                    }
                }
            }
            return _instance;
        }
    }

    private EventSystem _eventSystem;
    private GraphicRaycaster _graphicRaycaster;
    private List<ResizableUIObject> _resizableObjects { get; set; } = new List<ResizableUIObject>();
    private ResizableUIObject _selectedResizableUIObject = null;

    private void Awake()
    {

        _eventSystem = FindObjectOfType<EventSystem>();
        if (_eventSystem == null)
        {
            Debug.Log("EventSystem not found");
        }

        _graphicRaycaster = FindObjectOfType<GraphicRaycaster>();
        if (_graphicRaycaster == null)
        {
            Debug.Log("GraphicRaycaster not found");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _resizableObjects.Count > 0)
        {
            PointerEventData data = new PointerEventData(_eventSystem);
            data.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            _graphicRaycaster.Raycast(data, results);
            if (results.Count > 0)
            {
                for (int x = 0; x < results.Count; x++)
                {
                    ResizableUIObject resizableUIObject = results[x].gameObject.GetComponent<ResizableUIObject>();
                    if (resizableUIObject != null)
                    {
                        if (resizableUIObject != _selectedResizableUIObject)
                        {
                          ChangeSelectedObject(resizableUIObject);
                        }
                        break;
                    }
                }
            }
        }
    }

    private void ChangeSelectedObject(ResizableUIObject selectedResizableUIObject)
    {
        for (int x = 0; x < _resizableObjects.Count; x++)
        {
            _resizableObjects[x].Select(_resizableObjects[x] == selectedResizableUIObject);
        }
        _selectedResizableUIObject = selectedResizableUIObject;
    }

    public void AddObject(ResizableUIObject resizableUIObject)
    {
        if (!_resizableObjects.Contains(resizableUIObject))
        {
            _resizableObjects.Add(resizableUIObject);
        }
    }
}
