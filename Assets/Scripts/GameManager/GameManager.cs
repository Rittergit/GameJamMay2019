using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const string HealthChangeEvent = "GameManager.HealthChange";
    public const string PaddleChangeEvent = "GameManager.PaddleChange";
    public const string FoodChangeEvent = "GameManager.FoodChange";

    public const int MaxHealth = 3;
    public const int MaxPaddle = 1;
    public const int MaxFood = 5;

    public static GameManager Singleton { get; private set; }

    void Awake()
    {
        Singleton = this;
    }

    void OnDestroy()
    {
        Singleton = null;
    }

    public int CurrentHealth { get; private set; } = MaxHealth;

    public int CurrentPaddles { get; private set; } = 0;

    public int CurrentFood { get; private set; } = 0;

    public void DamageSlave()
    {
        --this.CurrentHealth;
        EventSystem.Publish(this, HealthChangeEvent);
    }

    public void CollectPaddle()
    {
        ++this.CurrentPaddles;
        EventSystem.Publish(this, PaddleChangeEvent);
    }

    public void CollectFood()
    {
        ++this.CurrentFood;
        EventSystem.Publish(this, FoodChangeEvent);
    }
}
