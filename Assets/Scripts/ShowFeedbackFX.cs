using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649


public class ShowFeedbackFX : MonoBehaviour
{
    [SerializeField] GameObject fx;

    /*
    private void OnEnable()
    {
        Debug.Log("ShowFeedbackFX.OnEnable(" + NetworkManager.highScore + ")");
        fx.SetActive(NetworkManager.highScore > 0f);
        Debug.Break();
    }
    
    private void Awake()
    {
        Debug.Log("ShowFeedbackFX.Awake(" + NetworkManager.highScore + ")");
        fx.SetActive(NetworkManager.highScore > 0f);
        Debug.Break();
    }
    */

    private void Start()
    {
        fx.SetActive(GameManager.instance.GetSessionHighScore() > 0f);
        Debug.Log("ShowFeedbackFX.Start(" + NetworkManager.highScore + ")");
    }

}
