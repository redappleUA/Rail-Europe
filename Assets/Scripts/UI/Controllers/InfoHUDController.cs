using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InfoHUDController : MonoBehaviour, IUIActivator
{
    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
