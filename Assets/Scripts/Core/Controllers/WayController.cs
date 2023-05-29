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

        (Rail rail, Bridge bridge) ways = (rail: null, bridge: null);
        ways.rail = WayService.GetRail(CityFrom, CityTo);
        ways.bridge = WayService.GetBridge(CityFrom, CityTo);

        if (ways.rail != null && ResourcesData.RailCount >= ways.rail.BuildResources)
        {
            ways.rail.gameObject.SetActive(true);
            ResourcesData.RailCount -= ways.rail.BuildResources;
        }
        else if (ways.bridge != null && ResourcesData.BridgeCount >= ways.bridge.BuildResources)
        {
            ways.bridge.gameObject.SetActive(true);
            ResourcesData.BridgeCount -= ways.bridge.BuildResources;
        }
        else Debug.LogWarning("Way cannot be build");

    }
}
