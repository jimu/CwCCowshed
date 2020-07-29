﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
