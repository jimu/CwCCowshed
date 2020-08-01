using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


#pragma warning disable 0649


public class Pool
{
    [SerializeField] GameObject prefab;
    GameObject[] pool;
    int next = 0;
    int size;

    public Pool(GameObject prefab, int size)
    {
        this.size = size;
        pool = new GameObject[size];

        for (int i = 0; i < size; ++ i)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            pool[i] = obj;
            obj.SetActive(false);
        }
    }

    public GameObject Get(Vector3 position)
    {
        for (int count = size; count > 0; --count)
        {
            GameObject obj = pool[next];
            next = (next + 1) % size; 
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                obj.transform.position = position;
               // Debug.Log("Get(" + obj.name + ")");
                return obj;
            }
        }
        return null;
    }
}
