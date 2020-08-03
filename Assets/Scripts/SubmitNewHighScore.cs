using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// This implements the "Enter your name" Dialog
public class SubmitNewHighScore : MonoBehaviour
{
    const int MAX_PLAYER_NAME_LENGTH = 15;

    [SerializeField] InputField nameInputField = null;
    [SerializeField] Button submitButton = null;
    [SerializeField] Text scoreText = null;

    // Update is called once per frame
    private GameManager gm;


    private void Init()
    {
        if (gm == null)
        {
            gm = GameManager.instance;
        }
    }

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Init();
        scoreText.text = gm.GetScore().ToString();
        EnableNameField();
    }

    void EnableNameField()
    {
        nameInputField.text = gm.GetPlayerName();
        nameInputField.gameObject.SetActive(true);
        nameInputField.Select();
        nameInputField.ActivateInputField();
        ValueChangeCheck();

        nameInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        nameInputField.onEndEdit.AddListener(fieldValue =>
        {
            OnSubmitButtonPressed();
        });
    }


    void ValueChangeCheck()
    {
        submitButton.interactable = nameInputField.text.Length > 2 && nameInputField.text[0] != ' ';
        if (nameInputField.text.Length > MAX_PLAYER_NAME_LENGTH)
            nameInputField.text = nameInputField.text.Substring(0, MAX_PLAYER_NAME_LENGTH);
    }

    public void OnSubmitButtonPressed()
    {
        string name = nameInputField.text;

        if (name.Length > 2)
        {
            //Debug.Log("SubmitNewHighScore.OnSubmitbuttonPressed()");
            NetworkManager.instance.SumbitScore(name, gm.GetScore());
            gm.SavePlayerName(name);
            gm.OnHighScoresButton();
        }
        else
            gm.PlaySound(GameManager.instance.sfxBadInput);
    }



}





