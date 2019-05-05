using UnityEngine;
using UnityEngine.Networking;

public class NetworkLobbyHUD : MonoBehaviour
{
    public string IpAddress;
    public string Port;
    private bool connected = false;
    private bool showDisconnect = false;

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
                NetworkLobbyManager.singleton.networkPort = int.Parse(Port);
                NetworkLobbyManager.singleton.StartHost();
            }

            IpAddress = GUILayout.TextField(IpAddress, GUILayout.Width(100));
            Port = GUILayout.TextField(Port, 5);
            if (GUILayout.Button("Connect"))
            {
                connected = true;
                NetworkLobbyManager.singleton.networkAddress = IpAddress;
                NetworkLobbyManager.singleton.networkPort = int.Parse(Port);
                NetworkLobbyManager.singleton.StartClient();
            }
        }
        else
        {
            if (showDisconnect)
            {
                if (GUILayout.Button("Disconnect"))
                {
                    connected = false;
                    NetworkLobbyManager.singleton.StopHost();
                }
            }
        }
    }
}