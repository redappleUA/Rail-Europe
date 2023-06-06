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
       // if (EventSystem.current == null) Debug.Log("EventSystem.current is null");
        EventSystem.current.RaycastAll(eventData, raycastResults);

        return raycastResults.Count > 0;
    }

    public static bool ContainsCity(this List<CityView> cityViews, City city)
    {
        foreach (var cityView in cityViews)
        {
            if (cityView.CityName == city)
            {
                return true;
            }
        }
        return false;
    }
}
