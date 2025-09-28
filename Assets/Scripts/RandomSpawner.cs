using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn;      // The object that will fall
    public float spawnInterval = 2f;      // How often to spawn (seconds)
    public float spawnRangeX = 8f;        // How wide to spread across the screen (left-right)

    [Header("Spawn Area")]
    public float spawnHeight = 6f;        // Height above the screen where objects spawn

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 10f);
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
