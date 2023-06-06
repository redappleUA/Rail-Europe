using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWayView : MonoBehaviour, IWay
{
    public abstract City CityA { get; }

    public abstract City CityB { get; }

    public abstract int BuildResources { get; }

    public abstract List<Transform> PointsForTranslate { get; }  

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
