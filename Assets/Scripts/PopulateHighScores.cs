using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649


public class PopulateHighScores : MonoBehaviour
{
    [SerializeField] Text ranksText;
    [SerializeField] Text namesText;
    [SerializeField] Text scoresText;
    [SerializeField] Text pranksText;
    [SerializeField] Text pnamesText;
    [SerializeField] Text pscoresText;
    [SerializeField] Text datesText;
    [SerializeField] GameObject loading;

    public void Awake()
    {
        if (NetworkManager.instance.scoresReady)
            ScoresReady();
    }


    public void ScoresReady()
    {
        Leaderboard leaderboard = new Leaderboard(NetworkManager.instance.scores, GameManager.instance.GetPlayerName());

        ranksText.text = leaderboard.ranks;
        namesText.text = leaderboard.names;
        scoresText.text = leaderboard.scores;
        datesText.text = leaderboard.dates;
        pranksText.text = leaderboard.pranks;
        pnamesText.text = leaderboard.pnames;
        pscoresText.text = leaderboard.pscores;

        pranksText.gameObject.SetActive(true);
        pnamesText.gameObject.SetActive(true);
        pscoresText.gameObject.SetActive(true);
        ranksText.gameObject.SetActive(true);
        namesText.gameObject.SetActive(true);
        scoresText.gameObject.SetActive(true);
        datesText.gameObject.SetActive(true);
        loading.SetActive(false);

    }
}
