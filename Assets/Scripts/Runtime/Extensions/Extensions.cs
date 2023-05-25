using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extensions
{
    public static bool IsPointerOverUIObject(this MonoBehaviour monoBehaviour, Vector2 touchPosition)
    {
        PointerEventData eventData = new(EventSystem.current)
        {
            position = touchPosition
        };

        List<RaycastResult> raycastResults = new();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        return raycastResults.Count > 0;
    }
}
