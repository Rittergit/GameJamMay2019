using UnityEngine;
using UnityEngine.Networking;

public class BoatSpawn : MonoBehaviour
{
    public GameObject boatPrefab;

    void Start()
    {
        var allSpawnPoints =
            GameObject.FindGameObjectsWithTag(Tags.BoatSpawnPoint);

        var random = new System.Random();
        var spawnPoint = allSpawnPoints.Length > 0
            ? allSpawnPoints[random.Next() % allSpawnPoints.Length]
            : null;

        Debug.Assert(spawnPoint != null, "No spawn points for boat");
        if (spawnPoint != null)
        {
            GameObject boat = Instantiate(boatPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

            NetworkServer.Spawn(boat);
        }
    }
}
