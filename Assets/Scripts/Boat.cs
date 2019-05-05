﻿using UnityEngine;

public class Boat : MonoBehaviour
{
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
            this.transform.position = spawnPoint.transform.position;
            this.transform.rotation = spawnPoint.transform.rotation;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerSpawn>();
        var isSlave = player != null
            && player.Type == PlayerSpawn.PlayerType.Slave;
        if (isSlave)
        {
            GameManager.Singleton.TrySlaveEscape();
        }
    }
}
