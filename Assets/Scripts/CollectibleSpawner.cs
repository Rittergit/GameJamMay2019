using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

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
            var paddle = GameObject.Instantiate(
                this.paddlePrefab,
                spawnPoint.transform);

            if (!GameManager.Singleton.IsSplitscreen)
                NetworkServer.Spawn(paddle);
        }

        for (var i = 0; i < GameManager.MaxFood; ++i)
        {
            var spawnPoint = this.GetSpawnPoint(random, allSpawnPoints);
            Debug.Assert(spawnPoint != null, "No more spawn points");
            var food = GameObject.Instantiate(
                this.foodPrefab,
                spawnPoint.transform);

            if (!GameManager.Singleton.IsSplitscreen)
                NetworkServer.Spawn(food);
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
