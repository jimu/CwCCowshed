using UnityEngine;
using System.Runtime.InteropServices;

#pragma warning disable 0649
#pragma warning disable 0414


public class Link : MonoBehaviour
{
    string animalFarm1feedbackUrl = "https://learn.unity.com/submission/5f1ad2ededbc2a00215d2dc7";
    //string easterEggUrl = "https://www.youtube.com/watch?v=-GL5lzMJomY";
    //string highscoresUrl = "https://osaka.jimu.net/cwc";
    public void OpenLinkJSPlugin(int iUrl = 0)
    {
        Debug.Log("Calling pluggin");
#if !UNITY_EDITOR
openWindow(animalFarm1feedbackUrl);
#endif
    }

    [DllImport("__Internal")]
    private static extern void openWindow(string url);

}
