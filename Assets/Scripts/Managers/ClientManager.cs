using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public const string GameReady = "ClientManager.GameReady";
    public const string CurrentPlayerSetEvent
        = "ClientManager.CurrentPlayerSet";

    public static ClientManager Singleton { get; private set; }

    void Awake()
    {
        Singleton = this;
        EventSystem.Publish(this, GameReady);
    }

    public GameObject CurrentPlayer { get; private set; }

    public void SetCurrentPlayer(GameObject player)
    {
        this.CurrentPlayer = player;
        EventSystem.Publish(this, CurrentPlayerSetEvent);
    }
}
