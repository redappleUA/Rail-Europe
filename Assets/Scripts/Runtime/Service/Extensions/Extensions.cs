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

    public static bool ContainsCity(this List<CityNameReference> cityNameReferences, City city)
    {
        foreach (var cityNameReference in cityNameReferences)
        {
            if (cityNameReference.CityName == city)
            {
                return true;
            }
        }
        return false;
    }
}
