using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 3f;

    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.animator = this.GetComponentInChildren<Animator>();
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
            this.rb.rotation = Quaternion.LookRotation(direction);
        }

        var movement = Vector3.ClampMagnitude(direction, 1f) * this.speed;
        this.rb.velocity = movement;

        //Animation
        animator.SetBool("moving", horizontal != 0 || vertical != 0);
    }
}
