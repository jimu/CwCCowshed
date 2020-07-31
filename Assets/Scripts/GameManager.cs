using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


#pragma warning disable 0649


public class GameManager : MonoBehaviour
{
    public enum GameState { INVALID, StartMenu, Instructions, Playing, GameOver, HighScoreMenu}

    string urlAnimalFarm1 = "https://www.jimu.net/CwCAnimalFarm/index.html";
    string urlFeedback = "https://learn.unity.com/submission/5f1ad2ededbc2a00215d2dc7";
    GameState gameState = GameState.INVALID;

    float distance = 0;
    Text distanceText;
    Transform playerTransform;
    TombstoneFactory tombstoneFactory;
    TombstoneList tombstoneList;

    const string KEY_PLAYER_NAME = "jimu.playername";
    const string KEY_DISTANCE = "jimu.cowshed.distance";


    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject highScorePanel;
    [SerializeField] GameObject instructionsPanel;
    [SerializeField] GameObject gameOverPanel;

    public static GameManager instance;

    void Awake()
    {
        distanceText = GameObject.FindGameObjectWithTag("DistanceText").GetComponent<Text>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SetDistance(0);

        instance = this;

        tombstoneFactory = GetComponent<TombstoneFactory>();
        tombstoneList = GetComponent<TombstoneList>();
        tombstoneList.tombstoneFactory = tombstoneFactory;
        /*
        string playername = PlayerPrefs.GetString(KEY_PLAYER_NAME);
        Debug.Log("Player Name: " + PlayerPrefs.GetString(KEY_PLAYER_NAME) + (playername == null ? " (NULL)" : "(NOTNULL)") + (playername == "" ? " (EMPTY)" : " (NOTEMPTY)"));

        if (PlayerPrefs.HasKey(KEY_DISTANCE))
        {
            Debug.Log("Has distance: " + PlayerPrefs.GetString(KEY_DISTANCE));
        }
        else
            Debug.Log("Doesn't have distance");
        */

        //PlayerPrefs0.Save();
    }


    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        // todo highscore
    }



    private void Start()
    {
        NetworkManager.instance.Listen(gameObject, "ScoresReady");
        NetworkManager.instance.Fetch();
        Debug.Log("Start");
        SetGameState(GameState.StartMenu);

        //tombstoneFactory.Create("Boxer", 20f, "I will work harder!", "1944");
        //tombstoneFactory.Create("Squealer", 24f, "Do not imagine, comrades, that leadership is a pleasure!", "Jul 29");
        //tombstoneFactory.Create("Squealer", 24f, "Do not imagine, comrades, that leadership is a pleasure!", "Jul 29");
        //tombstoneFactory.Create("Sheep", 42f, "FOUR LEGS GOOD, TWO LEGS BAD", "Jul 29");
        //tombstoneFactory.Create("Benjamin", 96f, "Donkeys live a long time. None of you has ever seen a dead donkey.", "1944");

    }

    void ScoresReady()
    {
        Debug.Log("ScoresReady: " + NetworkManager.instance.scores.Length);

        string names = "";
        string scores = "";
        string dates = "";

        foreach (HighScore score in NetworkManager.instance.scores)
        {
            Debug.Log("Name=" + score.name + " score=" + score.score + " epitath=" + score.epitath + " date=" + score.date);
            names += score.name + "\n";
            scores += score.score + "\n";
            dates += score.date + "\n";
        }

        tombstoneList.SetTombstones();

        highScorePanel.GetComponent<PopulateHighScores>().ScoresReady(names, scores, dates);
    }

    float SetDistance(float value)
    {
        distance = value;
        distanceText.text = Mathf.FloorToInt(distance).ToString() + " ft";
        return distance;
    }

    // Update is called once per frame
    void Update()
    {
        SetDistance(playerTransform.position.x);
        tombstoneList.UpdateIndicator(distance);

        if (Input.GetKeyDown(KeyCode.F3))
        {
            PlayerPrefs.SetString(KEY_DISTANCE, distance.ToString());
            PlayerPrefs.SetString(KEY_PLAYER_NAME, "Jim " + distance.ToString());
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs0.WriteString("Distance=" + distance);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            string s = PlayerPrefs0.ReadString();
            distanceText.text = s;
            Time.timeScale= 0f;
        }
    }

    void SetGameState(GameState gameState)
    {
        this.gameState = gameState;

        startPanel.SetActive(gameState == GameState.StartMenu);
        highScorePanel.SetActive(gameState == GameState.HighScoreMenu);
        instructionsPanel.SetActive(gameState == GameState.Instructions);
        gameOverPanel.SetActive(gameState == GameState.GameOver);

        Time.timeScale = gameState == GameState.Playing ? 1f : 0f;

    }

    public void OnStartButton()
    {
        SetGameState(GameState.Playing);
    }

    public void OnInstructionsButton()
    {
        SetGameState(GameState.Instructions);
    }

    public void OnHighScoresButton()
    {
        SetGameState(GameState.HighScoreMenu);

    }


    public void OnPlayAF1Button()
    {
        Application.OpenURL(urlAnimalFarm1);
    }

    public void OnFeedbackButton()
    {
        Application.OpenURL(urlFeedback);
    }

}


