using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientManager : MonoBehaviour
{
    public const string GameReady = "ClientManager.GameReady";
    public const string CurrentPlayerSetEvent
        = "ClientManager.CurrentPlayerSet";

    private const string MenuScene = "Menu";

    public static ClientManager Singleton { get; private set; }

    void Awake()
    {
        Singleton = this;
        EventSystem.Publish(this, GameReady);
    }

    void Update()
    {
        if (Input.GetButton("Cancel"))
            SceneManager.LoadScene(MenuScene);
    }

    public GameObject CurrentPlayer { get; private set; }

    public void SetCurrentPlayer(GameObject player)
    {
        this.CurrentPlayer = player;
        EventSystem.Publish(this, CurrentPlayerSetEvent);
    }
}
