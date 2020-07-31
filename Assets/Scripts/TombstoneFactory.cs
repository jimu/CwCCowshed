using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649


public class TombstoneFactory : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    Terrain terrain;

    const float Z_POSITION = 5.3f;
    const float Z_MIN = 4f;
    const float Z_MAX = 14f;

    void Awake()
    {
        terrain = GameObject.FindObjectOfType<Terrain>();
    }


    public GameObject Create(string name, float distance, string epitath, string date)
    {
        Vector3 pos = new Vector3(distance, 0, Random.Range(Z_MIN, Z_MAX));
        float height = terrain.SampleHeight(pos);
        pos.y = height;
        GameObject tombstone = Instantiate(prefab, pos, Quaternion.identity);
        //tombstone.GetComponent<Tombstone>().Set(name, distance, epitath, date);
        Tombstone t = tombstone.GetComponent<Tombstone>();
        if (t != null)
            t.Set(name, distance, epitath, date);
        else
            tombstone.GetComponent<TombstoneTMP>().Set(name, distance, epitath, date);

        return tombstone;
    }
}
