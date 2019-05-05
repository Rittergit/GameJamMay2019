using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip clip;

    public enum CollectibleType
    {
        Paddle,
        Food
    }

    [SerializeField] private CollectibleType type;

    void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerSpawn>();
        var audioSource = other.gameObject.GetComponent<AudioSource>();
        var isSlave = player != null
            && player.Type == PlayerSpawn.PlayerType.Slave;
        if (isSlave)
        {
            switch (this.type)
            {
                case CollectibleType.Paddle:
                    GameManager.Singleton.CollectPaddle();
                    audioSource.PlayOneShot(clip);
                    break;

                case CollectibleType.Food:
                    GameManager.Singleton.CollectFood();
                    audioSource.PlayOneShot(clip);
                    break;
            }

            GameObject.Destroy(this.gameObject);
        }
    }
}
