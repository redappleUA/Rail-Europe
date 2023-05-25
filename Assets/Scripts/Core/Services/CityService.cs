using Core.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CityService
{
    public static List<CityNameReference> Cities { get { return _cityModel.Elements; } }

    private static readonly CityModel _cityModel = new();

    public static City GetCityFromObject(ClickableObject clickableObject)
    {
        return clickableObject.GetComponent<CityNameReference>().CityName;
    }

    public static City GetRandomCity()
    {
        return (City)Random.Range(0, System.Enum.GetValues(typeof(City)).Length);
    }

    public static void AddCity(CityNameReference city)
    {
        _cityModel.Elements.Add(city);
    }
}
