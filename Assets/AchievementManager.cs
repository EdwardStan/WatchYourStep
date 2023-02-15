using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class AchievementManager : MonoBehaviour
{
    public void GrantAchievement(string achievement)
    {
        Social.ReportProgress(achievement, 100.00f, (bool success) =>
        {
            if (success) { Debug.Log(achievement + " " + success); }
            else { Debug.Log(achievement + " " + success); }
        });
    }

    public void GrantRevealAchievement(string achievement)
    {
        Social.ReportProgress(achievement, 0.00f, (bool success) =>
        {
            if (success) { Debug.Log(achievement + " " + success); }
            else { Debug.Log(achievement + " " + success); }
        });
    }
}
