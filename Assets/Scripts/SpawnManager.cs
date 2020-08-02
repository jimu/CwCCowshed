using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    //[SerializeField] GameObject obstaclePrefab = null;
    [SerializeField] float startDelay = 5f;
    //[SerializeField] float repeatRate = 3f;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    [SerializeField] float xOffset = 20f;
    Terrain terrain;
    Transform player;

    private int poolSize = 3;
    private Pool[] pools;

    [SerializeField] float minDelay = 1.6f;
    [SerializeField] float maxDelay = 3.5f;


    void Start()
    {
        Debug.Log("SpawnManager.Start()");
        terrain = GameObject.FindObjectOfType<Terrain>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        BuildPools();
        Invoke("RandomInvoker", startDelay);
        Debug.Log("SpawnManager.Start() - Exit");
    }

    void BuildPools()
    {
        int numPools = obstacles.Length;
        pools = new Pool[numPools];

        for(int i = 0; i < numPools; ++i)
            pools[i] = new Pool(obstacles[i], poolSize);
    }

    void RandomInvoker()
    {
        if (GameManager.instance.Running)
        {
            SpawnObstacle();
        }
        Invoke("RandomInvoker", Random.Range(minDelay, maxDelay));
    }


    void SpawnObstacle()
    {
        int nPrefab = Random.Range(0, obstacles.Length);
        Vector3 position = new Vector3(player.position.x + xOffset, 0, player.position.z);
        position.y = terrain.SampleHeight(position) + 0.3f;

        RaycastHit hit;
        Physics.Raycast(position, Vector3.down, out hit);
        position.y = terrain.SampleHeight(position);

        //GameObject obstacle = Instantiate(obstacles[nPrefab], position, obstacles[nPrefab].transform.rotation);
        GameObject o = pools[nPrefab].Get(position);
        if (o)
            o.transform.up -= (transform.up - hit.normal) * 0.4f;
        else
            Debug.Log("WTF: pools return NULL");

        //Debug.Log("SpawnObstacle: " + obstacles[nPrefab].name + " " + position);
        //obstacle.transform.up -= (transform.up - hit.normal) * 0.4f;
    }


}
