using UnityEngine;
using UnityEngine.Networking;

public class Boat : MonoBehaviour
{
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
