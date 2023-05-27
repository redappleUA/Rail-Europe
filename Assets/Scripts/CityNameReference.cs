using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CityNameReference : MonoBehaviour
{
    [SerializeField] City _city;
    public City CityName => _city;
    public List<Passenger> Passengers { get; private set; } = new();

    private void Awake()
    {
        CityService.AddCity(this);
    }
}
