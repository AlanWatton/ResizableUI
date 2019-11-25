using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
public class ResizableUI : MonoBehaviour
{
    private readonly static object _padlock = new object();
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
    private Dictionary<ResizableUIComponent, List<Transform>> _resizableComponentsAndTargetTransforms = new Dictionary<ResizableUIComponent, List<Transform>>();

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
        if (Input.GetMouseButtonDown(0) && _resizableComponentsAndTargetTransforms.Count > 0)
        {
            PointerEventData data = new PointerEventData(_eventSystem)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();

            _graphicRaycaster.Raycast(data, results);

            if (results.Count > 0)
            {
                //TODO find a more efficient way to do this
                for (int x = 0; x < results.Count; x++)
                {
                    ResizableUIComponent[] keys = _resizableComponentsAndTargetTransforms.Where(i => i.Value.Contains(results[x].gameObject.transform)).Select(i => i.Key).ToArray();
                    if (keys.Length > 0){
                        ChangeSelectedObject(keys[0]);
                        break;
                        //DECIDE Uncomment for multiple resize objects?(slider, toggle, etc..) 
                        //for (int j = 0; j < keys.Length; j++)
                        //{
                        //    ChangeSelectedObject(keys[j]);
                        //    break;
                        //}
                    }
                }
            }
        }
    }

    private void ChangeSelectedObject(ResizableUIComponent resizableUIComponent)
    {
        if (!resizableUIComponent.IsSelected)
        {
            foreach (KeyValuePair<ResizableUIComponent, List<Transform>> kvp in _resizableComponentsAndTargetTransforms)
            {

                kvp.Key.Select(kvp.Key == resizableUIComponent);
            }
        }
    }

    public void AddComponent(ResizableUIComponent resizableUIObject, Transform raycastTargetTransform)
    {
        if (!_resizableComponentsAndTargetTransforms.ContainsKey(resizableUIObject))
        {
            _resizableComponentsAndTargetTransforms.Add(resizableUIObject, new List<Transform>());
        }
        _resizableComponentsAndTargetTransforms[resizableUIObject].Add(raycastTargetTransform);
    }
}
