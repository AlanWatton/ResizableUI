using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtensions
{
    //Anchor to Corners
    public static void SetAnchorTopLeft(this RectTransform rt, bool positionAtAnchor = false)
    {
        rt.anchorMin = new Vector2(0f, 1f);
        rt.anchorMax = new Vector2(0f, 1f);
        rt.anchoredPosition = new Vector2(-1f, 1f);
    }
    public static void SetAnchorTopRight(this RectTransform rt, bool positionAtAnchor = false)
    {
        rt.anchorMin = new Vector2(1f, 1f);
        rt.anchorMax = new Vector2(1f, 1f);
        rt.anchoredPosition = new Vector2(1f, 1f);
    }
    public static void SetAnchorBottomLeft(this RectTransform rt, bool positionAtAnchor = false)
    {
        rt.anchorMin = new Vector2(0f, 0f);
        rt.anchorMax = new Vector2(0f, 0f);
        rt.anchoredPosition = new Vector2(-1f, -1f);
    }
    public static void SetAnchorBottomRight(this RectTransform rt, bool positionAtAnchor = false)
    {
        rt.anchorMin = new Vector2(1f, 0f);
        rt.anchorMax = new Vector2(1f, 0f);
        rt.anchoredPosition = new Vector2(1f, -1f);
        
    }

    //Stretch Sides
    public static void SetAnchorAndStretchAcrossTop(this RectTransform rt, float height = 1f)
    {
        rt.offsetMin = new Vector2(rt.offsetMax.x, -height);
        rt.offsetMax = new Vector2(rt.offsetMax.x, 0f);
        rt.anchorMin = new Vector2(0f, 1f);
        rt.anchorMax = new Vector2(1f, 1f);
        rt.anchoredPosition = new Vector2(0f, (height / 2));
    }

    public static void SetAnchorAndStretchAcrossBottom(this RectTransform rt, float height = 1f)
    {
        rt.offsetMin = new Vector2(rt.offsetMax.x, 0f);
        rt.offsetMax = new Vector2(rt.offsetMax.x, height);
        rt.anchorMin = new Vector2(0f, 0f);
        rt.anchorMax = new Vector2(1f, 0f);
        rt.anchoredPosition = new Vector2(0f, -(height / 2));
    }
    public static void SetAnchorAndStretchAcrossLeft(this RectTransform rt, float width = 1f)
    {
        rt.offsetMin = new Vector2(-width, rt.offsetMax.y);
        rt.offsetMax = new Vector2(0f, rt.offsetMax.y);
        rt.anchorMin = new Vector2(0f, 0);
        rt.anchorMax = new Vector2(0f, 1f);
        rt.anchoredPosition = new Vector2(-(width / 2), 0f);
    }
    public static void SetAnchorAndStrectchAcrossRight(this RectTransform rt, float width = 1f)
    {
        rt.offsetMin = new Vector2(0f, rt.offsetMax.y);
        rt.offsetMax = new Vector2(width, rt.offsetMax.y);
        rt.anchorMin = new Vector2(1f, 0);
        rt.anchorMax = new Vector2(1f, 1f);
        rt.anchoredPosition = new Vector2((width / 2), 0f);
    }
}
