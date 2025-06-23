using UnityEngine;

public class CandleSpawner : MonoBehaviour
{
    public GameObject candlePrefab;
    public Transform[] spawnPoints;
    public float timer;

    private void Start()
    {
        Invoke("SpawnCandle", timer);
    }
    void SpawnCandle()
    {
        int index = Random.Range(0, spawnPoints.Length-1);
        Instantiate(candlePrefab, spawnPoints[index]);

        Invoke("SpawnCandle", timer);
    }
}
