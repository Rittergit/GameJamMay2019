using UnityEngine;
using UnityEngine.Networking;

public class PlayerSelectionManager : NetworkLobbyManager
{
    [SerializeField] private GameObject[] playerTypes;

    private int nextPlayerType;

    public override void OnServerAddPlayer(
        NetworkConnection conn,
        short playerControllerId)
    {
        Debug.Assert(this.nextPlayerType < this.playerTypes.Length);
        var playerType = this.playerTypes[this.nextPlayerType++];
        var player = Instantiate(playerType);
        NetworkServer.AddPlayerForConnection(
            conn,
            player,
            playerControllerId);
    }
}
