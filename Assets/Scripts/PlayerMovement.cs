﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 3f;
    public AudioClip footStep;
    public bool isPlayer2 = false;

    public Rigidbody playerRigidbody;
    AudioSource audioSource;

    private string Horizontal
    {
        get { return this.isPlayer2 ? "Horizontal2" : "Horizontal"; }
    }

    private string Vertical
    {
        get { return this.isPlayer2 ? "Vertical2" : "Vertical"; }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (footStep)
        {
            audioSource.clip = footStep;
        }
    }
    void FixedUpdate()
    {
        CharacterMovement();
    }

    //MOVEMENT
    void CharacterMovement()
    {
        var horizontal = Input.GetAxis(this.Horizontal);
        var vertical = Input.GetAxis(this.Vertical);
        var direction = new Vector3(horizontal, 0f, vertical);
        if (direction.magnitude > 0)
        {
            playerRigidbody.rotation = Quaternion.LookRotation(direction);
        }

        var movement = Vector3.ClampMagnitude(direction, 1f) * this.speed;
        playerRigidbody.velocity = movement;

        if (horizontal == 0 && vertical == 0)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
}
