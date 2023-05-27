using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Transform[] _passengerPositions = new Transform[3] { null, null, null };
    public float Speed => _speed;
    public Transform[] PassengerPositions { get { return _passengerPositions; } }
    public float PassangerScale => .5f;
    public Route Route { get; set; }
    public List<PassengerAttached> Passengers { get; private set; } = new();
}
