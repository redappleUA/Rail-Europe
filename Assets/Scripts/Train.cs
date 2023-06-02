using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour, IPassengerPosition
{
    [SerializeField] float _speed;
    [SerializeField] Transform[] _passengerPositions = new Transform[3] { null, null, null };
    public float Speed => _speed;
    public Transform[] PassengerPositions { get { return _passengerPositions; } }
    public Route Route { get; set; }
    public List<PassengerAttached> Passengers { get; private set; } = new();
    public SpriteRenderer CitySpriteRenderer { get; private set; }
    public Sprite CitySprite { get; private set; }

    private void Awake()
    {
        CitySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        CitySprite = CitySpriteRenderer.sprite;
    }
}
