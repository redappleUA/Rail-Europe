using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUpCallback : MonoBehaviour
{
    public void DestroyOnAnimtionEnd()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        Destroy(parent);
    } 
}
