using Core.Models;
using System.Collections.Generic;
using UnityEngine;

public static class WayService
{
    public static List<RailView> Rails => _railModel.Elements;
    public static List<BridgeView> Bridges => _bridgeModel.Elements;

    private static RailModel _railModel = new();
    private static BridgeModel _bridgeModel = new();

    public static RailView GetRail(City cityFrom, City cityTo)
    {
        return _railModel.Elements.Find(item => (item.CityA == cityFrom || item.CityB == cityFrom) && (item.CityA == cityTo || item.CityB == cityTo));
    }
    public static bool TryGetRail(City cityFrom, City cityTo, out RailView rail)
    {
        rail = GetRail(cityFrom, cityTo);
        return rail != null;
    }
    public static BridgeView GetBridge(City cityFrom, City cityTo)
    {
        return _bridgeModel.Elements.Find(item => (item.CityA == cityFrom || item.CityB == cityFrom) && (item.CityA == cityTo || item.CityB == cityTo));
    }
    public static bool TryGetBridge(City cityFrom, City cityTo, out BridgeView bridge)
    {
        bridge = GetBridge(cityFrom, cityTo);
        return bridge != null;
    }
    public static void AddRail(RailView rail)
    {
        _railModel.Elements.Add(rail);
    }
    public static void AddBridge(BridgeView bridge)
    {
        _bridgeModel.Elements.Add(bridge);
    }
}
