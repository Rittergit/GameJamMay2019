using UnityEngine;
using UnityEngine.Networking;

public class PlayerAnimator : NetworkBehaviour
{
    public Animator playerAnimator;

    void Update()
    {
        check();
    }

    void check()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        //Animation
        playerAnimator.SetBool("moving", horizontal != 0 || vertical != 0);
    }
}
