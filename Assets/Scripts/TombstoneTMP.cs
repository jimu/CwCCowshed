using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#pragma warning disable 0649

public class TombstoneTMP : MonoBehaviour
{
    [SerializeField] MeshRenderer nameMesh;
    [SerializeField] TextMeshPro nameText;
    [SerializeField] TextMeshPro shadowText;
    [SerializeField] TextMeshPro dateText;
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

        nameText.text = name;
        shadowText.text = name;
        dateText.text = date;

        MeshRenderer renderer = nameMesh.GetComponent<MeshRenderer>();
        Vector3 size = nameplate.transform.localScale;
        size.x = renderer.bounds.size.x / 1.8f;
        nameplate.transform.localScale = size;
    }

}
