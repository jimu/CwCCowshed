using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class Tombstone : MonoBehaviour
{
    [SerializeField] TextMesh nameMesh;
    [SerializeField] TextMesh shadowMesh;
    [SerializeField] TextMesh dateMesh;
    [SerializeField] GameObject nameplate;

    private new string name;
    private float score;
    private string epitath;
    private string date;

    public void Set(string name, float score, string epitath, string date)
    {
        this.name = name;
        this.score = score;
        this.epitath = epitath;
        this.date = date;

        nameMesh.text = name;
        shadowMesh.text = name;
        dateMesh.text = date;

        MeshRenderer renderer = nameMesh.GetComponent<MeshRenderer>();
        Vector3 size = nameplate.transform.localScale;
        size.x = renderer.bounds.size.x / 1.8f;
        nameplate.transform.localScale = size;
    }

}
