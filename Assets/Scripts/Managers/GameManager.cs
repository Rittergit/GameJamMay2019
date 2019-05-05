using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public enum WinnerType
    {
        None,
        Slave,
        Landlord
    }

    public const string GameOverEvent = "GameManager.GameOver";
    public const string HealthChangeEvent = "GameManager.HealthChange";
    public const string PaddleChangeEvent = "GameManager.PaddleChange";
    public const string FoodChangeEvent = "GameManager.FoodChange";

    public const int MaxHealth = 3;
    public const int MaxPaddle = 1;
    public const int MaxFood = 5;

    public static GameManager Singleton { get; private set; }

    [SyncVar(hook = nameof(GameOverHook))]
    private bool gameOver = false;
    [SyncVar]
    private WinnerType winner = WinnerType.None; 
    [SyncVar(hook = nameof(HealthHook))]
    private int currentHealth = MaxHealth;
    [SyncVar(hook = nameof(PaddleHook))]
    private int currentPaddles = 0;
    [SyncVar(hook = nameof(FoodHook))]
    private int currentFood = 0;

    void Awake()
    {
        Singleton = this;
    }

    void OnDestroy()
    {
        Singleton = null;
    }

    public WinnerType CurrentWinner { get { return this.winner; } }

    public int CurrentHealth { get { return this.currentHealth; } }

    public int CurrentPaddles { get { return this.currentPaddles; } }

    public int CurrentFood { get { return this.currentFood; } }

    public void DamageSlave()
    {
        --this.currentHealth;
        EventSystem.Publish(this, HealthChangeEvent);

        if (this.currentHealth == 0)
        {
            this.winner = WinnerType.Landlord;
            this.gameOver = true;
        }
    }

    public void TrySlaveEscape()
    {
        if (this.currentPaddles == MaxPaddle && this.currentFood == MaxFood)
        {
            this.winner = WinnerType.Slave;
            this.gameOver = true;
        }
    }

    public void CollectPaddle()
    {
        ++this.currentPaddles;
        EventSystem.Publish(this, PaddleChangeEvent);
    }

    public void CollectFood()
    {
        ++this.currentFood;
        EventSystem.Publish(this, FoodChangeEvent);
    }

    private void GameOverHook(bool value)
    {
        if (value)
            EventSystem.Publish(this, GameOverEvent);
    }

    private void WinnerHook(WinnerType value)
    {
        if (this.gameOver)
            EventSystem.Publish(this, GameOverEvent);
    }

    private void HealthHook(int value)
    {
        EventSystem.Publish(this, HealthChangeEvent);
    }

    private void PaddleHook(int value)
    {
        EventSystem.Publish(this, PaddleChangeEvent);
    }

    private void FoodHook(int value)
    {
        EventSystem.Publish(this, FoodChangeEvent);
    }
}
