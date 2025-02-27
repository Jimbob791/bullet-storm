using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<ItemObject> itemObjects = new List<ItemObject>();
    [SerializeField] GameObject charmPrefab;

    float t;
    float timeElasped;
    [SerializeField] float initialSpawnTime = 20;
    float charmSpawnInterval;

    private void Update()
    {
        t += Time.deltaTime;
        timeElasped += Time.deltaTime;

        if (t >= charmSpawnInterval)
        {
            t = 0;
            SpawnItemCharm();
        }

        charmSpawnInterval = Mathf.Pow(timeElasped / 60f, 2) + initialSpawnTime;
    }

    private void SpawnItemCharm()
    {
        // Create charm of three items
        List<ItemObject> charmItems = new List<ItemObject>();
        for (int i = 0; i < 3; i++)
        {
            charmItems.Add(itemObjects[Random.Range(0, itemObjects.Count)]);
        }

        // Spawn charm
        Vector2 spawnPos = new Vector2(Random.Range(-18f, 18f), Random.Range(-8f, 8f));
        GameObject newCharm = Instantiate(charmPrefab, spawnPos, Quaternion.identity);
        newCharm.GetComponent<GiftCharm>().items = charmItems;
    }
}