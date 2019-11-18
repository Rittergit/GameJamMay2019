using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum WinnerType
    {
        None,
        Slave,
        Landlord
    }

    public const string GameReady = "ClientManager.GameReady";
    public const string GameOverEvent = "GameManager.GameOver";
    public const string HealthChangeEvent = "GameManager.HealthChange";
    public const string PaddleChangeEvent = "GameManager.PaddleChange";
    public const string FoodChangeEvent = "GameManager.FoodChange";

    public const int MaxHealth = 3;
    public const int MaxPaddle = 1;
    public const int MaxFood = 5;

    [SerializeField] private NetworkGameManager networkGameManager;

    private bool gameOver = false;
    private GameManager.WinnerType winner = GameManager.WinnerType.None;
    private int currentHealth = GameManager.MaxHealth;
    private int currentPaddles = 0;
    private int currentFood = 0;

    public static GameManager Singleton { get; private set; }

    void Awake()
    {
        Singleton = this;
        EventSystem.Publish(this, GameReady);
    }

    void Start()
    {
        if (this.networkGameManager != null)
        {
            this.networkGameManager.GameOverHook = this.GameOverHook;
            this.networkGameManager.WinnerHook = this.WinnerHook;
            this.networkGameManager.HealthHook = this.HealthHook;
            this.networkGameManager.PaddleHook = this.PaddleHook;
            this.networkGameManager.FoodHook = this.FoodHook;
        }
    }

    void OnDestroy()
    {
        Singleton = null;
    }

    public WinnerType CurrentWinner { get { return this.winner; } }

    public int CurrentHealth { get { return this.currentHealth; } }

    public int CurrentPaddles { get { return this.currentPaddles; } }

    public int CurrentFood { get { return this.currentFood; } }

    public bool IsSplitscreen { get; set; } = false;

    public void DamageSlave()
    {
        if (this.IsSplitscreen || (this.networkGameManager?.isServer ?? true))
        {
            --this.currentHealth;
            --this.networkGameManager.currentHealth;
            EventSystem.Publish(this, HealthChangeEvent);

            if (this.currentHealth == 0)
            {
                this.winner = WinnerType.Landlord;
                this.networkGameManager.winner = WinnerType.Landlord;
                this.gameOver = true;
                this.networkGameManager.gameOver = true;
                EventSystem.Publish(this, GameOverEvent);
            }
        }
    }

    public void TrySlaveEscape()
    {
        if (this.IsSplitscreen || (this.networkGameManager?.isServer ?? true))
        {
            if (this.currentPaddles == MaxPaddle && this.currentFood == MaxFood)
            {
                this.winner = WinnerType.Slave;
                this.networkGameManager.winner = WinnerType.Slave;
                this.gameOver = true;
                this.networkGameManager.gameOver = true;
                EventSystem.Publish(this, GameOverEvent);
            }
        }
    }

    public void CollectPaddle()
    {
        if (this.IsSplitscreen || (this.networkGameManager?.isServer ?? true))
        {
            ++this.currentPaddles;
            ++this.networkGameManager.currentPaddles;
            EventSystem.Publish(this, PaddleChangeEvent);
        }
    }

    public void CollectFood()
    {
        if (this.IsSplitscreen || (this.networkGameManager?.isServer ?? true))
        {
            ++this.currentFood;
            ++this.networkGameManager.currentFood;
            EventSystem.Publish(this, FoodChangeEvent);
        }
    }

    private void GameOverHook(bool value)
    {
        this.gameOver = value;
        if (value)
            EventSystem.Publish(this, GameOverEvent);
    }

    private void WinnerHook(WinnerType value)
    {
        this.winner = value;
        if (this.gameOver)
            EventSystem.Publish(this, GameOverEvent);
    }

    private void HealthHook(int value)
    {
        this.currentHealth = value;
        EventSystem.Publish(this, HealthChangeEvent);
    }

    private void PaddleHook(int value)
    {
        this.currentPaddles = value;
        EventSystem.Publish(this, PaddleChangeEvent);
    }

    private void FoodHook(int value)
    {
        this.currentFood = value;
        EventSystem.Publish(this, FoodChangeEvent);
    }
}
