using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    float xRepeat = 1024 + 47;
    [SerializeField] float speed = 10;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float offset = (speed * Time.deltaTime - pos.x) % xRepeat;
        pos.x = -offset;
        transform.position = pos;
    }
}
