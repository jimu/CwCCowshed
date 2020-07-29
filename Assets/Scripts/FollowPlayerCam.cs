using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    Transform player;
    Vector3 offset;
    float startTrackingX = 12.76f;
    bool isTracking = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isTracking)
            transform.position = player.position + offset;
        else if (player.position.x > startTrackingX)
        {
            offset = transform.position - player.position;
            isTracking = true;
        }
    }
}
