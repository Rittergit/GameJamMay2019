using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform player;

    void Update()
    {
        var position = this.player.position;
        this.transform.position = position + new Vector3(0f, 5f, -6f);

        var rotation = this.player.rotation;
        this.transform.rotation = rotation * Quaternion.Euler(35f, 0f, 0f);
    }
}
