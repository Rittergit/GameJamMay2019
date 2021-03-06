﻿using System;
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
            var playerSpawn = this.player.gameObject.GetComponent<PlayerSpawn>();
            if (playerSpawn.Type == PlayerSpawn.PlayerType.Landlord)
            {
                this.fromPlayerToCamera = this.fromPlayerToCamera * 1.5f;
            }
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

    public void SetCurrentPlayer(PlayerSpawn player)
    {
        this.player = player.transform;
        if (player.Type == PlayerSpawn.PlayerType.Landlord)
            this.fromPlayerToCamera = this.fromPlayerToCamera * 1.5f;
    }

    private void OnCurrentPlayerSet(object sender, EventArgs e)
    {
        var playerSpawn = ClientManager.Singleton.CurrentPlayer
            .GetComponent<PlayerSpawn>();
        this.SetCurrentPlayer(playerSpawn);
    }
}
