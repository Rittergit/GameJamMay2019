using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiplayerCamera : MonoBehaviour
{
    public bool isSplitscreen = false;
    public bool isPlayer2 = false;

    public void ApplyConfiguration()
    {
        var camera = this.GetComponent<Camera>();
        if (this.isSplitscreen)
        {
            camera.rect = this.isPlayer2
                ? new Rect(0.5f, 0f, 0.5f, 1f)
                : new Rect(0f, 0f, 0.5f, 1f);
        }
        else
        {
            camera.rect = new Rect(0f, 0f, 1f, 1f);
        }
    }
}
