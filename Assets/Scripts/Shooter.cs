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

    public string Fire
    {
        get
        {
            return GameManager.Singleton.IsSplitscreen ? "Fire2" : "Fire";
        }
    }

    void Update()
    {
        if (!isLocalPlayer && !GameManager.Singleton.IsSplitscreen)
        {
            return;
        }

        //Timer is running. Tick tick .... tok
        timer += Time.deltaTime;

        if (timer >= shootDelay && Input.GetButtonDown(this.Fire))
        {
            //Set Trigger Animation
            this.animator.SetTrigger("shoot");

            if (GameManager.Singleton.IsSplitscreen)
                Shoot();
            else
                CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = this.Shoot();

        NetworkServer.Spawn(bullet);
    }

    GameObject Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        //Set Trigger Animation
        this.animator.SetTrigger("shoot");

        //Playing Shoot Effect
        shootEffect.Play();

        //reset Timer
        timer = 0f;

        return bullet;
    }
}
