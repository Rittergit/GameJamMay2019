using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTestScript : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 3f;

    Rigidbody playerRigidbody;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CharacterMovement();
    }

    //MOVEMENT
    void CharacterMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, 0f, vertical);
        if (direction.magnitude > 0)
        {
            playerRigidbody.rotation = Quaternion.LookRotation(direction);
        }

        var movement = Vector3.ClampMagnitude(direction, 1f) * this.speed;
        playerRigidbody.velocity = movement;

        //Animation
        animator.SetBool("moving", horizontal != 0 || vertical != 0);
    }
}
