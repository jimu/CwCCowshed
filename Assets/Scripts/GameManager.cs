using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


#pragma warning disable 0649


public class GameManager : MonoBehaviour
{
    public enum GameState { INVALID, StartMenu, Instructions, Playing, GameOver, SubmitHighScore, HighScoreMenu}

    string urlAnimalFarm1 = "https://www.jimu.net/CwCAnimalFarm/index.html";
    string urlFeedback = "https://learn.unity.com/submission/5f1ad2ededbc2a00215d2dc7";
    GameState gameState = GameState.INVALID;

    float distance = 0;
    Text distanceText;
    Transform playerTransform;
    TombstoneFactory tombstoneFactory;
    TombstoneList tombstoneList;
    AudioSource audioSource;

    const string KEY_PLAYER_NAME = "jimu.playername";
    const string KEY_DISTANCE = "jimu.cowshed.distance";


    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject highScorePanel;
    [SerializeField] GameObject instructionsPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject enterNamePanel;

    [SerializeField] GameObject nextTombstoneIndicator;

    [SerializeField] public AudioClip sfxBadInput;

    private bool _running;
    private bool isDirty = false;

    public int GetScore()
    {
        return Mathf.FloorToInt(distance);
    }

    public bool Running {
        get { return _running; }
    }

    public static GameManager instance;

    void Awake()
    {
        Debug.Log("GM.Awake");
        distanceText = GameObject.FindGameObjectWithTag("DistanceText").GetComponent<Text>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        SetDistance(0);

        instance = this;

        audioSource = GetComponent<AudioSource>();

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


    private float SetSessionHighScore(float score)
    {
        return NetworkManager.highScore = score;
    }


    public float GetSessionHighScore()
    {
        return NetworkManager.highScore;
    }


    public void GameOver()
    {
        SetGameState(distance < GetSessionHighScore() ? GameState.GameOver : GameState.SubmitHighScore);
        SetSessionHighScore(distance);
        SetDirty();
    }



    private void Start()
    {
        NetworkManager.instance.Listen(gameObject, "ScoresReady");
        NetworkManager.instance.Fetch();
        SetGameState(GameState.StartMenu);

    }

    void ScoresReady()
    {
        //Debug.Log("ScoresReady: " + NetworkManager.instance.scores.Length);

        tombstoneList.SetTombstones();

        highScorePanel.GetComponent<PopulateHighScores>().ScoresReady();
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
        if (Running)
        {
            SetDistance(playerTransform.position.x);
            tombstoneList.UpdateIndicator(distance);
        }
    }

    void SetGameState(GameState gameState)
    {
        this.gameState = gameState;

        startPanel.SetActive(gameState == GameState.StartMenu);
        highScorePanel.SetActive(gameState == GameState.HighScoreMenu);
        instructionsPanel.SetActive(gameState == GameState.Instructions);
        gameOverPanel.SetActive(gameState == GameState.GameOver);
        enterNamePanel.SetActive(gameState == GameState.SubmitHighScore);

        _running = gameState == GameState.Playing;
        //Time.timeScale = gameState == GameState.Playing ? 1f : 0f;

    }



    public string SavePlayerName(string name)
    {
        PlayerPrefs.SetString("playerName", name);
        return name;
    }

    public void OnHighScoresPressed()
    {
        SetGameState(GameState.HighScoreMenu);
    }


    public void SeeAllHighscoresPressed()
    {
        Application.OpenURL("https://osaka.jimu.net/cwc");
    }


    public string GetPlayerName()
    {
        return PlayerPrefs.GetString("playerName", "");
    }



    public void OnStartButton()
    {
        SetGameState(GameState.Playing);
    }
    public void OnReturnToStartMenu()
    {
        SetGameState(GameState.StartMenu);
        if (isDirty)
        {
            isDirty = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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

    /*
    private void playDerp()
    {
        Time.timeScale = 1.0f;
        GameObject.Find("EmojiDerp").GetComponent<ParticleSystem>().Play();
        GameObject.Find("Beta").GetComponent<ParticleSystem>().Play();
        return;
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;

        Time.timeScale = 1.0f;


        derp2.Play();

        Time.timeScale = 1.0f;
        derp.Play();

        GameObject c = GameObject.Find("Canvas");

        c.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        Time.timeScale = 1.0f;
        Debug.Log("PlayDerp");
        playDerp();
    }
    */
    public void OnFeedbackButton()
    {
        Application.OpenURL(urlFeedback);
    }


    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);

    }

    public void SetDirty()
    {
        isDirty = true;
    }
}



