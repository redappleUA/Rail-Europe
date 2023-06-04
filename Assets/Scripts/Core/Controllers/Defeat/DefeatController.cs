using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatController : MonoBehaviour 
{
    [SerializeField] DefeatScreenController _defeatScreen;
    public DefeatScreenController DefeatScreen => _defeatScreen;
    public bool IsDefeat { get; set; } = false;

    private void Update()
    {
        if(IsDefeat && !_defeatScreen.gameObject.activeSelf)
        {
            ScoreService.CalculateScore();
            _defeatScreen.Activate();
            _defeatScreen.Initialize();
        }
    }
}
