using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Update()
    {
        var shooting = Input.GetButtonDown("Fire");
        Debug.Log("Shooting!");
        if (shooting)
        {
            this.animator.SetTrigger("shoot");
        }
    }
}
