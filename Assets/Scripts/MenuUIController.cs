using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class MenuUIController : MonoBehaviour
{
    public Button muteButton;
    public Sprite[] muteIcons; //0 - unmuted, 2 - muted
    public AudioSource music;
    bool muted = false;

    private void Awake()
    {
        music = GameObject.FindGameObjectWithTag("GameMusic").GetComponent<AudioSource>();
        muted = PlayerPrefs.HasKey("Mute") ?( PlayerPrefs.GetInt("Mute") > 0 ? true : false) : true ;
        if (muted)
        {
            muteButton.image.sprite = muteIcons[1];
            music.volume = 0;
        }
        else
        {
            muteButton.image.sprite = muteIcons[0];
            music.volume = 100;
        }
    }
    public void ButtonManager(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0:

                SceneManager.LoadScene(1);
                break;
            case 1:
                Application.Quit();
                break;

        }

    }

    public void MuteButton()
    {
        muted = !muted;
        PlayerPrefs.SetInt("Mute", muted ? 1 : 0);
        muteButton.image.sprite = muteIcons[muted ? 1 : 0];
        music.volume = muted ? 0 : 100;
    }

    public void ShowAchievement()
    {
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
}
