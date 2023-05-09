using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayService
{
    private RailModel _railModel;
    public Rail GetRail(City cityFrom, City cityTo)
    {
        return _railModel.Ways.Find(item => item.CityA == cityFrom || item.CityB == cityFrom && item.CityA == cityTo || item.CityB == cityTo);
    }
}
