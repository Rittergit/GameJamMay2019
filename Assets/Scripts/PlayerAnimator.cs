using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimator : MonoBehaviour
{
    public Animator playerAnimator;

    private PlayerMovement playerMovement;
    private NetworkIdentity networkIdentity;

    void Start()
    {
        this.playerMovement = this.GetComponent<PlayerMovement>();
        this.networkIdentity = this.GetComponent<NetworkIdentity>();
    }

    void Update()
    {
        check();
    }

    void check()
    {
        if (this.networkIdentity != null
            && !this.networkIdentity.isLocalPlayer
            && (GameManager.Singleton == null || !GameManager.Singleton.IsSplitscreen))
        {
            return;
        }

        var currentMovement = this.playerMovement.CurrentMovement;
        var horizontal = currentMovement.x;
        var vertical = currentMovement.y;

        //Animation
        playerAnimator.SetBool("moving", horizontal != 0 || vertical != 0);
    }
}
