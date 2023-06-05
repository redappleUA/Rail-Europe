using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateController : MonoBehaviour
{
    [SerializeField] long _vibrateDuration;
    [SerializeField] int _amplitude;

    public long VibrateDuration { get { return _vibrateDuration; } }
    public int Amplitude { get { return _amplitude; } }
}
