using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;
    [SerializeField] private ObjectPooler entityPooler;
    [SerializeField] private float minSpawnRadius;
    [SerializeField] private float maxSpawnRadius;
    [SerializeField] private Player player;
    private int maxUnit;
    private int currentUnitCount;

    // Start is called before the first frame update
    void Start()
    {
        maxUnit = entityPooler.amountToPool;
        StartCoroutine(SpawnCoroutine());
    }

    /// <summary>
    /// Coroutine that activates a certain amount of entities and initializes their position at a random place around the player
    /// </summary>
    /// <returns>SpawnCoroutine</returns>
    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
        if (currentUnitCount < maxUnit)
        {
            currentUnitCount++;
            GameObject pooledEntity = entityPooler.GetPooledObject();
            pooledEntity.SetActive(true);
            Vector3 playerPos = player.transform.position;
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            pooledEntity.transform.position = playerPos + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * radius;
        }
        yield return SpawnCoroutine();
    }
}
