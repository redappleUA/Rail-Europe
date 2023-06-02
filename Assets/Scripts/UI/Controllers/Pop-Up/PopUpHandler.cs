using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpHandler : MonoBehaviour
{
    [SerializeField] TextPopUpSpawner _spawner;

    private void Start()
    {
        UIPopUp.OnTextPopUp.AddListener(SpawnAndHandleText); 
    }

    public void SpawnAndHandleText(string text)
    {
        var TMPtext = _spawner.Spawn();
        TMPtext.GetComponentInChildren<TextMeshProUGUI>().SetText(text);
    }
}
