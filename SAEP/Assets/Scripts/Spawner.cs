using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private float startDelay = 2f;
    private float repeatRate = 2f;

    private Vector3 spawnPos = new Vector3(25, 0, 0);

    private PlayerControl playerControlScript;

    // Start is called before the first frame update
    void Start()
    {
        //Get the actual PlayerControl script
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if (playerControlScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
        

    }
}
