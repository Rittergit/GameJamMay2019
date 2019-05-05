using UnityEngine;
using UnityEngine.Networking;

public class PlayerSelectionManager : NetworkLobbyManager
{
    [SerializeField] private GameObject[] playerTypes;

    private int nextPlayerType;

    public override GameObject OnLobbyServerCreateGamePlayer(
        NetworkConnection conn,
        short playerControllerId)
    {
        Debug.Assert(this.nextPlayerType < this.playerTypes.Length);
        var playerType = this.playerTypes[this.nextPlayerType++];
        var player = Instantiate(playerType);
        return player;
    }

    public override void OnLobbyServerDisconnect(NetworkConnection conn)
    {
        this.ServerReturnToLobby();
    }
}
