using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] int customerCount = 2;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 1.5f;
    [SerializeField] float minimumSpawnTime = 0.2f;

    public int GetCustomerCount()
    {
        return customerCount;
    }

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(
            timeBetweenEnemySpawns - spawnTimeVariance,
            timeBetweenEnemySpawns + spawnTimeVariance
        );

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
