using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [Header("Customer Pool Settings")]
    [SerializeField] GameObject customerPrefab;
    [SerializeField] [Range(0, 20)] int poolSize = 10;
    GameObject[] pool;
    int currentCustomerIndex = 0;

    [Header("Wave Settings")]
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 2f;
    WaveConfigSO currentWave;

    void Awake()
    {
        PopulatePool();
    }

    void EnableCustomerInPool(int currentCustomer)
    {
        if (pool[currentCustomer].activeInHierarchy == false )
        {
            pool[currentCustomer].transform.position = currentWave.GetStartingWaypoint().position;
            pool[currentCustomer].SetActive(true);
            return;
        }
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(customerPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    public void SpawnCustomers()
    {
        StartCoroutine(SpawnCustomerWaves());
    }

    IEnumerator SpawnCustomerWaves()
    {
        foreach(WaveConfigSO wave in waveConfigs)
        {   
            currentWave = wave;

            for (int i = 0; i < currentWave.GetCustomerCount(); i++)
            {
                if (currentCustomerIndex == pool.Length)
                {
                    currentCustomerIndex = 0;
                }

                EnableCustomerInPool(currentCustomerIndex);
                Customer currentCustomer = pool[currentCustomerIndex].GetComponent<Customer>();
                while (currentCustomer.GetHasNotOrdered())
                {
                    yield return null;
                }
                currentCustomerIndex++;
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}