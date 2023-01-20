using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("GAME")]
    public int score = 0;
    [Space]
    public GameObject platformObject;
    public GameObject startingPlatform;
    [Space]
    public Player player;
    public GameObject playerPlaceholder;
    [Space]
    public float randomYMin = -5f;
    public float randomYMax = 5f;
    public float distanceBetweenPrefabs = 1f;
    public float platformHeighDiff = 2;
    private float nextPrefabX;
    private float nextPlatformY;
    [Space]
    public Transform platformParent;

    [Space, Space]
    [Header("UI")]
    public TMP_Text scoreTXT;
    public TMP_Text endScoreText;
    public TMP_Text highScoreText;
    public GameObject newHighScore;
    [Space]
    public GameObject gameOverPanel;
    [Space]
    [Header("GameStates")]
    public bool GameStarted = false;
    public GameObject gameStartCanvas;
    public TMP_Text startGameInstructions;

    [Space]
    [Header("AUDIO")]
    public AudioSource audioSource;
    public AudioClip[] audioClips; // 0- Jump, 1- land, 2- Die, 3- click, 4- Special Die :)

    private void Start()
    {
        SpriteRenderer sRenderer = startingPlatform.GetComponent<SpriteRenderer>();
        float spriteWidth = sRenderer.sprite.bounds.size.x * transform.lossyScale.x;
        nextPrefabX = /*startingPlatform.transform.position.x + (spriteWidth / 2) + 1*/20;
        nextPlatformY = startingPlatform.transform.localPosition.y + Random.Range(-platformHeighDiff, platformHeighDiff);
        scoreTXT.text = score.ToString();
    }

    private void Update()
    {
        if ( !GameStarted && (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0)))
        {
            ChangeGameState(0);
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreTXT.text = score.ToString();
    }

    public void SummonPlatform()
    {
        /*float randomY = Random.Range(-5, 5);*/

        Vector3 spawnPos = new Vector3(nextPrefabX, nextPlatformY, 0);

        GameObject platform = Instantiate(platformObject, spawnPos, Quaternion.identity, platformParent);
        if (nextPlatformY >= 2.5) { nextPlatformY += -platformHeighDiff; }
        else if (nextPlatformY <= -3) { nextPlatformY += platformHeighDiff; }
        else { nextPlatformY += Random.Range(-platformHeighDiff, platformHeighDiff); }

        nextPrefabX += distanceBetweenPrefabs;
    }

    public void TriggerGameOverEvent()
    {
        gameOverPanel.SetActive(true);
        endScoreText.text = "Score = " + score.ToString();

        if (score > (PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0))
        {
            newHighScore.gameObject.SetActive(true);
            PlayerPrefs.SetInt("HighScore", score);
        }

        highScoreText.text = "Highscore = " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    //UI 

    public void ButtonManagement(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0:
                SceneManager.LoadScene(0);
                break;
            case 1:
                SceneManager.LoadScene(1);
                break;
        }
    }


    public void ChangeGameState(int gameStateIndex)
    {
        switch (gameStateIndex)
        {
            case 0:
                StartGameState();
                break;
            case 1:
                PlaySoundEffect(4);
                Invoke("TriggerGameOverEvent",1.5f);
                break;

        }
    }

    public void StartGameState()
    {
        Debug.Log("START GAME?!");

        GameStarted= true;
        player.gameObject.SetActive(true); 
        startGameInstructions.gameObject.SetActive(false);
        Destroy(playerPlaceholder.gameObject);

        SummonPlatform();
        Invoke("SummonPlatform", 1f);
        Invoke("SummonPlatform", 2f);
        Invoke("SummonPlatform", 3f);
        Invoke("SummonPlatform", 4f);
    }

    public void PlaySoundEffect(int soundEffectIndex)
    {
        switch(soundEffectIndex) 
        {
            case 0:
                audioSource.PlayOneShot(audioClips[0]);
                break;
            case 1:
                audioSource.PlayOneShot(audioClips[1]);
                break;
            case 2:
                audioSource.PlayOneShot(audioClips[2]);
                break;
            case 3:
                audioSource.PlayOneShot(audioClips[3]);
                break;
            case 4:
                audioSource.PlayOneShot(audioClips[4]);
                break;

        }
    }

}
