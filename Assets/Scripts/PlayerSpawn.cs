using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public enum PlayerType
    {
        Landlord,
        Slave
    }

    [SerializeField] private PlayerType playerType;

    public PlayerType Type { get { return this.playerType; } }

    void Start()
    {
         string tag;
         switch (this.playerType)
         {
             case PlayerType.Landlord:
                tag = Tags.LandlordSpawnPoint;
                break;

            case PlayerType.Slave:
                tag = Tags.SlaveSpawnPoint;
                break;

            default:
                tag = null;
                break; 
         }

        var allSpawnPoints =
            GameObject.FindGameObjectsWithTag(tag);

        var random = new System.Random();
        var spawnPoint = allSpawnPoints.Length > 0
            ? allSpawnPoints[random.Next() % allSpawnPoints.Length]
            : null;

        Debug.Assert(
            spawnPoint != null,
            "No spawn points for " + this.playerType.ToString());
        if (spawnPoint != null)
        {
            this.transform.position = spawnPoint.transform.position;
            this.transform.rotation = spawnPoint.transform.rotation;
        }
    }
}
