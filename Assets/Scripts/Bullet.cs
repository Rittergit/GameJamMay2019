using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        this.GetComponent<Rigidbody>().velocity
            = this.transform.forward * this.speed;
        Destroy(this.gameObject, 2);
    }

    void OnCollisionEnter(Collision other)
    {
        var player = other.gameObject.GetComponent<PlayerSpawn>();
        var isSlave = player != null
            && player.Type == PlayerSpawn.PlayerType.Slave;
        if (isSlave)
        {
            Destroy(this.gameObject);
            GameManager.Singleton.DamageSlave();
        }
    }
}
