using UnityEngine;
using UnityEngine.Networking;

public class Splitscreen : MonoBehaviour
{
    const string Player1Name = "PLAYER_1";
    const string Player2Name = "PLAYER_2";
    const string Camera1Name = "CAMERA_1";
    const string Camera2Name = "CAMERA_2";

    [SerializeField] private GameObject playerPrefab1;
    [SerializeField] private GameObject playerPrefab2;
    [SerializeField] private GameObject cameraPrefab;

    public void StartSplitscreen()
    {
        var p1 = this.CreatePlayer(this.playerPrefab1, Player1Name, false);
        this.CreateCamera(Camera1Name, false, p1);
        var p2 = this.CreatePlayer(this.playerPrefab2, Player2Name, true);
        this.CreateCamera(Camera2Name, true, p2);
    }

    private GameObject CreatePlayer(
        GameObject prefab,
        string name,
        bool isPlayer2)
    {
        var player = GameObject.Instantiate(prefab);
        player.name = name;

        var playerMovement = player.GetComponent<PlayerMovement>();
        Debug.Assert(playerMovement != null);
        playerMovement.enabled = true;
        if (playerMovement)
            playerMovement.isPlayer2 = isPlayer2;

        var networkIdent = player.GetComponent<NetworkIdentity>();
        Debug.Assert(networkIdent != null);
        networkIdent.enabled = false;
        var networkTransform = player.GetComponent<NetworkTransform>();
        Debug.Assert(networkTransform != null);
        networkTransform.enabled = false;

        return player;
    }

    private void CreateCamera(string name, bool isPlayer2, GameObject player)
    {
        var camera = Instantiate(this.cameraPrefab);
        camera.name = name;

        var playerCamera = camera.GetComponent<PlayerCamera>();
        Debug.Assert(playerCamera != null);
        playerCamera.SetCurrentPlayer(player.GetComponent<PlayerSpawn>());

        var multiplayerCamera = camera.GetComponent<MultiplayerCamera>();
        Debug.Assert(multiplayerCamera != null);
        multiplayerCamera.isSplitscreen = true;
        multiplayerCamera.isPlayer2 = isPlayer2;
        multiplayerCamera.ApplyConfiguration();

        if (isPlayer2)
        {
            var listener = multiplayerCamera.GetComponent<AudioListener>();
            if (listener != null)
                GameObject.Destroy(listener);
        }
    }
}
