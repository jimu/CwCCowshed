using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


#pragma warning disable 0649


public class GameManager : MonoBehaviour
{
    public enum GameStates { INVALID, StartMenu, Playing, GameOver, ScoreMenu}
    float distance = 0;
    Text distanceText;
    Transform playerTransform;
    TombstoneFactory tombstoneFactory;
    TombstoneList tombstoneList;

    const string KEY_PLAYER_NAME = "jimu.playername";
    const string KEY_DISTANCE = "jimu.cowshed.distance";


    void Awake()
    {
        distanceText = GameObject.FindGameObjectWithTag("DistanceText").GetComponent<Text>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SetDistance(0);

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

    private void Start()
    {
        NetworkManager.instance.Listen(gameObject, "ScoresReady");
        NetworkManager.instance.Fetch();

        tombstoneFactory.Create("Boxer", 20f, "I will work harder!", "1944");
        tombstoneFactory.Create("Squealer", 24f, "Do not imagine, comrades, that leadership is a pleasure!", "Jul 29");
        tombstoneFactory.Create("Squealer", 24f, "Do not imagine, comrades, that leadership is a pleasure!", "Jul 29");
        tombstoneFactory.Create("Sheep", 42f, "FOUR LEGS GOOD, TWO LEGS BAD", "Jul 29");
        tombstoneFactory.Create("Benjamin", 96f, "Donkeys live a long time. None of you has ever seen a dead donkey.", "1944");

    }

    void ScoresReady()
    {
        Debug.Log("ScoresReady: " + NetworkManager.instance.scores.Length);

        foreach (HighScore score in NetworkManager.instance.scores)
            Debug.Log("Name=" + score.name + " score=" + score.score + " epitath=" + score.epitath + " date=" + score.date);

        tombstoneList.SetTombstones();

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
}
