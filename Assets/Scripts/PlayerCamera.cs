using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player = null;
    
    private Vector3 fromPlayerToCamera;

    void Start()
    {
        this.fromPlayerToCamera = this.transform.position;
        EventSystem.Subscribe(
            ClientManager.CurrentPlayerSetEvent,
            this.OnCurrentPlayerSet);
        if (ClientManager.Singleton.CurrentPlayer != null)
        {
            this.player = ClientManager.Singleton.CurrentPlayer.transform;
        }
    }

    void OnDestroy()
    {
        EventSystem.Unsubscribe(
            ClientManager.CurrentPlayerSetEvent,
            this.OnCurrentPlayerSet);
    }

    void Update()
    {
        if (this.player != null)
        {
            var position = this.player.position + this.fromPlayerToCamera;
            this.transform.position = position;
        }
    }

    private void OnCurrentPlayerSet(object sender, EventArgs e)
    {
        this.player = ClientManager.Singleton.CurrentPlayer.transform;
    }
}
