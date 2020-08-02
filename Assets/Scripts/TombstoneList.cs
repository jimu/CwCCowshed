using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

#pragma warning disable 0649

public class TombstoneList : MonoBehaviour
{

    const float MIN_INDICATOR_DISTANCE = 20f;


    HighScore[] tombstones;
    int next = 0;
    float nextDistance;
    bool noMore = false;

    int  indicatorIndex = -1;

    [SerializeField] GameObject nextIndicator;
    Text nextDistanceText;
    Text nextDistanceShadowText;
    Text nextNameText;
    Text nextNameShadowText;

    public TombstoneFactory tombstoneFactory;

    private void Awake()
    {
        nextDistanceText = nextIndicator.transform.Find("NextDistance").GetComponent<Text>();
        nextDistanceShadowText = nextIndicator.transform.Find("NextDistanceShadow").GetComponent<Text>();
        nextNameText = nextIndicator.transform.Find("NextName").GetComponent<Text>();
        nextNameShadowText = nextIndicator.transform.Find("NextNameShadow").GetComponent<Text>();
    }
    void Start()
    {
        SetTombstones();
    }

    public void SetTombstones()
    {

        tombstones = NetworkManager.instance.scores;


        if (tombstones != null)
        {
            Debug.Log("SetTombstones. There are " + tombstones.Length + " tombstones");
            ////next = 0;
            next = tombstones.Length - 1;
            noMore = false;
            SetNext(0);
        }
        else
            Debug.Log("SetTombstones. Tombstones is NULL");
    }

    public void SetNext(float distance)
    {
        //Debug.Log("SetNext(d=" + distance + ", n=" + next + ")");
        Assert.IsNotNull(tombstones);

        while (next < tombstones.Length && tombstones[next].score < distance)
            ////next++;
            next--;

        ////if (next < tombstones.Length)
        if (next >= 0)
        { 
            nextDistance = tombstones[next].score;
            float delta = nextDistance - distance;
            SetIndicatorActive(delta > MIN_INDICATOR_DISTANCE);

            tombstoneFactory.Create(tombstones[next].name, tombstones[next].score, tombstones[next].date);
        }
        else
        {
            SetIndicatorActive(false);
            noMore = true;
        }
    }



    // Assumes next is valid (<tombstones.Length)
    // Enables indicator if necessary
    // Updates name if necessary
    bool SetIndicatorActive(bool isActive = true)
    {

        if (isActive && indicatorIndex != next)
        {
            nextNameText.text = nextNameShadowText.text = tombstones[next].name;
            nextIndicator.SetActive(isActive);
            indicatorIndex = next;
        }
        else if (!isActive && indicatorIndex >= 0)
        {
            nextIndicator.SetActive(false);
            indicatorIndex = -1;
        }
        return isActive;
    }

    // if distance to next is > 20, show indicator
    // if distance to next < 0, advance to next tombstone
    public void UpdateIndicator(float distance)
    {
        if (!noMore)
        {
            float delta = nextDistance - distance;

            if (delta > 20)
                nextDistanceText.text = nextDistanceShadowText.text = Mathf.FloorToInt(delta).ToString();
            else if (delta > 0)
                SetIndicatorActive(false);
            else if (tombstones != null)
                SetNext(distance);
        }
    }

}
