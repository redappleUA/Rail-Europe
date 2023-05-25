using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayController
{
    public City CityFrom { get; private set; }
    public City CityTo { get; private set; }

    public void GetCityFromForWay(ClickableObject clickableObject)
    {
        CityFrom = CityService.GetCityFromObject(clickableObject);
    }

    public void TurnOnWay(ClickableObject clickableObject)
    {
        CityTo = CityService.GetCityFromObject(clickableObject);
        
        if (CityTo == CityFrom) return;

        (Rail, Bridge) ways = (null, null);
        ways.Item1 = WayService.GetRail(CityFrom, CityTo);
        ways.Item2 = WayService.GetBridge(CityFrom, CityTo);

        if (ways.Item1 != null)
            ways.Item1.gameObject.SetActive(true);

        else if (ways.Item2 != null)
            ways.Item2.gameObject.SetActive(true);

        else Debug.LogWarning("Way cannot be build");

    }
}
