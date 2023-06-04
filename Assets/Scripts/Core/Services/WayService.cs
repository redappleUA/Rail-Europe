using Core.Models;
using System.Collections.Generic;
using UnityEngine;

public static class WayService
{
    public static List<Rail> Rails => _railModel.Elements;
    public static List<Bridge> Bridges => _bridgeModel.Elements;

    private static RailModel _railModel = new();
    private static BridgeModel _bridgeModel = new();

    public static Rail GetRail(City cityFrom, City cityTo)
    {
        return _railModel.Elements.Find(item => (item.CityA == cityFrom || item.CityB == cityFrom) && (item.CityA == cityTo || item.CityB == cityTo));
    }
    public static bool TryGetRail(City cityFrom, City cityTo, out Rail rail)
    {
        rail = GetRail(cityFrom, cityTo);
        return rail != null;
    }
    public static Bridge GetBridge(City cityFrom, City cityTo)
    {
        return _bridgeModel.Elements.Find(item => (item.CityA == cityFrom || item.CityB == cityFrom) && (item.CityA == cityTo || item.CityB == cityTo));
    }
    public static bool TryGetBridge(City cityFrom, City cityTo, out Bridge bridge)
    {
        bridge = GetBridge(cityFrom, cityTo);
        return bridge != null;
    }
    public static void AddRail(Rail rail)
    {
        _railModel.Elements.Add(rail);
    }
    public static void AddBridge(Bridge bridge)
    {
        _bridgeModel.Elements.Add(bridge);
    }
}
