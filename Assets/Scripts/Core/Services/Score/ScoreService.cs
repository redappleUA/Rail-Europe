using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class ScoreService
{
    private const string SCORE_KEY = "BestScore";

    public static int Score { get; private set; } = 0;
    public static int BestScore { get; private set; }

    public static void CalculateScore()
    {
        int dayCount = HUDController.DayCount;
        int arrivedPassengers = PassengerService.ArrivedPassengers;

        Score = dayCount * arrivedPassengers;

        LoadScore();
        SaveScore();
    }

    private static void SaveScore()
    {
        if(Score > BestScore)
            PlayerPrefs.SetInt(SCORE_KEY, Score);
        PlayerPrefs.Save();
    }

    private static void LoadScore()
    {
        BestScore = PlayerPrefs.GetInt(SCORE_KEY);
    }
}
