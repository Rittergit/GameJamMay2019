using UnityEngine;
using UnityEngine.Networking;

public class Shooter : NetworkBehaviour
{   
    [Tooltip("Time delay between to shoot. 1.0f = 1 seconds")]
    public float shootDelay = 1f;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public ParticleSystem shootEffect;
    [SerializeField] private Animator animator;
  
    private float timer;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //Timer is running. Tick tick .... tok
        timer += Time.deltaTime;

        if (timer >= shootDelay && Input.GetButtonDown("Fire"))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        NetworkServer.Spawn(bullet);

        //Set Trigger Animation
        this.animator.SetTrigger("shoot");

        //Playing Shoot Effect
        shootEffect.Play();

        //reset Timer
        timer = 0f;
    }
}
