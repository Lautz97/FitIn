using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCycle());
    }

    private IEnumerator SpawnCycle()
    {
        for (int i = 0; i < 1;)
        {
            SpawnObstacle(Random.Range(0f, 1f));
            yield return new WaitForSecondsRealtime(3f);
        }
    }

    void SpawnObstacle(float a)
    {
        GameObject newObs = Instantiate(obstacle, spawnPoint.position, spawnPoint.rotation);
        newObs.transform.parent = transform;
        newObs.GetComponent<ObstacleBehaviour>().SetDimensions(a);

    }
}
