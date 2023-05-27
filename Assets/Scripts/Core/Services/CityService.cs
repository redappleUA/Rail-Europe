using Core.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class CityService
{
    public static List<CityNameReference> Cities { get { return _cityModel.Elements; } }

    private static readonly CityModel _cityModel = new();
    private static City lastGeneratedCity;

    public static CityNameReference GetCityNameReference(City city)
    {
        return Cities.Find(item => (item.CityName == city));
    }

    public static City GetCityFromObject(ClickableObject clickableObject)
    {
        return clickableObject.GetComponent<CityNameReference>().CityName;
    }

    public static CityNameReference GetCityNameReferenceFromObject(ClickableObject clickableObject)
    {
        return clickableObject.GetComponent<CityNameReference>();
    }

    public static City GetRandomCity()
    {
        Array cities = Enum.GetValues(typeof(City));

        City randomCity;
        do
        {
            randomCity = (City)cities.GetValue(Random.Range(0, cities.Length));
        } while (randomCity == lastGeneratedCity);

        lastGeneratedCity = randomCity;
        return randomCity;
    }

    public static CityNameReference GetRandomCityNameReference()
    {
        return Cities[Random.Range(0, CityService.Cities.Count)];
    }


    public static City GetCurrentCity(Train train, Way way) 
    { 
        // ќтримуЇмо позиц≥ю пот€га
        Vector3 trainPosition = train.transform.position;

        // ¬изначаЇмо в≥дстань до м≥ст CityA ≥ CityB
        float distanceToCityA = Vector3.Distance(trainPosition, GetCityNameReference(way.CityA).transform.position);
        float distanceToCityB = Vector3.Distance(trainPosition, GetCityNameReference(way.CityB).transform.position);

        // ѕор≥внюЇмо в≥дстан≥ та повертаЇмо поточне м≥сто
        if (distanceToCityA < distanceToCityB)
        {
            return way.CityA;
        }
        else
        {
            return way.CityB;
        }
    }

    public static void AddCity(CityNameReference city)
    {
        _cityModel.Elements.Add(city);
    }

    public static Sprite LoadCitySpite(City city)
    {
        Sprite citySprite = Resources.Load<Sprite>($"Sprites/cities/{city.ToString().ToLower().Trim()}");
        return citySprite;
    }
}
