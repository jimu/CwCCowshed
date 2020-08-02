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

    public void ScoresReady(string ranks, string names, string scores, string dates, string pranks, string pnames, string pscores)
    {
        ranksText.text = ranks;
        namesText.text = names;
        scoresText.text = scores;
        datesText.text = dates;
        pranksText.text = pranks;
        pnamesText.text = pnames;
        pscoresText.text = pscores;

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
