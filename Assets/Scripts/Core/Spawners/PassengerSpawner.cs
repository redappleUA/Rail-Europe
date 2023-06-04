using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] DefeatController _defeatController;
    [SerializeField] float _timeBetweenSpawn, _timeDecreasePerSpawn;
    
    void Start()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (!_defeatController.IsDefeat)
        {
            var randomCity = CityService.GetRandomCityNameReference();
            var cityTo = CityService.GetRandomCity();

            while(cityTo == randomCity.CityName)
            {
                cityTo = CityService.GetRandomCity();
            }

            if (PassengerService.CheckForMaxSpawnCount(randomCity))
            {
                _defeatController.DefeatScreen.Reason = $"Ñity {randomCity.name} is overcrowded";
                _defeatController.IsDefeat = true;
                yield break;
            }

            Passenger passenger = new(randomCity.CityName, cityTo);

            PassengerAttached passengerAttached = SpawnPassengerAttached(ref randomCity, passenger);
            while( passengerAttached == null )
                yield return null;

            randomCity.Passengers.Add(passengerAttached);

            Debug.Log($"City {randomCity.CityName} has {randomCity.Passengers.Count} passangers." + 
                $" New passanger going to {passenger.CityTo}");

            yield return new WaitForSeconds(_timeBetweenSpawn);

            if(_timeBetweenSpawn > 1)
                _timeBetweenSpawn -= _timeDecreasePerSpawn;
        }
    }

    private PassengerAttached SpawnPassengerAttached(ref CityNameReference city, Passenger passenger)
    {
        var scenePassenger = PassengerService.InstantiateAttachedPassenger().GetComponent<PassengerAttached>();
        scenePassenger.Construct(passenger, city);

        return scenePassenger;
    }
}
