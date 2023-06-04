using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route
{
    public List<CityNameReference> CitiesOnRoute { get; private set; } = new();
    public List<Way> WaysBetweenCities { get; private set; } = new();
}
