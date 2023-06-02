using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUpSpawner : MonoBehaviour
{
    [SerializeField] GameObject _textPrefab;

    public GameObject Spawn()
    {
        var text = Instantiate(_textPrefab);
        return text;
    }
}
