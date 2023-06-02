using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIPopUp : MonoBehaviour
{
    public static UnityEvent<string> OnTextPopUp { get; private set; } = new();
}
