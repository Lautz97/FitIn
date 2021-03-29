using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject obstacle;

    [SerializeField] private Renderer floor, road;
    [SerializeField] private float speed;

    private void Start()
    {
        StartCoroutine(SpawnCycle());
    }

    private IEnumerator SpawnCycle()
    {
        SpawnObstacle(Random.Range(0f, 1f));
        yield return new WaitForSecondsRealtime(3f);
        if (Time.timeScale != 0) StartCoroutine(SpawnCycle());
    }

    private void Update()
    {
        floor.material.mainTextureOffset = new Vector2(0, (-speed / 2) * Time.time);
        road.material.mainTextureOffset = new Vector2(0, -speed * Time.time);
    }

    void SpawnObstacle(float a)
    {
        GameObject newObs = Instantiate(obstacle, spawnPoint.position, spawnPoint.rotation);
        newObs.transform.parent = transform;
        newObs.GetComponent<ObstacleBehaviour>().SetDimensions(a);

    }
}
