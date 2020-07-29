using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameStates { INVALID, StartMenu, Playing, GameOver, ScoreMenu}
    float distance = 0;
    Text distanceText;
    Transform playerTransform;

    const string KEY_PLAYER_NAME = "jimu.playername";
    const string KEY_DISTANCE = "jimu.cowshed.distance";

    void Awake()
    {
        distanceText = GameObject.FindGameObjectWithTag("DistanceText").GetComponent<Text>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SetDistance(0);
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
