﻿using UnityEngine;
using UnityEngine.Networking;

public class Shooter : NetworkBehaviour
{
    [Tooltip("Time delay between to shoot. 1.0f = 1 seconds")]
    public float shootDelay = 1f;
    public float bulletSpeed = 5f;
    public GameObject bulletPrefab;
    public AudioClip shootSound;
    public Transform shootPoint;
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

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2);

        //Set Trigger Animation
        this.animator.SetTrigger("shoot");

        //FX
        if (shootSound)
        {
            //Play Sound Effect here
        }

        //reset Timer
        timer = 0f;
    }
}
