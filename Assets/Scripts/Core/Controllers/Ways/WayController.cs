using System.Collections.Generic;
using RDG;
using UnityEngine;

public class WayController : UIPopUp
{
    [SerializeField] TapController _tapController;
    [SerializeField] VibrateController _vibrateController;
    public City CityFrom { get; private set; }
    public City CityTo { get; private set; }
    private List<ClickableObject> _clickableObjects = new();

    private void Start()
    {
        _tapController.OnTapStarted += GetCityFromForWay;
        _tapController.OnTapMoved += AddClickableObjects;
        _tapController.OnTapEnded += TurnOnWay;
    }

    private void GetCityFromForWay(ClickableObject clickableObject)
    {
        CityFrom = CityService.GetCityFromObject(clickableObject);
    }

    private void AddClickableObjects(ClickableObject clickableObject)
    {
        if (!_clickableObjects.Contains(clickableObject))
        {
            _clickableObjects.Add(clickableObject);
        }

    }

    private void TurnOnWay(ClickableObject clickableObject)
    {
        CityTo = CityService.GetCityFromObject(clickableObject);
        
        if (CityTo == CityFrom) return;

        (Rail rail, Bridge bridge) ways = (rail: null, bridge: null);

        ways.rail = WayService.GetRail(CityFrom, CityTo);
        ways.bridge = WayService.GetBridge(CityFrom, CityTo);

        if (ways.rail != null && ResourcesData.RailCount >= ways.rail.BuildResources)
        {
            ways.rail.gameObject.SetActive(true);
            ResourcesData.RailCount -= ways.rail.BuildResources;

            Vibration.Vibrate(_vibrateController.VibrateDuration);
        }
        else if (ways.bridge != null && ResourcesData.BridgeCount >= ways.bridge.BuildResources)
        {
            ways.bridge.gameObject.SetActive(true);
            ResourcesData.BridgeCount -= ways.bridge.BuildResources;

            Vibration.Vibrate(_vibrateController.VibrateDuration);
        }
        else
        {
            if(_clickableObjects.Count <= 2) 
            {
                string text = "Way cannot be build";
                Debug.LogWarning(text);
                OnTextPopUp.Invoke(text);
            }
            _clickableObjects.Clear();
        }

    }
}
