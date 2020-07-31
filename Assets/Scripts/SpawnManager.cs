using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    //[SerializeField] GameObject obstaclePrefab = null;
    [SerializeField] float startDelay = 3f;
    //[SerializeField] float repeatRate = 3f;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    [SerializeField] float xOffset = 20f;
    Terrain terrain;
    Transform player;

    [SerializeField] float minDelay = 0.6f;
    [SerializeField] float maxDelay = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SpawnManager.Start()");
        terrain = GameObject.FindObjectOfType<Terrain>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("RandomInvoker", startDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomInvoker()
    {
        Debug.Log("RandomInvoker()");
        SpawnObstacle();
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

        GameObject obstacle = Instantiate(obstacles[nPrefab], position, obstacles[nPrefab].transform.rotation);
        Debug.Log("SpawnObstacle: " + obstacles[nPrefab].name + " " + position);
        obstacle.transform.up -= (transform.up - hit.normal) * 0.4f;
    }
}
