using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassangerSpawner : MonoBehaviour, ISpawner
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
            var randomCity = CityService.Cities[Random.Range(0, CityService.Cities.Count)];

            yield return new WaitForSeconds(_timeBetweenSpawn);

            Passanger passanger = new Passanger();

            randomCity.Passangers.Add(passanger);
            Debug.LogWarning("City " + randomCity.CityName + " has " + randomCity.Passangers.Count + " passangers." + 
                " New passanger going to " + passanger.CityTo);


            // if (Player has defeat) // TODO: Defeat
            // {
            //     _defeat = true;
            // }
        }
    }
}
