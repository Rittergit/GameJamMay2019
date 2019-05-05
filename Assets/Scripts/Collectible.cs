using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        Paddle,
        Food
    }

    [SerializeField] private CollectibleType type;

    void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerSpawn>();
        var isSlave = player != null
            && player.Type == PlayerSpawn.PlayerType.Slave;
        if (isSlave)
        {
            switch (this.type)
            {
                case CollectibleType.Paddle:
                    GameManager.Singleton.CollectPaddle();
                    break;

                case CollectibleType.Food:
                    GameManager.Singleton.CollectFood();
                    break;
            }

            GameObject.Destroy(this.gameObject);
        }
    }
}
