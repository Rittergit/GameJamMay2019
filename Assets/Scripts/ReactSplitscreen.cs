using UnityEngine;

public class ReactSplitscreen : MonoBehaviour
{
    private Splitscreen splitscreen;

    void Start()
    {
        this.splitscreen = Object.FindObjectOfType<Splitscreen>();
        if (this.splitscreen != null)
            this.splitscreen.StartSplitscreen();

        GameManager.Singleton.IsSplitscreen = this.splitscreen != null;
    }

    void OnDestroy()
    {
        if (this.splitscreen != null)
            GameObject.Destroy(this.splitscreen.gameObject);
    }
}
