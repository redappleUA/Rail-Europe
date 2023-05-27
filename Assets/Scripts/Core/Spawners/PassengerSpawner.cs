using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] float _timeBetweenSpawn;
    private bool _defeat = false;
    
    void Start()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (!_defeat)
        {
            var randomCity = CityService.GetRandomCityNameReference();
            var cityTo = CityService.GetRandomCity();

            while(cityTo == randomCity.CityName)
            {
                cityTo = CityService.GetRandomCity();
            }

            Passenger passenger = new(randomCity.CityName, CityService.GetRandomCity());
            randomCity.Passengers.Add(passenger);

            Debug.LogWarning("City " + randomCity.CityName + " has " + randomCity.Passengers.Count + " passangers." + 
                " New passanger going to " + passenger.CityTo);

            yield return new WaitForSeconds(_timeBetweenSpawn);

            // if (Player has defeat) // TODO: Defeat
            // {
            //     _defeat = true;
            // }
        }
    }
}
