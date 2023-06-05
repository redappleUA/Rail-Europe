using UnityEngine;

public class DefeatController : MonoBehaviour
{
    [SerializeField] DefeatScreenController _defeatScreen;
    public DefeatScreenController DefeatScreen => _defeatScreen;
    public bool IsDefeat { get; set; } = false;

    private void Update()
    {
        // Check if the defeat condition is met and the defeat screen is not active
        if (IsDefeat && !_defeatScreen.gameObject.activeSelf)
        {
            // Calculate the score
            ScoreService.CalculateScore();

            // Activate and initialize the defeat screen
            _defeatScreen.Activate();
            _defeatScreen.Initialize();
        }
    }
}
