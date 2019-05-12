using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private SceneAsset nextScene;

    public void GoToNextScene()
    {
        SceneManager.LoadScene(this.nextScene.name);
    }
}
