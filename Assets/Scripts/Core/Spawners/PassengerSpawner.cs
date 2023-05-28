using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] float _timeBetweenSpawn, _timeDecreasePerSpawn;
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

            if (PassengerService.CheckForMaxSpawnCount(randomCity)) Debug.LogError($"Ñity {randomCity.name} is overcrowded"); //TODO:Defeat

            Passenger passenger = new(randomCity.CityName, CityService.GetRandomCity());

            PassengerAttached passengerAttached = SpawnPassengerAttached(ref randomCity, passenger);
            while( passengerAttached == null )
                yield return null;

            randomCity.Passengers.Add(passengerAttached);

            Debug.LogWarning($"City {randomCity.CityName} has {randomCity.Passengers.Count} passangers." + 
                $" New passanger going to {passenger.CityTo}");

            yield return new WaitForSeconds(_timeBetweenSpawn);

            if(_timeBetweenSpawn > 1)
                _timeBetweenSpawn -= _timeDecreasePerSpawn;


            // if (Player has defeat) // TODO: Defeat
            // {
            //     _defeat = true;
            // }
        }
    }

    private PassengerAttached SpawnPassengerAttached(ref CityNameReference city, Passenger passenger)
    {
        var scenePassenger = PassengerService.InstantiateAttachedPassenger().GetComponent<PassengerAttached>();
        scenePassenger.Construct(passenger, city);

        return scenePassenger;
    }
}
