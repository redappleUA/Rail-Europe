using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainView : MonoBehaviour, IPassengerPosition
{
    [SerializeField] float _speed;
    [SerializeField] Transform[] _passengerPositions = new Transform[] { null, null, null }; //TODO: List instead array
    public float Speed => _speed;
    public Transform[] PassengerPositions { get { return _passengerPositions; } }
    public RouteScheme Route { get; set; }
    public List<PassengerView> Passengers { get; private set; } = new();
    public SpriteRenderer CitySpriteRenderer { get; private set; }
    public Sprite CitySprite { get; private set; }

    private void Awake()
    {
        CitySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        CitySprite = CitySpriteRenderer.sprite;
    }
}
