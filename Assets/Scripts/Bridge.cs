using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour, IWay
{
    [SerializeField] City _cityA;
    [SerializeField] City _cityB;
    [SerializeField] int _buildResources;
    public City CityA => _cityA;
    public City CityB => _cityB;
    public int BuildResources => _buildResources;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
