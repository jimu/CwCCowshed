using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateHighScores : MonoBehaviour
{
    [SerializeField] Text namesText;
    [SerializeField] Text scoresText;
    [SerializeField] Text datesText;
    [SerializeField] GameObject loading;

    public void ScoresReady(string names, string scores, string dates)
    {
        namesText.text = names;
        scoresText.text = scores;
        datesText.text = dates;

        namesText.gameObject.SetActive(true);
        scoresText.gameObject.SetActive(true);
        datesText.gameObject.SetActive(true);
        loading.SetActive(false);

    }
}
