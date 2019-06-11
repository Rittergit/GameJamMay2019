using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimator : NetworkBehaviour
{
    public Animator playerAnimator;

    private PlayerMovement playerMovement;

    void Start()
    {
        this.playerMovement = this.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        check();
    }

    void check()
    {
        if (!isLocalPlayer && !GameManager.Singleton.IsSplitscreen)
        {
            return;
        }

        var horizontal = Input.GetAxis(this.playerMovement.Horizontal);
        var vertical = Input.GetAxis(this.playerMovement.Vertical);

        //Animation
        playerAnimator.SetBool("moving", horizontal != 0 || vertical != 0);
    }
}
