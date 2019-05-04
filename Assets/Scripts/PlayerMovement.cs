using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 3f;

    public Rigidbody playerRigidbody;

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
    }
}
