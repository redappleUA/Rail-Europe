using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CityNameReference : MonoBehaviour, IPassengerPosition
{
    [SerializeField] City _city;
    [SerializeField] Transform[] _passengerPositions = new Transform[4] { null, null, null, null };
    public City CityName => _city;
    public Transform[] PassengerPositions { get { return _passengerPositions; } }
    public List<PassengerAttached> Passengers { get; private set; } = new();
    public SpriteRenderer CitySpriteRenderer { get; private set; }
    public Sprite CitySprite { get; private set; }

    private void Awake()
    {
        CityService.AddCity(this);
        CitySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        CitySprite = CitySpriteRenderer.sprite;
    }
}
