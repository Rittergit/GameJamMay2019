using UnityEngine;
using UnityEngine.Networking;

public class NetworkLobbyHUD : MonoBehaviour
{
    public string IpAddress;
    public string Port;
    public string TestScene;
    public NetworkLobbyManager networkLobbyManager;
    private bool connected = false;
    private bool showDisconnect = false;

    private string playScene;

    void Start()
    {
        playScene = networkLobbyManager.playScene;
    }

    void Update()
    {
        if (connected && Input.GetButtonUp("Pause"))
        {
            showDisconnect = !showDisconnect;
        }
    }

    public void OnGUI()
    {
        if (!connected)
        {
            if (GUILayout.Button("Host"))
            {
                connected = true;
                networkLobbyManager.playScene = this.playScene;
                networkLobbyManager.networkPort = int.Parse(Port);
                networkLobbyManager.StartHost();
            }

            IpAddress = GUILayout.TextField(IpAddress, GUILayout.Width(100));
            Port = GUILayout.TextField(Port, 5);
            if (GUILayout.Button("Connect"))
            {
                connected = true;
                networkLobbyManager.networkAddress = IpAddress;
                networkLobbyManager.networkPort = int.Parse(Port);
                networkLobbyManager.StartClient();
            }

            if (GUILayout.Button("Test"))
            {
                connected = true;
                networkLobbyManager.playScene = TestScene;
                networkLobbyManager.networkPort = int.Parse(Port);
                networkLobbyManager.StartHost();
            }
        }
        else
        {
            if (showDisconnect)
            {
                if (GUILayout.Button("Disconnect"))
                {
                    connected = false;
                    networkLobbyManager.StopHost();
                }
            }
        }
    }
}