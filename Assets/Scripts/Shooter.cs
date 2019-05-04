using UnityEngine;

public class Shooter : MonoBehaviour
{   
    [Tooltip("Time delay between to shoot. 1.0f = 1 seconds")]
    public float shootDelay = 1f;
    public AudioClip shootSound;
    public Transform shootPoint;
    [SerializeField] private Animator animator;
    private float timer;

    void Update()
    {
        //Timer is running. Tick tick .... tok
        timer += Time.deltaTime;

        if(timer >= shootDelay && Input.GetButtonDown("Fire"))
        {
            Debug.Log("Choo choo choo, Mother fucker!");

            //Set Trigger Animation
            this.animator.SetTrigger("shoot");

            //FX
            if (shootSound)
            {
                //Play Sound Effect here
            }

            //reset Timer
            timer = 0f;
        }
    }
}
