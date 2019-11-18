using System;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameManager : NetworkBehaviour
{
    [SyncVar(hook = nameof(GameOverHookInternal))]
    public bool gameOver = false;
    [SyncVar(hook = nameof(WinnerHookInternal))]
    public GameManager.WinnerType winner = GameManager.WinnerType.None;
    [SyncVar(hook = nameof(HealthHookInternal))]
    public int currentHealth = GameManager.MaxHealth;
    [SyncVar(hook = nameof(PaddleHookInternal))]
    public int currentPaddles = 0;
    [SyncVar(hook = nameof(FoodHookInternal))]
    public int currentFood = 0;

    public Action<bool> GameOverHook { get; set; }
    public Action<GameManager.WinnerType> WinnerHook { get; set; }
    public Action<int> HealthHook { get; set; }
    public Action<int> PaddleHook { get; set; }
    public Action<int> FoodHook { get; set; }

    private void GameOverHookInternal(bool value)
    {
        Debug.Log("GameOverHookInternal: " + value);
        this.GameOverHook(value);
    }

    private void WinnerHookInternal(GameManager.WinnerType value)
    {
        Debug.Log("WinnerHookInternal: " + value);
        this.WinnerHook(value);
    }

    private void HealthHookInternal(int value)
    {
        Debug.Log("HealthHookInternal: " + value);
        this.HealthHook(value);
    }

    private void PaddleHookInternal(int value)
    {
        Debug.Log("PaddleHookInternal: " + value);
        this.PaddleHook(value);
    }

    private void FoodHookInternal(int value)
    {
        Debug.Log("FoodHookInternal: " + value);
        this.FoodHook(value);
    }
}
