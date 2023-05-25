using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] float _speed;
    public float Speed => _speed;
    public Route Route { get; set; }
    public List<Passanger> Passengers { get; private set; } = new();
}
