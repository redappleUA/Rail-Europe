using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour, IWay
{
    public virtual City CityA { get; }

    public virtual City CityB { get; }

    public virtual int BuildResources { get; }

    public virtual List<Transform> PointsForTranslate { get; }  

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
