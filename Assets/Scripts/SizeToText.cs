using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class SizeToText : MonoBehaviour
{

    [SerializeField] MeshRenderer textMesh;

    // Start is called before the first frame update
    void Awake()
    {
        Vector3 size = transform.localScale;
        size.x = textMesh.bounds.size.x / 1.8f;
        transform.localScale = size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
