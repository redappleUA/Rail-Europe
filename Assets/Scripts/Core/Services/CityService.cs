using Core.Models;
using Cysharp.Threading.Tasks;
using Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class CityService
{
    public static List<CityView> Cities { get { return _cityModel.Elements; } }

    private static readonly CityModel _cityModel = new();
    private static City lastGeneratedCity;

    public static CityView GetCityNameReference(City city)
    {
        return Cities.Find(item => (item.CityName == city));
    }

    public static City GetCityFromObject(ClickableObjectView clickableObject)
    {
        return clickableObject.GetComponent<CityView>().CityName;
    }

    public static CityView GetCityNameReferenceFromObject(ClickableObjectView clickableObject)
    {
        return clickableObject.GetComponent<CityView>();
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

    public static CityView GetRandomCityNameReference()
    {
        return Cities[Random.Range(0, CityService.Cities.Count)];
    }


    public static City GetCurrentCity(TrainView train, BaseWayView way) 
    { 
        // �������� ������� ������
        Vector3 trainPosition = train.transform.position;

        // ��������� ������� �� ��� CityA � CityB
        float distanceToCityA = Vector3.Distance(trainPosition, GetCityNameReference(way.CityA).transform.position);
        float distanceToCityB = Vector3.Distance(trainPosition, GetCityNameReference(way.CityB).transform.position);

        // ��������� ������� �� ��������� ������� ����
        if (distanceToCityA < distanceToCityB)
        {
            return way.CityA;
        }
        else
        {
            return way.CityB;
        }
    }

    public static void AddCity(CityView city)
    {
        _cityModel.Elements.Add(city);
    }

    public static async UniTask<Sprite> LoadCitySpite(City city)
    {
        var citySprite = await LoadResourceService.LoadSprite($"cities/{city.ToString().ToLower().Trim()}");
        return citySprite;
    }

    public static async UniTask<Sprite> LoadCityPushSpite(City city)
    {
        var citySprite = await LoadResourceService.LoadSprite($"cities_push/{city.ToString().ToLower().Trim()}_push");
        return citySprite;
    }
}
