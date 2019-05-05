using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject paddlePrefab;
    [SerializeField] private GameObject foodPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var allSpawnPoints = GameObject
            .FindGameObjectsWithTag(Tags.CollectibleSpawnPoint)
            .ToList();
        var random = new System.Random();

        for (var i = 0; i < GameManager.MaxPaddle; ++i)
        {
            var spawnPoint = this.GetSpawnPoint(random, allSpawnPoints);
            Debug.Assert(spawnPoint != null, "No more spawn points");
            GameObject.Instantiate(this.paddlePrefab, spawnPoint.transform);
        }

        for (var i = 0; i < GameManager.MaxFood; ++i)
        {
            var spawnPoint = this.GetSpawnPoint(random, allSpawnPoints);
            Debug.Assert(spawnPoint != null, "No more spawn points");
            GameObject.Instantiate(this.foodPrefab, spawnPoint.transform);
        }
    }

    private GameObject GetSpawnPoint(
        System.Random random,
        List<GameObject> spawnPoints)
    {
        if (spawnPoints.Count > 0)
        {
            var idx = random.Next() % spawnPoints.Count;
            var spawnPoint = spawnPoints[idx];
            spawnPoints.RemoveAt(idx);
            return spawnPoint;
        }
        return null;
    }
}
