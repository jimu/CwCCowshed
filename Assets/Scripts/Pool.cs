using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    [SerializeField] T prefab;
    T[] pool;
    int next = 0;
    int size;

    Pool(int size)
    {
        this.size = size;
        pool = new T[size];

        for (int i = 0; i < size; ++ i)
        {
            T obj = GameObject.Instantiate(prefab) as T;
            pool[i] = obj;
            obj.gameObject.SetActive(false);
        }
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        for (int count = size; count > 0; --count)
        {
            T obj = pool[next];
            next = (next + 1) % size;
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }
        return null;
    }
}
