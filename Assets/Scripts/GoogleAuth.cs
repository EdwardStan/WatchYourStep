using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GoogleAuth : MonoBehaviour
{















    /* bool connectedToGooglePlay;

     private void Awake()
     {
         PlayGamesPlatform.DebugLogEnabled = true;
         PlayGamesPlatform.Activate();
     }

     private void Start()
     {
         LogInToGooglePlay();
     }

     void InitializeGoogle()
     {

     }

     private void LogInToGooglePlay()
     {
         PlayGamesPlatform.Instance.Authenticate(ProcessAuthentification);
     }

     private void ProcessAuthentification(SignInStatus status)
     {
         if(status == SignInStatus.Success)
         {
             connectedToGooglePlay = true;
         }
         else
         {
             connectedToGooglePlay = false;
         }
     }

     public void ShowLeaderboard()
     {
         if (!connectedToGooglePlay) { LogInToGooglePlay(); }
         Social.ShowLeaderboardUI();
     }*/

    public string Token;
    public string Error;

    void Awake()
    {
        //Initialize PlayGamesPlatform
        PlayGamesPlatform.Activate();
        LoginGooglePlayGames();
    }

    public void LoginGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play games successful.");

                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    Token = code;
                    // This token serves as an example to be used for SignInWithGooglePlayGames
                });
            }
            else
            {
                Error = "Failed to retrieve Google play games authorization code";
                Debug.Log("Login Unsuccessful");
            }
        });
    }


}
