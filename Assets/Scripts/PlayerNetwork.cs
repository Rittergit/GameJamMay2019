using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetwork : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        this.GetComponent<PlayerMovement>().enabled = true;
        ClientManager.Singleton.SetCurrentPlayer(this.gameObject);
    }
}
