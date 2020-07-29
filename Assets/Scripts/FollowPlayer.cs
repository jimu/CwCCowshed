using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform player;
    Vector3 offset;
    Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
        terrain = GameObject.FindObjectOfType<Terrain>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = player.position + offset;
        pos.y = terrain.SampleHeight(pos);
        transform.position = pos;

    }
}
