using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    [SerializeField] TapController _tapController;
    private WayController _wayController { get; set; } = new();
    void Start()
    {
        _tapController.OnTapStarted += _wayController.GetCityFromForWay;
        _tapController.OnTapMoved += _wayController.AddClickableObjects;
        _tapController.OnTapEnded += _wayController.TurnOnWay;
    }
}
