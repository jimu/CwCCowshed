using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUpdateScore : MonoBehaviour
{
    
    private void OnEnable()
    {
        //Debug.Log("GameOverUpdateScore.OnEnable called from " + gameObject.name);
        GetComponent<Text>().text = GameManager.instance.GetScore().ToString() + " ft";
    }
}
