using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeView : BaseWayView
{
    [SerializeField] City _cityA;
    [SerializeField] City _cityB;
    [SerializeField] int _buildResources;
    [SerializeField] List<Transform> _pointsForTranslate;
    public override City CityA => _cityA;
    public override City CityB => _cityB;
    public override int BuildResources => _buildResources;
    public override List<Transform> PointsForTranslate => _pointsForTranslate;

    private void Start()
    {
        WayService.AddBridge(this);
        gameObject.SetActive(false);
    }
}
