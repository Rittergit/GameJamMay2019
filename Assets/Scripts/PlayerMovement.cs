using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody rb;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, 0f, vertical);
        this.rb.rotation = Quaternion.LookRotation(direction);

        var movement = Vector3.ClampMagnitude(direction, 1f) * this.speed;
        this.rb.velocity = movement;
    }
}
