using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    private Vector3 fromPlayerToCamera;

    void Awake()
    {
        this.fromPlayerToCamera =
            this.transform.position - this.player.transform.position;
    }

    void Update()
    {
        var position = this.player.position + this.fromPlayerToCamera;
        this.transform.position = position;
    }
}
