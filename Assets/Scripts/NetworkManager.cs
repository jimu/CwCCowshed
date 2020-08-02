using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;

public class HighScore
{
    public int rank;
    public string name;
    public int score;
    public string date;

    public HighScore(string rankStr, string name, string scoreStr, string date)
    {
        this.rank = Convert.ToInt32(rankStr);
        this.name = name;
        score = Convert.ToInt32(scoreStr);
        this.date = date; // .Substring(0, 10);  // "2020-12-31 23:59:59"
    }
}


public class NetworkManager : MonoBehaviour
{
    static public NetworkManager instance = null;

    public bool scoresReady = false;
    public HighScore[] scores;

    private bool fetching = false;
    private float nextFetch = 0f;

    private GameObject listener = null;
    private string listenMethod;

    public void Listen(GameObject listener, string method)
    {
        this.listener = listener;
        this.listenMethod = method;
    }
     

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

    IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();
            callback(request);
        }
    }  

    public void Fetch(bool force = false)
    {
        Debug.Log("Fetch(): nextFetch=" + nextFetch + " Time.time=" + Time.time + " fetching=" + (fetching ? "T" : "F"));
        if (!fetching && (nextFetch < Time.realtimeSinceStartup || force))
        {
            Debug.Log("Starting Coroutine");

            fetching = true;
            StartCoroutine(GetRequest("https://osaka.jimu.net/cwc/cwc3/getleaderboard.php?name=Fred", (UnityWebRequest req) =>
            //StartCoroutine(GetRequest("https://osaka.jimu.net/cwc/cwc3/getscores.php?asc", (UnityWebRequest req) =>
            {
                if (req.isNetworkError || req.isHttpError)
                {
                    Debug.Log($"{req.error}: {req.downloadHandler.text}");
                }
                else
                {
                    Debug.Log(req.downloadHandler.text);
                    string[] rows = req.downloadHandler.text.Split('|');
                    scores = new HighScore[rows.Length / 4];
                    for (int i = 0; i < rows.Length - 3; i += 4)
                    {
                        Debug.Log("i=" + i + " scores.Length=" + scores.Length + " rows.Length=" + rows.Length + " scores.Length=" + scores.Length);
                        scores[i / 4] = new HighScore(rows[i], rows[i + 1], rows[i + 2], rows[i + 3]); // rank, name, score, date // name, score, epitath, date
                    }
                    scoresReady = true;
                    fetching = false;
                    nextFetch = Time.realtimeSinceStartup + 15f;
                    if (listener)
                        listener.SendMessage(listenMethod);
                }
            }));
        }
    }
    public void SumbitScore(string name, int score)
    {
        // git hub users: please don't abuse this
        string url = "https://osaka.jimu.net/cwc/cwc3/savescores.php?name=" + name + "&score=" + score;
        Debug.Log("SubmitScore(" + name + ", " + score +")");
        Post(url);
    }



    public void Post(string url)
    {
        Debug.Log("Post(" + url + ")");
        scoresReady = false;
        StartCoroutine(GetRequest(url, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            }
            else
            {
                Debug.Log(req.downloadHandler.text);
            }
            Fetch(true);
        }));
    }

}