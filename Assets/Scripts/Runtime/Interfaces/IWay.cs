using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWay 
{
    City CityA { get; }
    City CityB { get; }
    int BuildResources { get; }
    void Show();
    void Hide();
}
