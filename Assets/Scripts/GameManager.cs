using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const string HealthChangeEvent = "GameManager.HealthChange";

    private const int MaxHealth = 3;

    public static GameManager Singleton { get; private set; }

    void Awake()
    {
        Singleton = this;
        this.TotalHealth = MaxHealth;
        this.CurrentHealth = MaxHealth;
    }

    void OnDestroy()
    {
        Singleton = null;
    }

    public int TotalHealth { get; private set; }

    public int CurrentHealth { get; private set; }

    public void DamageSlave()
    {
        --this.CurrentHealth;
        EventSystem.Publish(this, HealthChangeEvent);
    }
}
