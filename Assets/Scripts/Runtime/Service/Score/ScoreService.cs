using UnityEngine;

public static class ScoreService
{
    private const string SCORE_KEY = "BestScore";

    public static int Score { get; set; } = 0;
    public static int BestScore => LoadScore();

    public static void CalculateScore()
    {
        int dayCount = HUDController.DayCount;
        int arrivedPassengers = PassengerService.ArrivedPassengers;

        Score = dayCount * arrivedPassengers;

        SaveScore();
    }

    private static void SaveScore()
    {
        if(Score > BestScore)
            PlayerPrefs.SetInt(SCORE_KEY, Score);
        PlayerPrefs.Save();
    }

    private static int LoadScore()
    {
        return PlayerPrefs.GetInt(SCORE_KEY);
    }
}
