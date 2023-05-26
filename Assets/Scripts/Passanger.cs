using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passanger
{
    public City CitySpawn { get; set; }
    public City CityTo { get; private set; } = CityService.GetRandomCity();

    public Passanger(City citySpawn)
    {
        CitySpawn = citySpawn;
    }
}
